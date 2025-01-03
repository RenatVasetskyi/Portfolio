using System;
using Code.GameInfrastructure.AllBaseServices.Interfaces;

namespace Code.GameInfrastructure.AllBaseServices
{
    public class CoinService : ICoinService
    {
        private const string SaveKey = "Coins Balance";
        private const int CoinsOnStart = 300;
        
        private readonly IPlayerPrefsFunctiousWrapper _playerPrefsFunctiousWrapper;
        
        public event Action OnCountChanged;
        
        public int CoinsCount { get; private set; }

        public CoinService(IPlayerPrefsFunctiousWrapper playerPrefsFunctiousWrapper)
        {
            _playerPrefsFunctiousWrapper = playerPrefsFunctiousWrapper;
        }
        
        public void GiveCoins(int count)
        {
            if (count <= 0)
                return;
            
            CoinsCount += count;
            Save();
        }

        public void TakeCoins(int count)
        {
            if (CoinsCount < count)
                return;
            
            CoinsCount -= count;
            Save();
        }

        public void LoadData()
        {
            if (!_playerPrefsFunctiousWrapper.HasKey(SaveKey))
                SetCoins(CoinsOnStart);
            else
                CoinsCount = _playerPrefsFunctiousWrapper.GetInt(SaveKey);
            
            OnCountChanged?.Invoke();
        }
        
        private void SetCoins(int count)
        {
            if (count <= 0)
                return;
            
            CoinsCount = count;
            Save();
        }

        private void Save()
        {
            _playerPrefsFunctiousWrapper.SetInt(SaveKey, CoinsCount);
            OnCountChanged?.Invoke();
        }
    }
}