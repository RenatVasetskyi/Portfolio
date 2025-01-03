using Code.Gaming;
using Code.Gaming.Camera;
using UnityEngine;

namespace Code.GameInfrastructure.AllBaseServices.Factories.Interfaces
{
    public interface IGamePlayFactory
    {
        Canvas SpawnMainMenuView(Transform parent, Camera camera);
        GameController SpawnGameplay(Transform parent, Camera camera);
        FollowCamera SpawnGamingCamera(Transform parent);
        Fish SpawnRandomFish(Transform parent, Transform from, Transform to);
        Hook SpawnHook(Vector3 at, Transform parent);
    }
}