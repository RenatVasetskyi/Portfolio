using Code.Audio.Enums;
using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Settings
{
    [RequireComponent(typeof(Button))]
    public class RateAppButton : MonoBehaviour
    {
        [SerializeField] private string _appId;

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
            _button.onClick.AddListener(OpenUrl);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenUrl);
        }

        private void OpenUrl()
        {
#if UNITY_IPHONE
            _soundManager.PlaySfx(SfxTypeEnum.Touch);
            Application.OpenURL($"https://apps.apple.com/app/id{_appId}");
#endif
        }
    }
}