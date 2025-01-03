using TMPro;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Fortune
{
    public class FortuneVictoryWindow : MonoBehaviour
    {
        [SerializeField] private AnimatedWindow _animatedWindow;
        [SerializeField] private Image _musicIconImage;
        [SerializeField] private TextMeshProUGUI _nameText;

        public void Initialize(Sprite musicIcon, string name)
        {
            _musicIconImage.sprite = musicIcon;
            _nameText.text = name;
        }

        public void Open()
        {
            _animatedWindow.Open();
        }
    }
}