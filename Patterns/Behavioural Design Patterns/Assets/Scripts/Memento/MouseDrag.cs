using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Memento
{
    public class MouseDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private MementoStorage _storage;
        private UISlot _uiSlot;
        private GameObject _objectToDrag;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _storage.SwapItem(_uiSlot);

            PrepareObjectToDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_objectToDrag != null)
            {
                _objectToDrag.transform.position = Input.mousePosition;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SwapItems(eventData);
        }
        
        public void Setup(MementoStorage storage, UISlot uiSlot)
        {
            _storage = storage;
            _uiSlot = uiSlot;
        }

        private void SwapItems(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject is GameObject targetObject)
            {
                UISlot targetSlot = targetObject.GetComponentInChildren<UISlot>();

                if (targetSlot != null)
                {
                    _storage.SwapItem(targetSlot);
                    
                    EventSystem.current.SetSelectedGameObject(_objectToDrag);
                }
            }
            
            _storage.ClearSwap();

            if (_objectToDrag != null)
            {
                Destroy(_objectToDrag);
            }
        }
        
        private void PrepareObjectToDrag()
        {
            _objectToDrag = new GameObject("Drag" + _uiSlot.name);

            Image image = _objectToDrag.AddComponent<Image>();

            image.sprite = _uiSlot.Sprite;
            image.raycastTarget = false;
            
            _objectToDrag.transform.SetParent(_storage.transform);
            _objectToDrag.transform.localScale = Vector3.one;
        }
    }
}