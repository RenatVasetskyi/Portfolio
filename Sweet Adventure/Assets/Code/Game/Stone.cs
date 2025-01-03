using System.Collections;
using Code.Game.Interfaces;
using UnityEngine;

namespace Code.Game
{
    public class Stone : MonoBehaviour
    {
        private const int Damage = 1;
        
        private void Awake()
        {
            StartCoroutine(DestroyMyself());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.GetDamage(Damage);
                Destroy(gameObject);
            }
        }

        private IEnumerator DestroyMyself()
        {
            yield return new WaitForSeconds(7f);
            Destroy(gameObject);
        }   
    }
}