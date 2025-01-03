using System;
using Games.ComposeTheSubject.Data;

namespace Games.ComposeTheSubject.Interfaces
{
    public interface IComposeTheSubjectGameController
    {
        event Action<ComposeTheSubjectQuestion> OnQuestionChanged;
        event Action<int> OnTriesCountChanged;
        event Action<int> OnHintUsed;
        public event Action OnEverythingRight;
        event Action OnWinInQuestion;
        event Action OnFullWin;
        event Action OnLose;
        bool CardInDrag { get; set; }
        int DraggingFingerId { get; set; }
        void Initialize();
        void CheckWinInQuestion();
        void MakeHint();
        void UpdateSlots();
    }
}