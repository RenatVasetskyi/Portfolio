using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Buttons
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleChangeSprite : MonoBehaviour
    {
        [SerializeField] private Sprite _isOnSprite;
        [SerializeField] private Sprite _isOffSprite;
        
        private Toggle _toggle;
        
        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(ChangeSprite);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(ChangeSprite);
        }

        private void ChangeSprite(bool isOn)
        {
            _toggle.image.sprite = isOn ? _isOnSprite : _isOffSprite;
        }
    }
}