using Observer.Interfaces;
using UnityEngine;

namespace Observer
{
    public class ObserverTest : MonoBehaviour
    {
        private void Awake()
        {
            Test();
        }

        private void Test()
        {
            IStore store = new Store();
            
            store.Subscribe(new Person1());
            
            store.GetNewGoods();
        }
    }
}