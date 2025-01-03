using System;
using Assets.Scripts.Architecture.Services.Interfaces;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts.Architecture.Services
{
    public class InputService : IInputService
    {
        private readonly PlayerInput _playerInput;

        public event Action<Vector2> OnScreenTouched;
        public event Action<Vector2> OnScreenTouchEnded;

        public InputService(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.Touches.Touch.started += context => StartTouch(context);
            _playerInput.Touches.Touch.canceled += context => EndTouch();
        }

        private void StartTouch(InputAction.CallbackContext context) =>
            OnScreenTouched?.Invoke(context.ReadValue<Vector2>());

        private void EndTouch() => 
            OnScreenTouchEnded?.Invoke(_playerInput.Touches.TouchPosition.ReadValue<Vector2>());
    }
}