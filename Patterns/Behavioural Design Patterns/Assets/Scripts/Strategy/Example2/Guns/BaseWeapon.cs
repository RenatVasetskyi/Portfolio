using System.Collections.Generic;
using Strategy.Example2.Interfaces;
using UnityEngine;

namespace Strategy.Example2.Guns
{
    public class BaseWeapon
    {
        protected int _damage;
        private List<IDoDamage> _doDamage = new();

        public void TryDoDamage()
        {
            foreach (IDoDamage doDamage in _doDamage)
            {
                doDamage?.DoDamage(_damage);
                
                Debug.Log($"Damage was made by {doDamage.GetType()}");
            }
        }

        protected void AddDamageType(IDoDamage doDamage)
        {
            _doDamage.Add(doDamage);
        }

        protected void RemoveDamageType(IDoDamage doDamage)
        {
            _doDamage.Remove(doDamage);
        }

        protected void CleanUp()
        {
            _doDamage.Clear();
        }
    }
}