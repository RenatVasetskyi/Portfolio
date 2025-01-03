using Code.MainInfrastructure.MainGameService.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI.Other
{
    public class UserName : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private IUserInformationService _userInformationService;

        [Inject]
        public void InjectServices(IUserInformationService userInformationService)
        {
            _userInformationService = userInformationService;
        }

        private void Awake()
        {
            _userInformationService.OnNameChanged += UpdateText;
        }

        private void OnEnable()
        {
            UpdateText();
        }

        private void OnDestroy()
        {
            _userInformationService.OnNameChanged -= UpdateText;
        }

        private void UpdateText()
        {
            _text.text = _userInformationService.Name;
        }
    }
}