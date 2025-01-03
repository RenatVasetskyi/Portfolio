using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop.Pages
{
    public class OpenWindowToggle : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private MultipageWindow _multipageWindow;
        
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _disabledSprite;

        public void Disable()
        {
            _image.sprite = _disabledSprite;
            _window.SetActive(false);
        }
        
        public void Enable()
        {
            _image.sprite = _enabledSprite;
            _image.SetNativeSize();
            _window.SetActive(true);
            _multipageWindow.OnToggleStateChanged(this);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Enable);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Enable);
        }
    }
}