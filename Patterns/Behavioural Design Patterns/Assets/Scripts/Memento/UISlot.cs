using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Memento
{
    public class UISlot : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private MementoStorage _storage;
        private MouseDrag _mouseDrag;

        public Sprite Sprite => _image.sprite;

        public void SetupStorage(MementoStorage storage)
        {
            _storage = storage;
        }

        public MementoStorage GetStorage()
        {
            return _storage;
        }

        public void UpdateUI(Item item)
        {
            _image.sprite = item == null ? null : item.Sprite;
        }

        public void SetupMouseDrag(MementoStorage mementoStorage)
        {
            _mouseDrag = gameObject.GetOrAddComponent<MouseDrag>();
            _mouseDrag.Setup(mementoStorage, this);
        }
    }
}