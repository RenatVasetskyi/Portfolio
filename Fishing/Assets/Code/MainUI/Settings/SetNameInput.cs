using System.Collections;
using System.Text.RegularExpressions;
using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.MainUI.Settings
{
    [RequireComponent(typeof(TMP_InputField))]
    public class SetNameInput : MonoBehaviour
    {
        [SerializeField] private Button _saveButton;
        [SerializeField] private GameObject _editNameWindow;
        
        private TMP_InputField _inputField;
        
        private IPlayerDataSaveService _playerDataSaveService;
        private ISoundManager _soundManager;
        
        [Inject]
        public void Injector(IPlayerDataSaveService playerDataSaveService, ISoundManager soundManager)
        {
            _playerDataSaveService = playerDataSaveService;
            _soundManager = soundManager;
        }

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _inputField.onValidateInput += ValidateInput;
        }

        private void OnEnable()
        {
            SetCurrentName();
            
            _saveButton.onClick.AddListener(SetName);
            StartCoroutine(ActivateInput());
        }

        private void OnDisable()
        {
            _saveButton.onClick.RemoveListener(SetName);
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

        private void SetName()
        {
            _soundManager.PlaySfx(Sfxes.Click);
            _playerDataSaveService.SetUserName(_inputField.text);
            _editNameWindow.SetActive(false);
        }

        private IEnumerator ActivateInput()
        {
            yield return null;
            
            _inputField.ActivateInputField();
        }

        private void SetCurrentName()
        {
            _inputField.text = _playerDataSaveService.UserName;
        }
    }
}