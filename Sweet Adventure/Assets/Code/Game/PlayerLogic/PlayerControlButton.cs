using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Game.PlayerLogic
{
    public class PlayerControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private int _direction;
        [SerializeField] private PlayerControls _playerControls;

        public bool IsPressed { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            _playerControls.SetDirection(_direction);
            IsPressed = true;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            IsPressed = false;
            _playerControls.ResetDirection();
        }
    }
}