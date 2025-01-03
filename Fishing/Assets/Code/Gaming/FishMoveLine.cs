using UnityEngine;

namespace Code.Gaming
{
    public class FishMoveLine : MonoBehaviour
    {
        public Transform Start;
        public Transform End;
        
        private Transform _fish;

        public bool HasFish => _fish != null;

        public void SetFish(Transform fish)
        {
            _fish = fish;
        }
    }
}