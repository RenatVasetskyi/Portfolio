using UnityEngine;

namespace AbstractFactory.Enemies
{
    public abstract class WaterEnemy : Enemy
    {
        [SerializeField] protected int _waterCount;

        public void Initialize(int waterCount)
        { 
            _waterCount = waterCount;
        }

        public abstract void CastWaterBall();
    }
}