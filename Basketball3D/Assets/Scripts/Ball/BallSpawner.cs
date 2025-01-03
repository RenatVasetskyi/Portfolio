using System.Collections;
using Assets.Scripts.Architecture.Services.Factories;
using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Ball
{
    public class BallSpawner : IBallSpawner
    {
        private readonly IMainFactory _mainFactory;
        private readonly ICoroutineRunner _coroutineRunner;

        private Vector3 _spawnPoint = new(0, 0.75f, 1.2f);

        private float _spawnDelay = 0.5f;
        private float _destroyDelay = 5f;

        private BallMovement _ballMovement;
        private Coroutine _ballSpawning;

        private Transform _ballParent;

        public BallSpawner(IMainFactory mainFactory, ICoroutineRunner coroutineRunner)
        {
            _mainFactory = mainFactory;
            _coroutineRunner = coroutineRunner;
        }

        public void StartSpawning()
        {
            _ballParent = _mainFactory.CreateDefault(AssetPath.BallParent);
            _ballSpawning = _coroutineRunner.StartCoroutine(SpawnBall());
        }

        public void StopSpawning()
        {
            Object.Destroy(_ballParent.gameObject);
            _coroutineRunner.StopCoroutine(_ballSpawning);
        }

        private IEnumerator SpawnBall()
        {
            yield return new WaitForSeconds(_spawnDelay);

            _ballMovement = _mainFactory.CreateBaseGameObject<BallMovement>(AssetPath.Ball, _spawnPoint, _ballParent);
            _ballMovement.OnThrown += StartSpawning;
            _ballMovement.OnThrown += StartDestroying;
        }

        private IEnumerator DestroyTheBall(GameObject ball)
        {
            yield return new WaitForSeconds(_destroyDelay);
            Object.Destroy(ball);
        }

        private void StartDestroying() =>
            _coroutineRunner.StartCoroutine(DestroyTheBall(_ballMovement.gameObject));
    }
}