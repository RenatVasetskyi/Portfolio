using TMPro;
using UnityEngine;

namespace ChainOfResponsibility.Example2
{
    public class SignIn : BasePerformer
    {
        [SerializeField] private string _login;
        [SerializeField] private string _password;

        [SerializeField] private TMP_InputField _loginInputField;
        [SerializeField] private TMP_InputField _passwordInputField;

        [SerializeField] private BasePerformer _next;
        
        public override void Perform()
        {
            if (_loginInputField.text == _login & _passwordInputField.text == _password)
                EnterAccount();
            else if (_next != null)
                _next.Perform();
        }

        private void EnterAccount() =>
            Debug.Log("Enter account");
    }
}