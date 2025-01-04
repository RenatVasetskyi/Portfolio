using System;
using Architecture.Services.Interfaces;

namespace Architecture.Services
{
    public class XpService : IXpService
    {
        private const string XpSaveId = "Xp";
        private const string LevelSaveId = "Level";
        
        private const int StartXp = 0;
        private const int StartLevel = 0;
        private const int XpToLevelUp = 100;
        
        private readonly ISaveService _saveService;

        public event Action OnXpChanged; 
        public event Action OnLevelChanged;

        public int Xp { get; private set; }
        public int MaxXp => XpToLevelUp;
        public int Level { get; private set; }

        public XpService(ISaveService saveService)
        {
            _saveService = saveService;
        }
        
        public void Add(int count)
        {
            if (count <= 0)
                return;
            
            Xp += count;
            LevelUp();
            
            _saveService.SaveInt(XpSaveId, Xp);
            OnXpChanged?.Invoke();
        }
        
        public void Load()
        {
            Level = _saveService.HasKey(LevelSaveId) ? _saveService.LoadInt(LevelSaveId) : StartLevel;
            Xp = _saveService.HasKey(XpSaveId) ? _saveService.LoadInt(XpSaveId) : StartXp;
        }

        private void LevelUp()
        {
            if (Xp >= XpToLevelUp)
            {
                Xp -= XpToLevelUp;
                Level++;
                _saveService.SaveInt(LevelSaveId, Level);
                OnLevelChanged?.Invoke();
                LevelUp();
            }
        }
    }
}