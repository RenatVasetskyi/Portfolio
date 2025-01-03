using System.Collections.Generic;
using Architecture.Services.Interfaces;
using Audio;
using Data;
using Games.ComposeTheSubject.Data;
using Games.ComposeTheSubject.Interfaces;
using Games.ComposeTheSubject.Slots;
using Games.ComposeTheSubject.Subject;
using TMPro;
using UI.Base;
using UnityEngine;
using Zenject;

namespace Games.ComposeTheSubject.UI
{
    public class ComposeTheSubjectGameView : MonoBehaviour
    {
        public List<SlotForSubject> SlotsForSubject;
        public List<SubjectToCompose> Subjects;
        
        [SerializeField] private CheckWinButton _checkWinButton;
        [SerializeField] private HintButton _hintButton;
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private AnimatedWindow _victoryWindow;
        [SerializeField] private AnimatedWindow _loseWindow;
        
        private IBaseFactory _baseFactory;
        private IComposeTheSubjectGameController _gameController;

        [Inject]
        public void Construct(IBaseFactory baseFactory)
        {
            _baseFactory = baseFactory;
        }
        
        public void Initialize(IComposeTheSubjectGameController gameController)
        {
            _gameController = gameController;
            _checkWinButton.Initialize(gameController);
            _hintButton.Initialize(gameController);

            foreach (SubjectToCompose subject in Subjects)
                subject.Initialize(gameController);
            
            foreach (SlotForSubject slot in SlotsForSubject)
                slot.Initialize(gameController);
            
            Subscribe();
        }

        private void OnDestroy()
        {
            UnSubscribe();
        }

        private void SetQuestionText(ComposeTheSubjectQuestion question)
        {
            _questionText.text = question.Text;
        }
        
        private void Subscribe()
        {
            _gameController.OnQuestionChanged += SetQuestionText;
            _gameController.OnWinInQuestion += ShowAnimatedText;
            _gameController.OnLose += ShowLoseWindow;
            _gameController.OnEverythingRight += ShowAnimatedText;
            _gameController.OnFullWin += ShowVictoryWindow;
        }

        private void UnSubscribe()
        {
            if (_gameController != null)
            {
                _gameController.OnQuestionChanged -= SetQuestionText;
                _gameController.OnWinInQuestion -= ShowAnimatedText;
                _gameController.OnLose -= ShowLoseWindow;
                _gameController.OnEverythingRight -= ShowAnimatedText;
                _gameController.OnFullWin -= ShowVictoryWindow;
            }
        }

        private void ShowAnimatedText()
        {
            _baseFactory.CreateBaseWithContainer<AnimatedText>(AssetPath.AnimatedText, transform);
        }
        
        private void ShowLoseWindow()
        { 
            _loseWindow.Open();
        }
        
        private void ShowVictoryWindow()
        { 
            _victoryWindow.Open();
        }
    }
}