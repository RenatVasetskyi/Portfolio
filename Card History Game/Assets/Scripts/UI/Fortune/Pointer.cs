using UnityEngine;

namespace UI.Fortune
{
    public class Pointer : MonoBehaviour
    {
        [SerializeField] private FortuneWheel _fortuneWheel;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out FortuneWheelSlice fortuneWheelSlice))
            {
                _fortuneWheel.SetCurrentSliceOnPointer(fortuneWheelSlice);
            }
        }
    }
}