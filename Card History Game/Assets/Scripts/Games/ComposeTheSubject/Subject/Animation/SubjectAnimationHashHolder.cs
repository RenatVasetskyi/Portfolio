using UnityEngine;

namespace Games.ComposeTheSubject.Subject.Animation
{
    public class SubjectAnimationHashHolder
    {
        public int Idle { get; }
        public int Hint { get; }

        public SubjectAnimationHashHolder(string idle, string hint)
        {
            Idle = Animator.StringToHash(idle);
            Hint = Animator.StringToHash(hint);
        }
    }
}