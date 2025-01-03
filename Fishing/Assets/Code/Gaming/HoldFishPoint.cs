using UnityEngine;

namespace Code.Gaming
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class HoldFishPoint : MonoBehaviour
    {
        [SerializeField] private Hook _hook;
        
        private BoxCollider2D _collider;
        private Transform _fish;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_fish != null)
                return;
            
            if (other.TryGetComponent(out Fish fish))
            {
                if (fish.OnHook)
                    return;
                
                fish.TakeOnHook(transform);
                _fish = fish.transform;
                _hook.SetFish(fish);
            }
        }

        public void Disable()
        {
            _collider.enabled = false;
        }
        
        public void Enable()
        {
            _collider.enabled = true;
        }
    }
}