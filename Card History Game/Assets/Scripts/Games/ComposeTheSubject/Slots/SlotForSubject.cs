using Games.ComposeTheSubject.Interfaces;
using Games.ComposeTheSubject.Subject;
using UI.Rect;
using UI.Rect.Enums;
using UnityEngine;

namespace Games.ComposeTheSubject.Slots
{
    public class SlotForSubject : MonoBehaviour
    {
        private IComposeTheSubjectGameController _gameController;
        public SubjectToCompose Subject { get; private set; }

        public void Initialize(IComposeTheSubjectGameController gameController)
        {
            _gameController = gameController;
        }

        public void SetSubject(SubjectToCompose subject, bool switchSubjects)
        {
            if (switchSubjects)
            {
                if (Subject != null & subject.Slot != null)
                    subject.Slot.SetSubject(Subject, false);
                else if(Subject != null)
                    Subject.SetOriginalPosition();
            }

            subject.transform.SetParent(transform);
            subject.RectTransform.SetAnchorPreset(AnchorPresets.Centered);
            subject.transform.localPosition = Vector3.zero;
            subject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            _gameController.UpdateSlots();
        }

        public bool HasSubject()
        {
            return Subject != null;
        }

        public void Clear()
        {
            Subject = null;
        }

        public void SetSubjectByChildren()
        {
            SubjectToCompose subject = transform.GetComponentInChildren<SubjectToCompose>();
            Subject = subject;

            if (subject != null)
                subject.Slot = this;
        }
    }
}