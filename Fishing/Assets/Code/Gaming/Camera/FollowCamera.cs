using UnityEngine;

namespace Code.Gaming.Camera
{
    public class FollowCamera : MonoBehaviour
    {
        private const float SmoothSpeed = 0.125f;
        private readonly Vector3 _offset = new(0, 1f);
        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;
            
            Vector3 desiredPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
            transform.position = new(transform.position.x, smoothedPosition.y);
        }
    }
}