using Code.Infrastructure.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Game
{
    public class Platform : MonoBehaviour
    {
        private const int MaxSpawnCandyChance = 100;
        private const int SpawnCandyChance = 70;
        
        [SerializeField] private Transform _min;
        [SerializeField] private Transform _max;

        private ICreator _creator;
        private Data _data;
        
        [Inject]
        public void Initialize(ICreator creator, Data data)
        {
            _data = data;
            _creator = creator;
        }

        private void Awake()
        {
            SpawnCandyOnRandomPosition();
        }

        private void SpawnCandyOnRandomPosition()
        {
            int number = Random.Range(0, MaxSpawnCandyChance);

            if (number < SpawnCandyChance)
                return;
            
            Vector2 position = new Vector2(Random.Range(_min.position.x, _max.position.x), _max.position.y);
            _creator.Do(_data.CandyPrefab, position, Quaternion.identity, transform);
        }
    }
}