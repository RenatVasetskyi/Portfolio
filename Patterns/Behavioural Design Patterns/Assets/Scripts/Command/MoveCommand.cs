using Command.Interfaces;
using UnityEngine;

namespace Command
{
    public abstract class MoveCommand : ICancellableCommand
    {
        protected readonly float _step;
        protected readonly Transform _transform;

        protected MoveCommand(float step, Transform transform)
        {
            _step = step;
            _transform = transform;
        }

        public abstract void Execute();
        public abstract void Undo();
    }
}