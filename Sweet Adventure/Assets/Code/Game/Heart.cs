using UnityEngine;
using UnityEngine.UI;

namespace Code.Game
{
    public class Heart : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _on;
        [SerializeField] private Sprite _off;

        public void On()
        {
            _image.sprite = _on;
        }

        public void Off()
        {
            _image.sprite = _off;
        }
    }
}