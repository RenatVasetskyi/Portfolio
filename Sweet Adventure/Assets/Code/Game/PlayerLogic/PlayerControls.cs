using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Game.PlayerLogic
{
    public class PlayerControls : MonoBehaviour
    {
        public event Action OnMove;
        public event Action OnJump;
        
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
        [SerializeField] private Button _jumpButton;
        
        public float HorizontalDirection { get; private set; }

        public void SetDirection(int direction)
        {
            HorizontalDirection = direction;
            OnMove?.Invoke();
        }
        
        private void Awake()
        {
            _jumpButton.onClick.AddListener(SendJump);
        }

        private void OnDestroy()
        {
            _jumpButton.onClick.RemoveListener(SendJump);
        }
        
        private void SendJump()
        {
            OnJump?.Invoke();
        }

        public void ResetDirection()
        {
            if (!_leftArrow.GetComponent<PlayerControlButton>().IsPressed &
                !_rightArrow.GetComponent<PlayerControlButton>().IsPressed)
                HorizontalDirection = 0;   
        }
    }
}