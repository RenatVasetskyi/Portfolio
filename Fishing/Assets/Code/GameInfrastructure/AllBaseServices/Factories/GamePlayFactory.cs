using Code.GameInfrastructure.AllBaseServices.Factories.Interfaces;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using Code.Gaming;
using Code.Gaming.Camera;
using Code.RootGameData;
using UnityEngine;
using Zenject;

namespace Code.GameInfrastructure.AllBaseServices.Factories
{
    public class GamePlayFactory : IGamePlayFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IPrefabLoader _prefabLoader;
        private readonly GameDataWrapper _gameDataWrapper;
        private readonly IStoreService _storeService;

        public GamePlayFactory(IInstantiator instantiator, IPrefabLoader prefabLoader,
            GameDataWrapper gameDataWrapper, IStoreService storeService)
        {
            _gameDataWrapper = gameDataWrapper;
            _storeService = storeService;
            _instantiator = instantiator;
            _prefabLoader = prefabLoader;
        }

        public Canvas SpawnMainMenuView(Transform parent, Camera camera)
        {
            Canvas mainMenuView = _instantiator.InstantiatePrefabForComponent<Canvas>(_prefabLoader
                .Load<Canvas>(PrefabPath.MainMenuView), parent);
            mainMenuView.worldCamera = camera;
            return mainMenuView;
        }

        public GameController SpawnGameplay(Transform parent, Camera camera)
        {
            GameController gamePlay = _instantiator.InstantiatePrefabForComponent<GameController>(_prefabLoader
                .Load<GameController>(PrefabPath.Gameplay), parent);
            gamePlay.Initialize(camera);
            return gamePlay;
        }

        public FollowCamera SpawnGamingCamera(Transform parent)
        {
            return _instantiator.InstantiatePrefabForComponent<FollowCamera>(_prefabLoader
                .Load<FollowCamera>(PrefabPath.GamingCamera), parent);
        }

        public Fish SpawnRandomFish(Transform parent, Transform from, Transform to)
        {
            Fish fish = _instantiator.InstantiatePrefabForComponent<Fish>(_gameDataWrapper.Fishes
                [Random.Range(0, _gameDataWrapper.Fishes.Length)], parent);

            fish.transform.position = from.position;
            fish.transform.rotation = from.rotation;
            fish.Move(to);
            return fish;
        }

        public Hook SpawnHook(Vector3 at, Transform parent)
        {
            return _instantiator.InstantiatePrefabForComponent<Hook>(_storeService.SelectedHook.Hook, at, Quaternion.identity, parent);
        }
    }
}