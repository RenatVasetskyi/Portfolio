using Code.Gameplay.Candies;
using Code.Gameplay.Candies.Enums;
using Code.Gameplay.Person;
using Code.Gameplay.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MainInfrastructure.MainGameService.Factories.Interfaces
{
    public interface IUIFactory
    {
        GameUI GameUI { get; }
        Canvas CreateMenuUI(Transform parent, Camera camera);
        GameUI CreateGameUI(Transform parent, Camera camera);
        Image CreateCandyImage(Transform parent);
        DoneCandy CreateFullCandy(FullCandyType fullCandyType, Transform parent);
        Customer CreateRandomCustomer(Vector3 at, Transform parent);
    }
}