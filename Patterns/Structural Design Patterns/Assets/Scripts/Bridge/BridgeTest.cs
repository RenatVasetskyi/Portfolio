using UnityEngine;

namespace Bridge
{
    public class BridgeTest : MonoBehaviour
    {
        private void Awake()
        {
            Controller controller = new Controller(new Joystick());
         
            Player player = new Player();
            player.Move(controller);
        }
    }
}