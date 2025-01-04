using Architecture.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace UI.Base
{
    public class MainMenu : MonoBehaviour
    {
        private const string IsFirstComeToGameSaveId = "IsFirstComeToGame";

        [SerializeField] private AnimatedWindow _infoWindow;
        
        private ISaveService _saveService;
        
        [Inject]
        public void Construct(ISaveService saveService)
        {
            _saveService = saveService;
        }
        
        private void Awake()
        {
            if (IsFirstComeToGame())
            {
                _saveService.SaveBool(IsFirstComeToGameSaveId, false);
                _infoWindow.Open();
            }
            else
            {
                _infoWindow.gameObject.SetActive(false);
            }
        }

        private bool IsFirstComeToGame()
        {
            if (_saveService.HasKey(IsFirstComeToGameSaveId))
                return _saveService.LoadBool(IsFirstComeToGameSaveId);

            return true;
        }
    }
}