using UnityEngine;

namespace AbstractFactory.Enemies.Blue
{
    public class BlueFireEnemy : FireEnemy
    {
        public override void CastFireBeam()
        {
            Debug.Log("Blue fire enemy cast fire beam");
        }
    }
}