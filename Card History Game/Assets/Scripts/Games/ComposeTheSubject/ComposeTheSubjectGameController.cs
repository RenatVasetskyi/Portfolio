using System;
using System.Collections.Generic;
using Architecture.Services.Interfaces;
using Audio;
using Data;
using Games.ComposeTheSubject.Data;
using Games.ComposeTheSubject.Enums;
using Games.ComposeTheSubject.Interfaces;
using Games.ComposeTheSubject.Slots;
using Games.ComposeTheSubject.Subject;
using Random = UnityEngine.Random;

namespace Games.ComposeTheSubject
{
    public class ComposeTheSubjectGameController : IComposeTheSubjectGameController
    {
        private readonly List<SubjectToCompose> _subjectToComposes;
        private readonly List<SlotForSubject> _slotForSubjects;
        
        private readonly GameSettings _gameSettings;
        private readonly IAudioService _audioService;

        public event Action<ComposeTheSubjectQuestion> OnQuestionChanged;
        public event Action<int> OnTriesCountChanged;
        public event Action<int> OnHintUsed;
        public event Action OnEverythingRight;
        public event Action OnWinInQuestion;
        public event Action OnLose;
        public event Action OnFullWin;
        
        private int _checkTriesCountLeft;
        private int _currentQuestionIndex = -1;
        private int _hintsLeft;
        
        public bool CardInDrag { get; set; }
        public int DraggingFingerId { get; set; } = -1;

        public ComposeTheSubjectGameController(GameSettings gameSettings, IAudioService audioService,
            List<SubjectToCompose> subjectToComposes, List<SlotForSubject> slotForSubjects)
        {
            _gameSettings = gameSettings;
            _audioService = audioService;
            _checkTriesCountLeft = gameSettings.CheckTriesCount;
            _hintsLeft = gameSettings.HintsCount;
            _subjectToComposes = subjectToComposes;
            _slotForSubjects = slotForSubjects;
        }

        public void Initialize()
        {
            SetNextQuestion();
            
            OnHintUsed?.Invoke(_hintsLeft);
        }

        public void CheckWinInQuestion()
        {
            List<SubjectType> answers = _gameSettings.ComposeTheSubjectQuestions[_currentQuestionIndex].Answers;
            
            if (_slotForSubjects.TrueForAll(slot => slot.HasSubject()))
            {
                foreach (SlotForSubject slot in _slotForSubjects)
                {
                    if (!answers.Contains(slot.Subject.Type))
                    {
                        LoseTry();
                        return;
                    }
                }
            }
            else
            {
                LoseTry();
                return;
            }

            _audioService.PlaySfx(SfxType.WinQuestion);
            Reset();
            SetNextQuestion();
            OnWinInQuestion?.Invoke();
        }

        public void MakeHint()
        {
            if (_hintsLeft <= 0)
                return;

            List<SubjectType> answers = _gameSettings.ComposeTheSubjectQuestions[_currentQuestionIndex].Answers;
            List<SubjectToCompose> subjectsToHint = new();

            foreach (SubjectToCompose subject in _subjectToComposes)
            {
                if (answers.Contains(subject.Type) & subject.Slot == null)
                    subjectsToHint.Add(subject);
            }
            
            if (subjectsToHint.Count <= 0)
                OnEverythingRight?.Invoke();   
            else
                subjectsToHint[Random.Range(0, subjectsToHint.Count)].DoHintAnimation();    
            
            _audioService.PlaySfx(SfxType.Hint);
            _hintsLeft--;
            OnHintUsed?.Invoke(_hintsLeft);
        }

        public void UpdateSlots()
        {
            foreach (SlotForSubject slot in _slotForSubjects)
                slot.SetSubjectByChildren();
        }

        private void LoseTry()
        {
            _audioService.PlaySfx(SfxType.LoseTry);
            _checkTriesCountLeft--; 
            OnTriesCountChanged?.Invoke(_checkTriesCountLeft); 
            CheckLose();
        }
        
        private void CheckLose()
        {
            if (_checkTriesCountLeft <= 0)
            {
                _audioService.PlaySfx(SfxType.Lose);
                OnLose?.Invoke();
            }
        }

        private void SetNextQuestion()
        {
            if (_currentQuestionIndex >= _gameSettings.ComposeTheSubjectQuestions.Count - 1)
            {
                OnFullWin?.Invoke();
                return;
            }
            
            ComposeTheSubjectQuestion question = _gameSettings.ComposeTheSubjectQuestions[++_currentQuestionIndex];
            OnQuestionChanged?.Invoke(question);
        }

        private void Reset()
        {
            foreach (SlotForSubject slot in _slotForSubjects)
                slot.Clear();

            foreach (SubjectToCompose subject in _subjectToComposes)
                subject.SetOriginalPosition();
        }
    }
}