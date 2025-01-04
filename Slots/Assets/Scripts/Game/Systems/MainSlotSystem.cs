using System;
using System.Collections;
using System.Collections.Generic;
using Architecture.Services.Interfaces;
using Audio;
using Data;
using Game.Bets.Interfaces;
using Game.Combinations;
using Game.Combinations.Interfaces;
using Game.Interfaces;
using Game.UI.Buttons;
using Game.UI.UIMediator.Enums;
using Upgrade.Enums;
using Random = UnityEngine.Random;

namespace Game.Systems
{
    public class MainSlotSystem : IWinCountReporter, ICoefficientReporter, ISlotSystem
    {
        public event Action<List<PlayedCombination>> OnCoefficient;
        public event Action<int> OnWin;
        public event Action OnStop;
        
        private readonly ICurrencyService _currencyService;
        private readonly IAudioService _audioService;
        private readonly IUpgradeService _upgradeService;
        private readonly IXpService _xpService;
        private readonly GameSettings _gameSettings;
        private readonly IUIMediator _uiMediator;
        private readonly Column[] _columns;
        private readonly List<SlotPosition> _slotPositions;
        private readonly IBetSystem _betSystem;
        private readonly ICombinationDeterminer _combinationDeterminer;
        private readonly MainSlotsData _slotsData;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly SpinButton _spinButton;
        private readonly List<string> _winCounts = new();

        private SlotsGameBoardState _currentState;

        public List<Slot> SlotPack { get; private set; }
        public int CurrentWinCount { get; private set; }

        public MainSlotSystem(ICurrencyService currencyService, IAudioService audioService, 
            IUpgradeService upgradeService, IXpService xpService, GameSettings gameSettings,
            IBetSystem betSystem, ICombinationDeterminer combinationDeterminer,
            IUIMediator uiMediator, Column[] columns, List<SlotPosition> slotPositions, 
            MainSlotsData slotsData, ICoroutineRunner coroutineRunner, SpinButton spinButton)
        {
            _currencyService = currencyService;
            _audioService = audioService;
            _upgradeService = upgradeService;
            _xpService = xpService;
            _gameSettings = gameSettings;
            _betSystem = betSystem;
            _combinationDeterminer = combinationDeterminer;
            _uiMediator = uiMediator;
            _columns = columns;
            _slotPositions = slotPositions;
            _slotsData = slotsData;
            _coroutineRunner = coroutineRunner;
            _spinButton = spinButton;

            ChangeState(SlotsGameBoardState.StopSpin);
            SetRandomSlotPack();
            SetSlotPositionsPosition();

            foreach (Column column in _columns)
                column.Initialize(this);
        }
        
        public void ChangeState(SlotsGameBoardState state)
        {
            _currentState = state;
            _uiMediator.Notify(_currentState);
        }

        public void Spin(float moveSlotDuration)
        {
            if (_currentState != SlotsGameBoardState.StopSpin |
                _currencyService.Coins < _betSystem.CurrentBet)
                return;

            ChangeState(SlotsGameBoardState.Spin);
            _currencyService.Buy(_betSystem.CurrentBet);
            _audioService.PlaySlotSpinSound(_spinButton.BaseSpinSpeed, _spinButton.SpinSpeed);
            _winCounts.Clear();

            ResetWinCount();
            SetRandomSlotPack();
            
            foreach (Column column in _columns)
                column.Spin(moveSlotDuration);
        }

        public void Stop()
        {
            _coroutineRunner.StartCoroutine(StopCoroutine());
        }
        
        public void MultiplyWinCount(float multiplayer)
        {
            if (_winCounts.Count == 0)
                CurrentWinCount = _betSystem.CurrentBet;
            
            int winCount = (int)(CurrentWinCount * multiplayer * _upgradeService.GetUpgradeValue(UpgradeableType.WinCount));
            CurrentWinCount = winCount;
            _winCounts.Add(CurrentWinCount.ToString());
        }

        public void SendWinCount(int count)
        {
            OnWin?.Invoke(count);
        }

        private IEnumerator StopCoroutine()
        {
            foreach (Column column in _columns)
            {
                if (!column.IsStopped)
                    yield break;
            }

            CheckWin();
            
            ChangeState(SlotsGameBoardState.StopSpin);
            OnStop?.Invoke();
        }
        
        private void SetSlotPositionsPosition()
        {
            int currentSlotPosition = 0;

            SlotWinCombinations combinations = (SlotWinCombinations)_slotsData.WinCombinations;

            for (int column = 0; column < combinations.Columns; column++)
            {
                for (int row = 0; row < combinations.Rows; row++)
                {
                    _slotPositions[currentSlotPosition].SetPosition(column, row);

                    currentSlotPosition++;
                }
            }
        }

        private void SetRandomSlotPack()
        {
            SlotPack = _slotsData.SlotPacks[Random.Range(0, _slotsData.SlotPacks.Count)].Slots;
        }

        private void CheckWin()
        {
            List<PlayedCombination> winCombinations = _combinationDeterminer
                .GetPlayedCombinations(_slotPositions);

            ResetWinCount();
            
            if (winCombinations.Count > 0)
            {
                foreach (PlayedCombination winCombination in winCombinations)
                {
                    _xpService.Add(_gameSettings.AddXpForWinCombination);
                    winCombination.WinAction?.Invoke();
                }
                
                _audioService.PlaySfx(SfxType.WinCoins);
                OnWin?.Invoke(CurrentWinCount);
            }

            _currencyService.Earn(CurrentWinCount);
            OnCoefficient?.Invoke(winCombinations);
        }

        private void ResetWinCount()
        {
            CurrentWinCount = 0;
            _winCounts.Clear();
            OnWin?.Invoke(CurrentWinCount);
        }
    }
}