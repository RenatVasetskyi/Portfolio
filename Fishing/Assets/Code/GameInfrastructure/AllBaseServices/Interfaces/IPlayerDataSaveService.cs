using System;

namespace Code.GameInfrastructure.AllBaseServices.Interfaces
{
    public interface IPlayerDataSaveService
    {
        event Action OnUserNameChanged;
        string UserName { get; }
        void SetUserName(string name);
        void LoadData();
    }
}