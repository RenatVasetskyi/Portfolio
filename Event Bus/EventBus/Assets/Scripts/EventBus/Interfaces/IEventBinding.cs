using System;

namespace EventBus.Interfaces
{
    public interface IEventBinding<T>
    {
        Action<T> OnEvent { get; set; }
        Action OnEventNoArgs { get; set; }
    }
}