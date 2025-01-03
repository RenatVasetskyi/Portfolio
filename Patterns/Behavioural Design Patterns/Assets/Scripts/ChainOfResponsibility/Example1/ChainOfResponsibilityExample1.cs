using UnityEngine;
using UnityEngine.UI;

namespace ChainOfResponsibility.Example1
{
    public class ChainOfResponsibilityExample1 : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private BaseHandler[] _handlers;

        private void OnEnable() =>
            _button.onClick.AddListener(CheckChain);

        private void OnDisable() =>
            _button.onClick.RemoveListener(CheckChain);

        private void CheckChain()
        {
            foreach (BaseHandler handler in _handlers)
            {
                if (handler.Perform() == false)
                    return;
            }
            
            Enter();
        }
        
        private void Enter() =>
            Debug.Log("Enter");
    }
}