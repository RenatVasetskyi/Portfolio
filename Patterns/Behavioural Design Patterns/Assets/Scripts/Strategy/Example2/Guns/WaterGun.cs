using Strategy.Example2.Damages;

namespace Strategy.Example2.Guns
{
    public class WaterGun : BaseWeapon
    {
        public WaterGun()
        {
            _damage = 15;
            AddDamageType(new WaterDamage());
        }
    }
}