using Game.Bets.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Bets
{
    public class BetSelector : MonoBehaviour
    {
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _reduceButton;
        [SerializeField] private TextMeshProUGUI _betText;

        private IBetSystem _betSystem;
        private IBetSelectionSystem _betSelection;

        public void Initialize(IBetSystem betSystem)
        {
            _betSystem = betSystem;
            _betSelection = new BetSelectionSystem(betSystem);
            
            UpdateBetText();
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _addButton.onClick.AddListener(_betSelection.AddBetCount);
            _reduceButton.onClick.AddListener(_betSelection.ReduceBetCount);
            _betSystem.OnBetChanged += UpdateBetText;
            _betSystem.OnBetChanged += SetButtonsInteraction;
        }

        private void Unsubscribe()
        {
            _addButton.onClick.RemoveListener(_betSelection.AddBetCount);
            _reduceButton.onClick.RemoveListener(_betSelection.ReduceBetCount);
            _betSystem.OnBetChanged -= UpdateBetText;
            _betSystem.OnBetChanged -= SetButtonsInteraction;
        }

        private void UpdateBetText()
        {
            _betText.text = $"{_betSystem.CurrentBet}$";
        }
        
        private void SetButtonsInteraction()
        {
            _addButton.interactable = _betSystem.CurrentBet < _betSelection.MaxBetCount;
            _reduceButton.interactable = _betSystem.CurrentBet > _betSelection.MinBetCount;
        }
    }
}