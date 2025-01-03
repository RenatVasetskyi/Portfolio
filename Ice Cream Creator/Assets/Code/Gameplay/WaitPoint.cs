using UnityEngine;

namespace Code.Gameplay
{
    public class WaitPoint : MonoBehaviour
    {
        private Transform _customer;

        public void SetCustomer(Transform customer)
        {
            if (_customer != null)
                return;    
            
            _customer = customer;
        }

        public bool IsEmpty()
        {
            return _customer == null;
        }
    }
}