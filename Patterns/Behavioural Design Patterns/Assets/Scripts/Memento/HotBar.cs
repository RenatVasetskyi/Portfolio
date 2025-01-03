using System.Collections.Generic;

namespace Memento
{
    public class HotBar : MementoStorage
    {
        public Memento CreateMemento()
        {
            return new Memento(new List<Item>(_items));
        }
        
        public void SetMemento(Memento memento)
        {
            _items = memento.GetItems();

            for (int i = 0; i < _uiSlots.Count; i++)
                _uiSlots[i].UpdateUI(_items[i]);
        }
        
        public class Memento
        {
            private readonly List<Item> _items;

            public Memento(List<Item> items)
            {
                _items = items;
            }

            public List<Item> GetItems()
            {
                return _items;
            }
        }
    }
}