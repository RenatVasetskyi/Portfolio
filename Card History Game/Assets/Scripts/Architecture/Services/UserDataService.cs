using System;
using Architecture.Services.Interfaces;

namespace Architecture.Services
{
    public class UserDataService : IUserDataService
    {
        private const string SaveNameKey = "UserName";
        private const string DefaultName = "User";
        
        private readonly ISaveService _saveService;

        public event Action OnNameChanged;

        public string UserName { get; private set; }

        public UserDataService(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public void SetUserName(string name)
        {
            UserName = name == string.Empty ? DefaultName : name;

            Save();
            
            OnNameChanged?.Invoke();
        }

        public void Load()
        {
            UserName = _saveService.HasKey(SaveNameKey) ?
                _saveService.LoadString(SaveNameKey) : DefaultName;
        }

        private void Save()
        {
            _saveService.SaveString(SaveNameKey, UserName);
        }
    }
}