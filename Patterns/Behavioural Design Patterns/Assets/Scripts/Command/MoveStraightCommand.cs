using UnityEngine;

namespace Command
{
    public class MoveStraightCommand : MoveCommand
    {
        public MoveStraightCommand(float step, Transform transform) : base(step, transform) {}

        public override void Execute()
        {
            _transform.position += Vector3.right * _step;
        }

        public override void Undo()
        {
            _transform.position -= Vector3.right * _step;
        }
    }
}