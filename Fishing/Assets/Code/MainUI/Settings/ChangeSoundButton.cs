using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using Code.MainUI.Settings.Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.MainUI.Settings
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public class ChangeSoundButton : MonoBehaviour
    {
        [SerializeField] private SountToggleType _toggleType;
        
        [SerializeField] private Sprite _disabledVolumeSprite;
        [SerializeField] private Sprite _enabledVolumeSprite;

        private ISoundManager _soundManager;
        
        private Button _button;
        private Image _image;
        
        [Inject]
        public void Injector(ISoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            SetSpriteByCurrentVolumeState();
            
            _button.onClick.AddListener(ChangeVolumeType);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ChangeVolumeType);
        }

        private void SetSpriteByCurrentVolumeState()
        {
            _image.sprite = _soundManager.GetVolumeStateByType(_toggleType) ? _enabledVolumeSprite : _disabledVolumeSprite;
        }

        private void ChangeVolumeType()
        {
            _soundManager.PlaySfx(Sfxes.Click);
            _soundManager.SetVolume(_toggleType, !_soundManager.GetVolumeStateByType(_toggleType));
            SetSpriteByCurrentVolumeState();
        }
    }
}