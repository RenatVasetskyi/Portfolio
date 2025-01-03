using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVP
{
    public class View : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private Image _image;

        [SerializeField] private TMP_InputField _inputField;

        [SerializeField] private TextMeshProUGUI _text;
        
        private Presenter _presenter;

        public void UpdateView(Color color, string text)
        {
            _image.color = color;
            _text.text = text;
        }

        private void Awake() =>
            _presenter = new Presenter(this);

        private void OnEnable() =>
            _button.onClick.AddListener(OnButtonClickHandler);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnButtonClickHandler);

        private void OnButtonClickHandler() =>
            _presenter.OnChangeTextClicked(_inputField.text);
    }
}