using UnityEngine;
using UnityEngine.UI;

namespace Upgrade.UI
{
    public class LevelIndicationImage : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _enabled;
        [SerializeField] private Sprite _disabled;

        public void Enable()
        {
            _image.sprite = _enabled;
        }

        public void Disable()
        {
            _image.sprite = _disabled;
        }
    }
}