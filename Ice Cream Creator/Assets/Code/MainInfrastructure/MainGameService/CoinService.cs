using System;
using Code.MainInfrastructure.MainGameService.Interfaces;

namespace Code.MainInfrastructure.MainGameService
{
    public class CoinService : ICoinService
    {
        private const string SaveKey = "Coins Balance";
        private const int CoinsOnStart = 300;
        
        private readonly ISaveToPlayerPrefs _saveToPlayerPrefs;
        
        public event Action OnChanged;
        
        public int Coins { get; private set; }

        public CoinService(ISaveToPlayerPrefs saveToPlayerPrefs)
        {
            _saveToPlayerPrefs = saveToPlayerPrefs;
        }
        
        public void AddCoins(int count)
        {
            if (count <= 0)
                return;
            
            Coins += count;
            Save();
        }

        public void ReduceCoins(int count)
        {
            if (Coins < count)
                return;
            
            Coins -= count;
            Save();
        }

        public void Load()
        {
            if (!_saveToPlayerPrefs.HasKey(SaveKey))
                SetCoins(CoinsOnStart);
            else
                Coins = _saveToPlayerPrefs.GetInt(SaveKey);
            
            OnChanged?.Invoke();
        }
        
        private void SetCoins(int count)
        {
            if (count <= 0)
                return;
            
            Coins = count;
            Save();
        }

        private void Save()
        {
            _saveToPlayerPrefs.SetInt(SaveKey, Coins);
            OnChanged?.Invoke();
        }
    }
}