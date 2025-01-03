using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Person
{
    public class OrderDialogWindow : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void DisplayOrder(Sprite sprite)
        {
            _image.sprite = sprite;
            _image.SetNativeSize();
        }
    }
}