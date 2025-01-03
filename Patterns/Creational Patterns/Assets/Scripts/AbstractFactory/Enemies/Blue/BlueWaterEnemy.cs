using UnityEngine;

namespace AbstractFactory.Enemies.Blue
{
    public class BlueWaterEnemy : WaterEnemy
    {
        public override void CastWaterBall()
        {
            Debug.Log("Blue water enemy cast sphere");
        }
    }
}