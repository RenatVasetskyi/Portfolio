using EventBus.Interfaces;

namespace EventBus.Events
{
    public struct PlayerEvent : IEvent
    {
        public int Health;
        public int Mana;
    }
}