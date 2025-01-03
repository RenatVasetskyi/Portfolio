using Strategy.Example2.Damages;

namespace Strategy.Example2.Guns
{
    public class Pistol : BaseWeapon
    {
        public Pistol()
        {
            _damage = 10;
            AddDamageType(new FireDamage());
            AddDamageType(new ShrapnelDamage());
        }
    }
}