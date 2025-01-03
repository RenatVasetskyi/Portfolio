using UnityEngine;
using UnityEngine.UI;

namespace ChainOfResponsibility.Example1
{
    public class ButtonColorHandler : BaseHandler
    {
        [SerializeField] private Image _image;

        public override bool Perform()
        {
            if (_key == _inputField.text)
            {
                _image.color = Color.cyan;
                return true;
            }
            else if(_next != null)
                _next.Perform();

            return false;
        }
    }
}