using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.MainUI.Settings
{
    [RequireComponent(typeof(Button))]
    public class RateAppButton : MonoBehaviour
    {
        [SerializeField] private string _appStoreId;

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
            _button.onClick.AddListener(OpenAppInAppStore);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenAppInAppStore);
        }

        private void OpenAppInAppStore()
        {
#if UNITY_IPHONE
            _soundManager.PlaySfx(Sfxes.Click);
            Application.OpenURL($"https://apps.apple.com/app/id{_appStoreId}");
#endif
        }
    }
}