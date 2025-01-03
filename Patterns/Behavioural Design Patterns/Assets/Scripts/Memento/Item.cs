using UnityEngine;

namespace Memento
{
    [CreateAssetMenu(fileName = "Item", menuName = "Memento/Item")]
    public class Item : ScriptableObject
    {
        public string Name;
        public Sprite Sprite;
    }
}