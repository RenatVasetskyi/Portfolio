using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayButtonAnimation
{
    public class Element : MonoBehaviour
    {
        public int PositionInLine;

        [SerializeField] private Image _image; 

        public void SetSprite(Sprite elementSprite)
        {
            _image.sprite = elementSprite;
            _image.SetNativeSize();
        }
    }
}