using System;
using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Audio;
using Assets.Scripts.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Ball
{
    public class BallMovement : MonoBehaviour
    {
        public event Action OnThrown;

        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _forwardThrowForce;
        [SerializeField] private float _upThrowForce;

        private IInputService _inputService;
        private IAudioService _audioService;

        private Vector2 _startPosition;

        private Camera _camera;

        [Inject]
        public void Construct(IInputService inputService, IAudioService audioService)
        {
            _inputService = inputService;
            _audioService = audioService;
        }

        private void OnEnable()
        {
            _camera = Camera.main;

            _inputService.OnScreenTouched += (startPostion) => _startPosition = startPostion;
            _inputService.OnScreenTouchEnded += Throw;

            _rigidbody.isKinematic = true;
        }

        private void OnDisable() =>
            _inputService.OnScreenTouchEnded -= Throw;

        private void Throw(Vector2 endPosition)
        {
            _audioService.PlaySfx(SfxType.Throw);

            _rigidbody.isKinematic = false;
            Vector3 throwDirection = GetThrowDirection(endPosition);
            _rigidbody.AddForce(new Vector3(throwDirection.x, throwDirection.y * _upThrowForce, throwDirection.z * _forwardThrowForce) , ForceMode.Impulse);
            enabled = false;
            OnThrown?.Invoke();
        }

        private Vector3 GetThrowDirection(Vector2 endPosition) =>
            endPosition.ToWorldPosition(_camera) - _startPosition.ToWorldPosition(_camera);
    }
}