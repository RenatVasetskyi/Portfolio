using Code.GameInfrastructure.AllBaseServices.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.MainUI.Main
{
    public class PlayerNameDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerNameText;
        
        private IPlayerDataSaveService _playerDataSaveService;

        [Inject]
        public void Injector(IPlayerDataSaveService playerDataSaveService)
        {
            _playerDataSaveService = playerDataSaveService;
        }

        private void Awake()
        {
            _playerDataSaveService.OnUserNameChanged += DisplaySelectedName;
        }

        private void OnEnable()
        {
            DisplaySelectedName();
        }

        private void OnDestroy()
        {
            _playerDataSaveService.OnUserNameChanged -= DisplaySelectedName;
        }

        private void DisplaySelectedName()
        {
            _playerNameText.text = _playerDataSaveService.UserName;
        }
    }
}