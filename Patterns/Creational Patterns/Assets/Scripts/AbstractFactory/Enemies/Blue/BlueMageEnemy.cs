using UnityEngine;

namespace AbstractFactory.Enemies.Blue
{
    public class BlueMageEnemy : MageEnemy
    {
        public override void CastSphere()
        {
            Debug.Log("Blue mage enemy cast sphere");
        }
    }
}