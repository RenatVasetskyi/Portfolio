using UnityEngine;

namespace Command
{
    public class MoveDownCommand : MoveCommand
    {
        public MoveDownCommand(float step, Transform transform) : base(step, transform) {}

        public override void Execute()
        {
            _transform.position += -Vector3.up * _step;
        }

        public override void Undo()
        {
            _transform.position -= -Vector3.up * _step;
        }
    }
}