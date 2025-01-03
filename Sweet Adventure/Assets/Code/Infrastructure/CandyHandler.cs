using System;
using Code.Infrastructure.Interfaces;

namespace Code.Infrastructure
{
    public class CandyHandler : ICandyHandler
    {
        private const string CandiesCountSaveId = "Candies Count";
        private const int StartCandies = 100;
        
        private readonly IPlayerPrefsController _playerPrefsController;
        
        public event Action OnCandiesChanged;
        
        public int Candies { get; private set; }
        
        public CandyHandler(IPlayerPrefsController playerPrefsController)
        {
            _playerPrefsController = playerPrefsController;
        }
        
        public void IncreaseCandies(int candies)
        {
            if (candies > 0)
            {
                Candies += candies;
                SaveData();
                OnCandiesChanged?.Invoke();
            }
        }

        public void ReduceCandies(int candies)
        {
            if (candies > 0)
            {
                Candies -= candies;
                SaveData();
                OnCandiesChanged?.Invoke();
            }
        }

        public void LoadCandiesFromSaves()
        {
            Candies = _playerPrefsController.Path(CandiesCountSaveId) ? _playerPrefsController.Int(CandiesCountSaveId) : StartCandies;
        }

        private void SaveData()
        {
            _playerPrefsController.Int(CandiesCountSaveId, Candies);
        }
    }
}