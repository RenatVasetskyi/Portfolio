using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Game
{
    public class PlatformSpawner : MonoBehaviour
    {
        private const int PlatformLimit = 40;
        private const int PlatformIndexToGetInOrderToSpawnNewPlatform = 10;
        
        private const float MinSpawnPlatformDistanceY = 2f;
        private const float MaxSpawnPlatformDistanceY = 3f;
        
        private const float SpawnOffsetY = -2.5f;
        
        private readonly List<GameObject> _platforms = new();
        private readonly GetBounds _getBounds = new();

        [SerializeField] private Transform _player;
        
        private Vector2 _screenBounds;
        private float _platformWidth;
        
        private Data _data;
        private ICreator _creator;

        public List<GameObject> Platforms => _platforms;

        [Inject]
        public void Initialize(Data data, ICreator creator)
        {
            _creator = creator;
            _data = data;
        }

        private void Awake()
        {
            SpawnOnStart();
        }

        private void SpawnOnStart()
        {
            int startCount = PlatformLimit / 2; 
            
            for (int i = 0; i < startCount; i++)
                SpawnPlatform();   

            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitUntil(() => _platforms.Count >= PlatformIndexToGetInOrderToSpawnNewPlatform && _player
                    .position.y > _platforms[PlatformIndexToGetInOrderToSpawnNewPlatform].transform.position.y);
                
                SpawnPlatform();
            }
        }

        private void SpawnPlatform()
        {
            Vector2 lastPlatformPosition;
            
            if (_platforms.Count > 0)
                lastPlatformPosition = _platforms.Last().transform.position;   
            else
                lastPlatformPosition = new Vector2(0, SpawnOffsetY);
            
            Platform createdPlatform = _creator.Do(_data.PlatformPrefab, new Vector2(999, 999), Quaternion.identity, transform);
            float randomDistanceToNextPlatformY = Random.Range(MinSpawnPlatformDistanceY, MaxSpawnPlatformDistanceY);
            
            _getBounds.Get(Camera.main, out _screenBounds, out _platformWidth, createdPlatform.GetComponent<SpriteRenderer>());
            
            if (_platforms.Count > 0)
            {
                createdPlatform.transform.position = new Vector2(Random.Range(-_screenBounds.x + _platformWidth,
                    _screenBounds.x - _platformWidth), lastPlatformPosition.y + randomDistanceToNextPlatformY);
            }
            else
            {
                createdPlatform.transform.position = lastPlatformPosition;
            }

            if (_platforms.Count >= PlatformLimit)
            {
                GameObject firstPlatform = _platforms[0];
                _platforms.Remove(firstPlatform);
                Destroy(firstPlatform);
            }

            _platforms.Add(createdPlatform.gameObject);
        }
    }
}