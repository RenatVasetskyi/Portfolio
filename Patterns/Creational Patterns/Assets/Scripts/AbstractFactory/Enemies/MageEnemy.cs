using UnityEngine;

namespace AbstractFactory.Enemies
{
    public abstract class MageEnemy : Enemy
    {
        [SerializeField] protected int _mana;

        public void Initialize(int mana)
        { 
            _mana = mana;
        }

        public abstract void CastSphere();
    }
}