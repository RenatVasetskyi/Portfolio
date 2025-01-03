using Code.Game.PlayerLogic.Machine.Interfaces;
using UnityEngine;

namespace Code.Game.PlayerLogic.Machine.States
{
    public class PlayerWalkState : IPlayerState
    {
        private const float MinVelocityToStop = 0.01f;
        private const int ZeroDirection = 0;
        
        private readonly Player _player;

        public PlayerWalkState(Player player)
        {
            _player = player;
        }
        
        public void Enter()
        {
            _player.PlayerSpineAnimator.PlayWalk();
        }

        public void Exit()
        {
        }

        public void OnUpdate()
        {
        }

        public void OnFixedUpdate()
        {
            if (_player.LockedFull | _player.LockedHorizontal)
                return;
            
            _player.DoHorizontalMovement();
            
            if (Mathf.Abs(_player.Rigidbody.velocity.x) <= MinVelocityToStop & Mathf.Abs(_player.Rigidbody.velocity.y) <= MinVelocityToStop)
                _player.PlayerStateMachine.Enter<PlayerIdleState>();
            else if (_player.PlayerControls.HorizontalDirection == ZeroDirection)
                _player.PlayerStateMachine.Enter<PlayerIdleState>();
        }
    }
}