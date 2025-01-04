using System;
using System.Collections.Generic;
using Architecture.Services.Interfaces;
using Audio;
using Daily.Interfaces;
using UnityEngine;

namespace Daily
{
    public class DailyBonusSystem : IDailyBonusSystem
    {
        private const string LastDaySaveId = "LastDay";
        private const string IsDailyBonusGotSaveId = "IsDailyBonusGot";
        private const string DaysInARowSaveId = "DaysInARow";
        
        private readonly List<int> _bonuses = new() { 50, 100, 150, 200, 250, 300, 500 };
        
        public event Action OnDeactivated;
        public event Action OnActivated;

        private readonly ISaveService _saveService;
        private readonly IAudioService _audioService;
        private readonly ICurrencyService _currencyService;

        private int _lastDay;
        private int _daysInARow;

        private bool _isBonusGot;

        public int CurrentBonus => _bonuses[_daysInARow];

        public DailyBonusSystem(ISaveService saveService, IAudioService audioService, 
            ICurrencyService currencyService)
        {
            _saveService = saveService;
            _audioService = audioService;
            _currencyService = currencyService;
        }
        
        public void Open()
        {
            _isBonusGot = true;
    
            OnDeactivated?.Invoke();
    
            _audioService.PlaySfx(SfxType.UIClick);

            AddDaysInARowByCurrentDate();
            EarnBonus();
            UpdateLastDay();
            Save();
        }
        
        public void Show()
        {
            Load();

            if (CanActivate())
                Activate();
            else
                OnDeactivated?.Invoke();
        }
        
        public void Collect()
        {
            _audioService.PlaySfx(SfxType.GetCoin);
        }

        private void AddDaysInARowByCurrentDate()
        {
            if (DateTime.Now.DayOfYear - 1 == _lastDay & _daysInARow < _bonuses.Count - 1)
                AddDaysInARow();
            else if(DateTime.Now.DayOfYear - 1 != _lastDay)
                ResetDaysInARow();
        }

        private void EarnBonus()
        {
            _currencyService.Earn(_bonuses[_daysInARow]);
        }

        private void UpdateLastDay()
        {
            _lastDay = DateTime.Now.DayOfYear;
        }

        private void Save()
        {
            _saveService.SaveInt(LastDaySaveId, _lastDay);
            _saveService.SaveInt(DaysInARowSaveId, _daysInARow);
            _saveService.SaveBool(IsDailyBonusGotSaveId, _isBonusGot);
        }

        private void Load()
        {
            _lastDay = PlayerPrefs.HasKey(LastDaySaveId) ? _saveService.LoadInt(LastDaySaveId) : DateTime.Now.DayOfYear;

            if (PlayerPrefs.HasKey(IsDailyBonusGotSaveId))
                _isBonusGot = _lastDay == DateTime.Now.DayOfYear && _saveService.LoadBool(IsDailyBonusGotSaveId);
            else
                _isBonusGot = false;

            if (PlayerPrefs.HasKey(DaysInARowSaveId))
                _daysInARow = _saveService.LoadInt(DaysInARowSaveId);
        }

        private void Activate()
        {
            // _audioService.PlaySfx(SfxType.DailyBonusActivated);
            
            OnActivated?.Invoke(); 
        }
        
        private void ResetDaysInARow()
        {
            _daysInARow = 0;
        }

        private void AddDaysInARow()
        {
            _daysInARow++;
        }

        private bool CanActivate()
        {
            return _lastDay != DateTime.Now.DayOfYear ||
                   (_lastDay == DateTime.Now.DayOfYear && _isBonusGot == false);
        }
    }
}
