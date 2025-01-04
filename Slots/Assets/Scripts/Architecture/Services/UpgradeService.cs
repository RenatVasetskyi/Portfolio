using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Services.Interfaces;
using Data;
using UnityEngine;
using Upgrade;
using Upgrade.Enums;

namespace Architecture.Services
{
    public class UpgradeService : IUpgradeService
    {
        private const int StartUpgradeLevel = 0;
        
        private readonly ISaveService _saveService;
        private readonly GameSettings _gameSettings;
        
        public event Action<UpgradeableType, int> OnUpgradeLevelChanged;

        private Dictionary<UpgradeableType, UpgradeSaveData> _upgrades;

        public UpgradeService(ISaveService saveService, GameSettings gameSettings)
        {
            _saveService = saveService;
            _gameSettings = gameSettings;
        }
        
        public void SetUpgradeLevel(UpgradeableType type, int level)
        {
            if (IsLastUpgradeLevel(type))
                return;
            
            _upgrades[type].Level = level;
            Save(type);
        }
        
        public int GetUpgradeLevel(UpgradeableType type)
        {
            try
            {
                UpgradeSaveData upgradeData = _upgrades.FirstOrDefault
                    (upgrade => upgrade.Key == type).Value;

                return upgradeData.Level;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public bool IsLastUpgradeLevel(UpgradeableType type)
        {
            int upgradeLevels = _gameSettings.UpgradeableStaticData.FirstOrDefault
                (upgrade => upgrade.Key == type).Value.Count;

            return _upgrades[type].Level >= upgradeLevels - 1;
        }
        
        public int GetUpgradePrice(UpgradeableType type)
        {
            List<UpgradeableStaticData> staticData = _gameSettings.UpgradeableStaticData.FirstOrDefault
                (upgrade => upgrade.Key == type).Value;
            
            return staticData[_upgrades[type].Level].UpgradePrice;
        }

        public float GetUpgradeValue(UpgradeableType type)
        {
            List<UpgradeableStaticData> staticData = _gameSettings.UpgradeableStaticData.FirstOrDefault
                (upgrade => upgrade.Key == type).Value;
            
            return staticData[_upgrades[type].Level].Value;
        }

        public void Load()
        {
            _upgrades = new();

            for (int i = 0; i < EnumHelper.GetMaxEnumValue<UpgradeableType>() + 1; i++)
            {
                UpgradeableType type = (UpgradeableType)i;
                
                int upgradeLevel = StartUpgradeLevel;
                
                if (_saveService.HasKey(type.ToString()))
                    upgradeLevel = _saveService.LoadInt(type.ToString());
                
                ConstructUpgradeData(type, upgradeLevel);
            }
        }

        private void Save(UpgradeableType type)
        {
            _saveService.SaveInt(type.ToString(), _upgrades[type].Level);
            OnUpgradeLevelChanged?.Invoke(type, _upgrades[type].Level);
        }

        private void ConstructUpgradeData(UpgradeableType type, int upgradeLevel)
        {
            _upgrades.Add(type, new UpgradeSaveData
            {
                Level = upgradeLevel,
                Value = GetUpgradeValueByLevelAndMultiplayer(type, upgradeLevel),
            });
        }

        private float GetUpgradeValueByLevelAndMultiplayer(UpgradeableType type, int level)
        { 
            return _gameSettings.UpgradeableStaticData.FirstOrDefault
                (upgrade => upgrade.Key == type).Value[level].Value;
        }
    }
}