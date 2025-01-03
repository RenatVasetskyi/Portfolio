using System;
using Code.GameInfrastructure.AllBaseServices.Interfaces;

namespace Code.GameInfrastructure.AllBaseServices
{
    public class PlayerDataSaveService : IPlayerDataSaveService
    {
        private const string DefaultName = "User";
        private const string SaveKey = "User Name";
        
        private readonly IPlayerPrefsFunctiousWrapper _playerPrefsFunctiousWrapper;

        public event Action OnUserNameChanged;

        public string UserName { get; private set; }

        public PlayerDataSaveService(IPlayerPrefsFunctiousWrapper playerPrefsFunctiousWrapper)
        {
            _playerPrefsFunctiousWrapper = playerPrefsFunctiousWrapper;
        }

        public void SetUserName(string name)
        {
            if (name == string.Empty)
                name = DefaultName;
            
            UserName = name;
            _playerPrefsFunctiousWrapper.SetString(SaveKey, name);
            OnUserNameChanged?.Invoke();
        }

        public void LoadData()
        {
            UserName = _playerPrefsFunctiousWrapper.HasKey(SaveKey) ? _playerPrefsFunctiousWrapper.GetString(SaveKey) : DefaultName;
        }
    }
}