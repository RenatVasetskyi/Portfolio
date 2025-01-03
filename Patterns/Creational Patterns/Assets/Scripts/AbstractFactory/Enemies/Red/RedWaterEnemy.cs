using UnityEngine;

namespace AbstractFactory.Enemies.Red
{
    public class RedWaterEnemy : WaterEnemy
    {
        [SerializeField] private float _waterBallImpulse;
        
        public void Initialize(int waterBallImpulse)
        { 
            _waterBallImpulse = waterBallImpulse;
            base.Initialize(_waterCount);
        }

        public override void CastWaterBall()
        {
            Debug.Log("Red water enemy cast water ball");
        }
    }
}