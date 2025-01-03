using System;
using UnityEngine;

namespace InputSystems
{
    public class InputSystem : MonoBehaviour
    {
        public event Action OnShootPressed;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                OnShootPressed?.Invoke();
        }
    }
}