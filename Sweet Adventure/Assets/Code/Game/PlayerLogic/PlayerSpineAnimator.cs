using System.Linq;
using Code.Infrastructure.Interfaces;
using Spine.Unity;
using UnityEngine;
using Zenject;

namespace Code.Game.PlayerLogic
{
    public class PlayerSpineAnimator : MonoBehaviour
    {
        private const int WalkAnimationSpeed = 15;
        private const int JumpAnimationSpeed = 20;
        
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private AnimationReferenceAsset _idle;
        [SerializeField] private AnimationReferenceAsset _walk;
        [SerializeField] private AnimationReferenceAsset _jump;
        
        private IPlayerDataSocket _playerDataSocket;
        private Data _data;

        [Inject]
        public void Initialize(IPlayerDataSocket playerDataSocket, Data data)
        {
            _data = data;
            _playerDataSocket = playerDataSocket;
        }
        
        public void PlayIdle()
        {
            _skeletonAnimation.state.SetAnimation(0, _idle, true).TimeScale = 1;
        }
        
        public void PlayWalk()
        {
            _skeletonAnimation.state.SetAnimation(0, _walk, true).TimeScale = 
                (_playerDataSocket.MovementSpeed / _data.MovementSpeed.First().Value) * WalkAnimationSpeed;
        }
        
        public void PlayJump()
        {
            _skeletonAnimation.state.SetAnimation(0, _jump, false).TimeScale = 
                (_data.JumpPower.First().Value / _playerDataSocket.JumpPower) * JumpAnimationSpeed;
        }
    }
}