using System.Collections;
using Game.Effects.Enums;
using UnityEngine;

namespace Game.Effects
{
    public class Effect : MonoBehaviour
    {
        public EffectType Type;
        public Vector3 SpawnPosition;
        
        [SerializeField] private ParticleSystem _particleSystem;

        public void SetMaterial(Material material)
        {
            _particleSystem.GetComponent<Renderer>().material = material;
        }
        
        private void Awake()
        {
            StartCoroutine(DestroyWithDelay());
            PlaySound();
        }

        private IEnumerator DestroyWithDelay()
        {
            yield return new WaitForSeconds(_particleSystem.main.duration + _particleSystem.main.startLifetime.constant);
            
            Destroy(gameObject);
        }

        protected virtual void PlaySound() {}
    }
}