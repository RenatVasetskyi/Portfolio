using Code.Audio.Enums;
using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Other
{
    [RequireComponent(typeof(Button))]
    public class EnableDisableWindowButton : MonoBehaviour
    {
        [SerializeField] private GameObject[] _windowsToEnable;
        [SerializeField] private GameObject[] _windowsToDisable;
        
        private ISoundManager _soundManager;
        private Button _button;

        [Inject]
        public void InjectServices(ISoundManager soundManager)
        {
            _soundManager = soundManager;
        }
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(EnableDisable);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(EnableDisable);
        }

        private void EnableDisable()
        {
            _soundManager.PlaySfx(SfxTypeEnum.Touch);
            
            foreach (GameObject windowToEnable in _windowsToEnable)
                windowToEnable.SetActive(true);
            
            foreach (GameObject windowToDisable in _windowsToDisable)
                windowToDisable.SetActive(false);
        }
    }
}