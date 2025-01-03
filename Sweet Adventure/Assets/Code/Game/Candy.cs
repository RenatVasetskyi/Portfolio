using Code.Game.PlayerLogic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Candy : MonoBehaviour
    {
        private Data _data;

        [Inject]
        public void Initialize(Data data)
        {
            _data = data;
        }

        private void Awake()
        {
            GetComponent<SpriteRenderer>().sprite = _data.Candies[Random.Range(0, _data.Candies.Count)];
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.AddCandyCount(1);
                Destroy(gameObject);
            }
        }
    }
}