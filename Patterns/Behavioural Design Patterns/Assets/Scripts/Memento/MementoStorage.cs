using System.Collections.Generic;
using UnityEngine;

namespace Memento
{
    public class MementoStorage : MonoBehaviour
    {
        [SerializeField] private bool _isStatic;
        
        [SerializeField] protected List<UISlot> _uiSlots;
        [SerializeField] protected List<Item> _items;

        private UISlot _uiSlotToSwap;

        public void SwapItem(UISlot targetSlot)
        {
            if (_uiSlotToSwap == null)
            {
                _uiSlotToSwap = targetSlot;
            }
            else if (_uiSlotToSwap == targetSlot)
            {
                ClearSwap();
            }
            else
            {
                MementoStorage storage1 = _uiSlotToSwap.GetStorage();
                int index1 = storage1.GetItemIndex(_uiSlotToSwap);
                Item item1 = storage1.GetItem(index1);
                
                MementoStorage storage2 = targetSlot.GetStorage();
                int index2 = storage2.GetItemIndex(targetSlot);
                Item item2 = storage2.GetItem(index2);

                if (!storage1._isStatic)
                {
                    storage1.SetItemInSlot(index1, item2);
                    _uiSlotToSwap.UpdateUI(item2);
                }

                if (!storage2._isStatic)
                {
                    storage2.SetItemInSlot(index2, item1);
                    targetSlot.UpdateUI(item1);
                }
                
                ClearSwap();
            }
        }
        
        public void ClearSwap()
        {
            _uiSlotToSwap = null;
        }
        
        private void Awake()
        {
            InitializeUISlots();
        }

        private void InitializeUISlots()
        {
            for (int i = 0; i < _uiSlots.Count; i++)
            {
                _uiSlots[i].UpdateUI(_items[i]);
                _uiSlots[i].SetupStorage(this);
                _uiSlots[i].SetupMouseDrag(this);   
            }
        }

        private int GetItemIndex(UISlot uiSlot)
        {
            return _uiSlots.IndexOf(uiSlot);
        }

        private Item GetItem(int index)
        {
            return _items[index];
        }

        private void SetItemInSlot(int index, Item item)
        {
            _items[index] = item;
        }
    }
}