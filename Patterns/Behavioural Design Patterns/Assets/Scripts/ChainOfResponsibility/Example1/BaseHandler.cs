using TMPro;
using UnityEngine;

namespace ChainOfResponsibility.Example1
{
    public class BaseHandler : MonoBehaviour, IPerformer
    {
        [SerializeField] protected TMP_InputField _inputField;
        
        [SerializeField] protected BaseHandler _next;

        [SerializeField] protected string _key;

        public virtual bool Perform()
        {
            if (_inputField.text == _key)
                return true;
            else if (_next != null)
                _next.Perform();

            return false;
        }
    }
}