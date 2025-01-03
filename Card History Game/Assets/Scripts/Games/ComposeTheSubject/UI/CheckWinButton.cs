using Data;
using Games.ComposeTheSubject.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Games.ComposeTheSubject.UI
{
    public class CheckWinButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;
        
        private IComposeTheSubjectGameController _gameController;
        private GameSettings _gameSettings;

        [Inject]
        public void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        
        public void Initialize(IComposeTheSubjectGameController gameController)
        {
            _gameController = gameController;
            UpdateTriesCountText(_gameSettings.CheckTriesCount);
            Subscribe();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(CheckWin);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(CheckWin);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void CheckWin()
        {
            _gameController.CheckWinInQuestion();
        }

        private void UpdateTriesCountText(int count)
        {
            _text.text = $"{count} att";
            _button.interactable = count > 0;
        }
        
        private void Subscribe()
        {
            _gameController.OnTriesCountChanged += UpdateTriesCountText;
        }

        private void Unsubscribe()
        {
            if (_gameController != null) 
                _gameController.OnTriesCountChanged -= UpdateTriesCountText;
        }
    }
}