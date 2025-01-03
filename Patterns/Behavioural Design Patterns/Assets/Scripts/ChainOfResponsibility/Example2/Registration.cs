using UnityEngine;

namespace ChainOfResponsibility.Example2
{
    public class Registration : BasePerformer
    {
        public override void Perform() => Open();

        private void Open() =>
            Debug.Log("Open registration window");
    }
}