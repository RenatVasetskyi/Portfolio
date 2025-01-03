using System.Collections.Generic;
using EventBus.Interfaces;
using UnityEngine;

namespace EventBus
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> Bindings = new();

        public static void Register(IEventBinding<T> binding)
        {
            Bindings.Add(binding);
        }
        
        public static void UnRegister(IEventBinding<T> binding)
        {
            Bindings.Remove(binding);
        }

        public static void Raise(T @event)
        {
            foreach (IEventBinding<T> binding in Bindings)
            {
                binding.OnEvent?.Invoke(@event);
                binding.OnEventNoArgs?.Invoke();
            }
        }

        private static void Clear()
        {
            Bindings.Clear();
            
            Debug.Log($"Clear {typeof(T).Name} bindings");
        }
    }
}