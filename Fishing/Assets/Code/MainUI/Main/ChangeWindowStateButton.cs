using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.MainUI.Main
{
    [RequireComponent(typeof(Button))]
    public class ChangeWindowStateButton : MonoBehaviour
    {
        [SerializeField] private GameObject[] _enablingWindows;
        [SerializeField] private GameObject[] _disablingWindows;
        
        private ISoundManager _soundManager;
        private Button _button;

        [Inject]
        public void Injector(ISoundManager soundManager)
        {
            _soundManager = soundManager;
        }
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Change);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Change);
        }

        private void Change()
        {
            _soundManager.PlaySfx(Sfxes.Click);
            
            foreach (GameObject windowToEnable in _enablingWindows)
                windowToEnable.SetActive(true);
            
            foreach (GameObject windowToDisable in _disablingWindows)
                windowToDisable.SetActive(false);
        }
    }
}