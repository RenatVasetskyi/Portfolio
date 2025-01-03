using UnityEngine;

namespace Games.ComposeTheSubject.Subject.Animation
{
    public class SubjectAnimator
    {
        private readonly Animator _animator;
        private readonly SubjectAnimationHashHolder _subjectAnimationHashHolder;

        public SubjectAnimator(Animator animator)
        {
            _animator = animator;
            _subjectAnimationHashHolder = new(SubjectAnimationName.Idle, SubjectAnimationName.Hint);
        }

        public void PlayIdleAnimation()
        {
            _animator.Play(_subjectAnimationHashHolder.Idle);
        }

        public void PlayHintAnimation()
        {
            _animator.Play(_subjectAnimationHashHolder.Hint);
        }
    }
}