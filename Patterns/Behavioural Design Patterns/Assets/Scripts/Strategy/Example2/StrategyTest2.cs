using Strategy.Example2.Guns;
using UnityEngine;

namespace Strategy.Example2
{
    public class StrategyTest2 : MonoBehaviour
    {
        private void Awake()
        {
            BaseWeapon baseWeapon = new Pistol();
            baseWeapon.TryDoDamage();

            baseWeapon = new WaterGun();
            baseWeapon.TryDoDamage();
        }
    }
}