using UnityEngine;

namespace Code.Store
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private MovementSpeedIncreaser _movementSpeedIncreaser;
        [SerializeField] private JumpPowerIncreaser _jumpPowerIncreaser;
        
        public void UpdateAll()
        {
            _movementSpeedIncreaser.UpdateAll();
            _jumpPowerIncreaser.UpdateAll();
        }

        private void OnEnable()
        {
            UpdateAll();
        }
    }
}