using InputSystems;
using ObjectPool;
using UnityEngine;

namespace Game
{
    public class Tower : MonoBehaviour
    {
        private const int StartBulletsCount = 10;
        private const float MoveBulletDuration = 0.5f;
        
        [SerializeField] private GameObject _bullet;

        [SerializeField] private Transform _target;

        [SerializeField] private InputSystem _inputSystem;

        private GameObjectPool _bulletPool;
        
        private void Awake()
        {
            _bulletPool = new(_bullet, transform, StartBulletsCount);

            _inputSystem.OnShootPressed += Shoot;
        }

        private void OnDestroy()
        {
            _inputSystem.OnShootPressed -= Shoot;
        }

        private void Shoot()
        {
            GameObject bullet = _bulletPool.Get();

            LeanTween.move(bullet, _target, MoveBulletDuration)
                .setOnComplete(() => _bulletPool.Return(bullet));
        }
    }
}