using Architecture.Services.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Settings
{
    public class UserNameDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _userNameText;
        
        private IUserDataService _userDataService;
        
        [Inject]
        public void Construct(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        private void Awake()
        {
            UpdateNameText();
            
            _userDataService.OnNameChanged += UpdateNameText;
        }

        private void OnDestroy()
        {
            _userDataService.OnNameChanged -= UpdateNameText;   
        }

        private void UpdateNameText()
        {
            _userNameText.text = _userDataService.UserName;
        }
    }
}