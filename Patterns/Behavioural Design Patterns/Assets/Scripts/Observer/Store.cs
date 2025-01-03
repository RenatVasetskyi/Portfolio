using System.Collections.Generic;
using System.Linq;
using Observer.Interfaces;
using UnityEngine;

namespace Observer
{
    public class Store : IStore
    {
        private readonly List<IObserver> _observers = new();
        
        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void UnSubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserver observer in _observers.ToList())
            {
                observer.GetInfo(this);
            }
        }

        public void Sale()
        {
            Debug.Log("Store sale something");
        }

        public void GetNewGoods()
        {
            Debug.Log("Store got new goods");
            
            Notify();
        }
    }
}