using System.Collections.Generic;
using Architecture.Services.Interfaces;
using Data;
using Game.Bets;
using Game.Bets.Interfaces;
using Game.Combinations;
using Game.Combinations.Interfaces;
using Game.Interfaces;
using Game.Systems;
using Game.UI;
using Game.UI.Buttons;
using Game.UI.UIMediator;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class MainSlotSystemUI : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private List<SlotPosition> _slotPositions;
        [SerializeField] private Column[] _columns;
        [SerializeField] private MainSlotsData _slotsData;
        
        [SerializeField] protected List<Selectable> _selectableElements;
        [SerializeField] private BetSelector _betSelector;
        [SerializeField] protected SpinButton _spinButton;
        [SerializeField] private WinCountDisplayer _winCountDisplayer;

        private ICurrencyService _currencyService;
        private IAudioService _audioService;
        private IUpgradeService _upgradeService;
        private IXpService _xpService;
        private GameSettings _gameSettings;
        private IBetSystem _betSystem;
        private IUIMediator _uiMediator;
        private ICombinationDeterminer _combinationDeterminer;
        private MainSlotSystem _slotSystem;

        [Inject]
        public void Construct(ICurrencyService currencyService, IAudioService audioService, 
            IUpgradeService upgradeService, IXpService xpService, GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _xpService = xpService;
            _upgradeService = upgradeService;
            _currencyService = currencyService;
            _audioService = audioService;
        }

        private void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _betSystem = new BetSystem();
            _combinationDeterminer = new CombinationDeterminer(_slotsData.WinCombinations as SlotWinCombinations);
            _uiMediator = new SlotsUIMediator(_selectableElements, _currencyService,
                _betSystem, _spinButton.GetComponent<Button>());
            _slotSystem = new MainSlotSystem(_currencyService, _audioService, _upgradeService, _xpService, _gameSettings,
                _betSystem, _combinationDeterminer, _uiMediator, _columns, _slotPositions, _slotsData, this, _spinButton);
            
            _betSelector.Initialize(_betSystem);
            _winCountDisplayer.Initialize(_slotSystem);
            _spinButton.Initialize(_slotSystem);
        }
    }
}