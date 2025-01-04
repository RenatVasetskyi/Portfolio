using System.Collections.Generic;
using Architecture.Services.Interfaces;
using Game.Bets.Interfaces;
using Game.Interfaces;
using Game.UI.UIMediator.Enums;
using UnityEngine.UI;

namespace Game.UI.UIMediator
{
    public class SlotsUIMediator : IUIMediator
    {
        private readonly List<Selectable> _buttons;
        private readonly ICurrencyService _currencyService;
        private readonly IBetSystem _betSystem;
        private readonly Button _spinButton;

        public SlotsUIMediator(List<Selectable> buttons, ICurrencyService currencyService, 
            IBetSystem betSystem, Button spinButton)
        {
            _buttons = buttons;
            _currencyService = currencyService;
            _betSystem = betSystem;
            _spinButton = spinButton;

            _betSystem.OnBetChanged += SetSpinButtonsInteractionByCurrentBet;
        }

        public void Notify<T>(T state)
        {
            switch (state)
            {
                case SlotsGameBoardState.Spin:
                    SetButtonsInteractionState(false);
                    break;
                case SlotsGameBoardState.StopSpin:
                    SetButtonsInteractionState(true);
                    break;
            }
        }
        
        protected virtual void SetSpinButtonsInteractionByCurrentBet()
        {
            bool isCoinsEnoughToEnableInteraction = IsCoinsEnoughToEnableInteraction();

            _spinButton.interactable = isCoinsEnoughToEnableInteraction;
        }
        
        private bool IsCoinsEnoughToEnableInteraction()
        {
            return _currencyService.Coins >= _betSystem.CurrentBet;
        }

        private void SetButtonsInteractionState(bool interaction)
        {
            foreach (Selectable button in _buttons)
                button.interactable = interaction;
            
            if (!IsCoinsEnoughToEnableInteraction())
                _spinButton.interactable = false;
        }
    }
}