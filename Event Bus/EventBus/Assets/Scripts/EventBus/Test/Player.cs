using EventBus.Events;
using EventBus.Interfaces;
using UnityEngine;

namespace EventBus.Test
{
    public class Player : MonoBehaviour
    {
        private IEventBinding<PlayerEvent> _playerEventBinding;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EventBus<PlayerEvent>.Raise(new PlayerEvent
                {
                    Health = 10,
                    Mana = 5
                });
            }
        }

        private void OnEnable()
        {
            _playerEventBinding = new EventBinding<PlayerEvent>(HandlePlayerEvent);
            EventBus<PlayerEvent>.Register(_playerEventBinding);
        }

        private void OnDisable()
        {
            EventBus<PlayerEvent>.UnRegister(_playerEventBinding);
        }

        private void HandlePlayerEvent(PlayerEvent playerEvent)
        {
            Debug.Log($"Health: {playerEvent.Health}, Mana: {playerEvent.Mana}");   
        }
    }
}