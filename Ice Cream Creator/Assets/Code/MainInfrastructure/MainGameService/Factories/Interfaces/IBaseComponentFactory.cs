using UnityEngine;

namespace Code.MainInfrastructure.MainGameService.Factories.Interfaces
{
    public interface IBaseComponentFactory
    {
        Transform CreateParent();
        Camera CreateCamera();
    }
}