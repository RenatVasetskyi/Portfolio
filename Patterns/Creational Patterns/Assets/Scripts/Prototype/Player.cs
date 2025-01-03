using UnityEngine;

namespace Prototype
{
    public class Player : MonoBehaviour, ICloneable
    {
        [SerializeField] private int _speed;
        [SerializeField] private int _damage;

        public ICloneable Clone()
        {
            Player clone = Instantiate(this);
            clone.Initialize(this);

            return clone;
        }

        private void Initialize(Player player)
        {
            player._speed = _speed;
            player._damage = _damage;
        }
    }
}