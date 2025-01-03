using System.Collections;
using Code.Infrastructure.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Game
{
    public class StoneSpawner : MonoBehaviour
    {
        private const float SpawnOffsetY = 20f;
        
        private readonly GetBounds _getBounds = new();
        
        private const float MinSpawnInterval = 1.5f;
        private const float MaxSpawnInterval = 5f;
        
        [SerializeField] private Transform _player;
        
        private Data _data;
        private ICreator _creator;

        private Vector2 _screenBounds;
        private float _stoneWidth;

        [Inject]
        public void Initialize(Data data, ICreator creator)
        {
            _creator = creator;
            _data = data;
        }

        private void Awake()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                float randomWaitTime = Random.Range(MinSpawnInterval, MaxSpawnInterval);

                yield return new WaitForSeconds(randomWaitTime);
                
                Stone stone = _creator.Do(_data.StonePrefab);
                _getBounds.Get(Camera.main, out _screenBounds, out _stoneWidth, stone.GetComponent<SpriteRenderer>());
                stone.transform.position = new Vector2(Random.Range(-_screenBounds.x + _stoneWidth, _screenBounds
                    .x - _stoneWidth), _player.position.y + SpawnOffsetY);
            }
        }
    }
}