using UnityEngine;

namespace Bridge
{
    public class KeyBoard : IDevice
    {
        public void Work()
        {
            Debug.Log("Keyboard work");
        }
    }
}