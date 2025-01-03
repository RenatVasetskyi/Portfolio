using System;
using Code.Gameplay.Swipes.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Gameplay.Swipes
{
    public class Swiper : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler
    {
        private const float MinDistanceForSwipeDetection = 0.7f;

        private const int MaxAngle = 180;

        private const int AngleOne = -45;
        private const int AngleTwo = 45;
        private const int AngleThree = 135;
        private const int AngleFour = -135;

        public event Action<SwipeDirection, Vector2> OnSwiped;

        [SerializeField] private Image _image;

        private Vector2 _startFingerPosition;
        private Vector2 _endFingerPosition;

        private Camera _camera;

        private bool _canSwipe;

        public void OnPointerDown(PointerEventData eventData)
        {
            _startFingerPosition = _camera.ScreenToWorldPoint(eventData.position);

            _canSwipe = true;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!_canSwipe | _camera == null)
                return;
            
            _endFingerPosition = _camera.ScreenToWorldPoint(eventData.position);

            if (Vector2.Distance(_startFingerPosition, _endFingerPosition) >= MinDistanceForSwipeDetection)
            {
                CalculateAngle();

                _canSwipe = false;
            }
        }
        
        public void Disable()
        {
            _image.raycastTarget = false;
        }
        
        public void Enable()
        {
            _image.raycastTarget = true;
        }

        private void Awake()
        {
            _camera = Camera.main;
            Enable(); 
        }

        private void CalculateAngle()
        {
            float swipeAngle = Mathf.Atan2(_endFingerPosition.y - _startFingerPosition.y,
                _endFingerPosition.x - _startFingerPosition.x) * MaxAngle / Mathf.PI;

            GetDirectionByAngle(swipeAngle);
        }

        private void GetDirectionByAngle(float swipeAngle)
        {
            switch (swipeAngle)
            {
                case > AngleOne and <= AngleTwo:
                    OnSwiped?.Invoke(SwipeDirection.Right, _startFingerPosition);
                    break;
                case > AngleTwo and <= AngleThree:
                    OnSwiped?.Invoke(SwipeDirection.Up, _startFingerPosition);
                    break;
                case > AngleThree:
                case <= AngleFour:
                    OnSwiped?.Invoke(SwipeDirection.Left, _startFingerPosition);
                    break;
                case < AngleOne and >= AngleFour:
                    OnSwiped?.Invoke(SwipeDirection.Down, _startFingerPosition);
                    break;
            }
        }
    }
}