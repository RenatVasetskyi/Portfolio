using System;
using Code.MainInfrastructure.MainGameService.Interfaces;

namespace Code.MainInfrastructure.MainGameService
{
    public class UserInformationService : IUserInformationService
    {
        private const string DefaultName = "User";
        private const string SaveKey = "User Name";
        
        private readonly ISaveToPlayerPrefs _saveToPlayerPrefs;

        public event Action OnNameChanged;

        public string Name { get; private set; }

        public UserInformationService(ISaveToPlayerPrefs saveToPlayerPrefs)
        {
            _saveToPlayerPrefs = saveToPlayerPrefs;
        }

        public void SetName(string name)
        {
            if (name == string.Empty)
                name = DefaultName;
            
            Name = name;
            _saveToPlayerPrefs.SetString(SaveKey, name);
            OnNameChanged?.Invoke();
        }

        public void Load()
        {
            Name = _saveToPlayerPrefs.HasKey(SaveKey) ? _saveToPlayerPrefs.GetString(SaveKey) : DefaultName;
        }
    }
}