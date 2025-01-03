using System;

namespace Architecture.Services.Interfaces
{
    public interface IUserDataService
    {
        event Action OnNameChanged;
        string UserName { get; }
        void SetUserName(string name);
        void Load();
    }
}