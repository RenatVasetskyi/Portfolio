using System;

namespace Code.MainInfrastructure.MainGameService.Interfaces
{
    public interface IUserInformationService
    {
        event Action OnNameChanged;
        string Name { get; }
        void SetName(string name);
        void Load();
    }
}