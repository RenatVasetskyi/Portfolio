using UnityEngine;
using UnityEngine.UI;

namespace ChainOfResponsibility.Example2
{
    public class ChainOfResponsibilityExample2 : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private BasePerformer _entry;

        private void OnEnable() =>
            _button.onClick.AddListener(SignIn);

        private void OnDisable() =>
            _button.onClick.RemoveListener(SignIn);

        private void SignIn() => _entry.Perform();
    }
}