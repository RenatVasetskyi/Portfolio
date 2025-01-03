using Interfaces;

public class Heap<T> where T : IHeapItem<T>
{
    private readonly T[] _items;

    private int _currentItemsCount;

    public int Count => _currentItemsCount;

    public Heap(int maxSize)
    {
        _items = new T[maxSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = _currentItemsCount;
        
        _items[_currentItemsCount] = item;
        
        SortUp(item);
        
        _currentItemsCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = _items[0];

        _currentItemsCount--;

        _items[0] = _items[_currentItemsCount];
        _items[0].HeapIndex = 0;
        
        SortDown(_items[0]);
        
        return firstItem;
    }

    public bool Contains(T item)
    {
        return Equals(_items[item.HeapIndex], item);
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    private void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = _items[parentIndex];

            if (item.CompareTo(parentItem) > 0)
                Swap(item, parentItem);
            else
                break;
            
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    private void SortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;

            int swapIndex = 0;

            if (childIndexLeft < _currentItemsCount)
            {
                swapIndex = childIndexLeft;

                if (childIndexRight < _currentItemsCount)
                {
                    if (_items[childIndexLeft].CompareTo(_items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (item.CompareTo(_items[swapIndex]) < 0)
                    Swap(item, _items[swapIndex]);
                else
                    return;
            }
            else
                return;
        }   
    }

    private void Swap(T firstItem, T secondItem)
    {
        _items[firstItem.HeapIndex] = secondItem;
        _items[secondItem.HeapIndex] = firstItem;

        int firstItemIndex = firstItem.HeapIndex;
        int secondItemIndex = secondItem.HeapIndex;

        firstItem.HeapIndex = secondItemIndex;
        secondItem.HeapIndex = firstItemIndex;
    }
}