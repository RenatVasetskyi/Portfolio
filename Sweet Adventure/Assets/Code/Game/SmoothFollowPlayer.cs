using UnityEngine;

namespace Code.Game
{
    public class SmoothFollowPlayer : MonoBehaviour
    {
        private const float Offset = 2f;
        private const float Smoothing = 0.9f;
        
        [SerializeField] private Transform _player;

        private void LateUpdate()
        {
            Vector3 currentPosition = transform.position;
            float targetY = _player.position.y + Offset;

            float newY = Mathf.Lerp(currentPosition.y, targetY, Smoothing * Time.deltaTime);

            transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);
        }
    }
}