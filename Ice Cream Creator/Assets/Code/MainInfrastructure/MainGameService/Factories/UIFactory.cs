using System.Linq;
using Code.Data;
using Code.Gameplay.Candies;
using Code.Gameplay.Candies.Enums;
using Code.Gameplay.Person;
using Code.Gameplay.UI;
using Code.MainInfrastructure.MainGameService.Factories.Interfaces;
using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.MainInfrastructure.MainGameService.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IPrefabService _prefabService;
        private readonly GameData _gameData;
        
        public GameUI GameUI { get; private set; }
        
        public UIFactory(IInstantiator instantiator, IPrefabService prefabService, GameData gameData)
        {
            _instantiator = instantiator;
            _prefabService = prefabService;
            _gameData = gameData;
        }

        public Canvas CreateMenuUI(Transform parent, Camera camera)
        {
            Canvas menuUI = _instantiator.InstantiatePrefabForComponent<Canvas>(_prefabService
                .LoadPrefabFromResources<Canvas>(ResourcesLoadPath.MenuUI), parent);
            menuUI.worldCamera = camera;
            return menuUI;
        }

        public GameUI CreateGameUI(Transform parent, Camera camera)
        {
            GameUI = _instantiator.InstantiatePrefabForComponent<GameUI>(_prefabService
                .LoadPrefabFromResources<GameUI>(ResourcesLoadPath.GameUI), parent);
            GameUI.GetComponent<Canvas>().worldCamera = camera;
            return GameUI;
        }

        public Image CreateCandyImage(Transform parent)
        {
            Image image = _instantiator.InstantiatePrefabForComponent<Image>(_prefabService
                .LoadPrefabFromResources<Image>(ResourcesLoadPath.CandyImage), parent);
            image.transform.localPosition = Vector3.zero;
            return image;
        }

        public DoneCandy CreateFullCandy(FullCandyType fullCandyType, Transform parent)
        {
            DoneCandy candy = _instantiator.InstantiatePrefabForComponent<DoneCandy>(_prefabService
                .LoadPrefabFromResources<Image>(ResourcesLoadPath.FullCandyImage), parent); 
            Sprite sprite = _gameData.CandyDatas.First(x => x.FullCandyType == fullCandyType).FullCandySprite;
            candy.Image.sprite = sprite;
            candy.Image.SetNativeSize();
            candy.transform.localPosition = Vector3.zero;
            return candy;
        }

        public Customer CreateRandomCustomer(Vector3 at, Transform parent)
        {
            Customer customer = _instantiator.InstantiatePrefabForComponent<Customer>(_gameData
                .Customers[Random.Range(0, _gameData.Customers.Count)], at, Quaternion.identity, parent);
            customer.transform.localPosition = at;
            customer.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            return customer;
        }
    }
}