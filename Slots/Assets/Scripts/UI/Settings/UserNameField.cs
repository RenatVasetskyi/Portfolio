using System.Collections;
using System.Text.RegularExpressions;
using Architecture.Services.Interfaces;
using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Settings
{
    public class UserNameField : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;

        [SerializeField] private Button _editButton;
        [SerializeField] private Button _saveButton;
        
        [SerializeField] private GameObject _editWindow;

        [SerializeField] private TextMeshProUGUI _currentNameText;
        
        private IUserDataService _userDataService;
        private IAudioService _audioService;
        
        [Inject]
        public void Construct(IUserDataService userDataService, IAudioService audioService)
        {
            _userDataService = userDataService;
            _audioService = audioService;
        }

        private void Awake()
        {
            _inputField.onValidateInput += ValidateInput;
            
            _editWindow.SetActive(false);
        }

        private void OnEnable()
        {
            _saveButton.onClick.AddListener(SaveName);
            _editButton.onClick.AddListener(OpenEditNameWindow);
        }

        private void OnDisable()
        {
            _saveButton.onClick.RemoveListener(SaveName);
            _editButton.onClick.RemoveListener(OpenEditNameWindow);
        }

        private void OnDestroy()
        { 
            _inputField.onValidateInput -= ValidateInput;
        }

        private char ValidateInput(string input, int charIndex, char addedChar)
        {
            if (Regex.IsMatch(addedChar.ToString(), "[^a-zA-Z0-9]"))
                addedChar = '\0';

            return addedChar;
        }

        private void SaveName()
        {
            _audioService.PlaySfx(SfxType.UIClick);
            
            _userDataService.SetUserName(_inputField.text);
            
            _saveButton.gameObject.SetActive(false);
            
            _editWindow.SetActive(false);
        }

        private void OpenEditNameWindow()
        {
            _audioService.PlaySfx(SfxType.UIClick);
            
            SetCurrentName();
            
            _saveButton.gameObject.SetActive(true);
            
            _editWindow.SetActive(true);

            _currentNameText.text = string.Empty;
            
            StartCoroutine(ActivateInput());
        }

        private IEnumerator ActivateInput()
        {
            yield return null;
            
            _inputField.ActivateInputField();
        }

        private void SetCurrentName()
        {
            _inputField.text = _userDataService.UserName;
        }
    }
}