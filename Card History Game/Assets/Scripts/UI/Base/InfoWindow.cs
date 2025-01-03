using Architecture.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace UI.Base
{
    public class InfoWindow : MonoBehaviour
    {
        [SerializeField] private AnimatedWindow _infoWindow;
        [SerializeField] private string _saveKey;
        
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
                _saveService.SaveBool(_saveKey, false);
                _infoWindow.Open();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private bool IsFirstComeToGame()
        {
            if (_saveService.HasKey(_saveKey))
                return _saveService.LoadBool(_saveKey);

            return true;
        }
    }
}