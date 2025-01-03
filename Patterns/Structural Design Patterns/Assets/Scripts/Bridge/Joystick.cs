using UnityEngine;

namespace Bridge
{
    public class Joystick : IDevice
    {
        public void Work()
        {
            Debug.Log("Joystick work");
        }
    }
}