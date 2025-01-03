using UnityEngine;

namespace ChainOfResponsibility.Example2
{
    public abstract class BasePerformer : MonoBehaviour
    {
        public abstract void Perform();
    }
}