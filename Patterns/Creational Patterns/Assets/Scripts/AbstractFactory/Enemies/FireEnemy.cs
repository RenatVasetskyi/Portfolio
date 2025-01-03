using UnityEngine;

namespace AbstractFactory.Enemies
{
    public abstract class FireEnemy : Enemy
    {
        [SerializeField] protected int _beamLenght;

        public void Initialize(int beamLenght)
        {
            _beamLenght = beamLenght;
        }
        
        public abstract void CastFireBeam();
    }
}