using Code.Game.PlayerLogic.Machine.Interfaces;
using Code.Music;
using UnityEngine;

namespace Code.Game.PlayerLogic.Machine.States
{
    public class PlayerJumpState : IPlayerState
    {
        private readonly Player _player;

        public PlayerJumpState(Player player) 
        {
            _player = player;
        }
        
        public void Enter()
        {
            DoJump();
        }

        public void Exit()
        {
        }

        public void OnUpdate()
        {
            
        }

        public void OnFixedUpdate()
        {
            if (_player.PlayerControls.HorizontalDirection != 0 & !_player.LockedFull & !_player.LockedHorizontal)
                _player.DoHorizontalMovement();
        }

        private void DoJump()
        {
            _player.PlayerSpineAnimator.PlayJump();
            _player.Rigidbody.AddForce(Vector2.up * _player.PlayerDataSocket.JumpPower, ForceMode2D.Force);
            _player.GameSoundPlayer.Play(ShortSfx.Jump);
        }
    }
}