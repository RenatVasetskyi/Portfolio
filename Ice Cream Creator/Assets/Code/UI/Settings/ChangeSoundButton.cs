using Code.Audio.Enums;
using Code.MainInfrastructure.MainGameService.Interfaces;
using Code.UI.Settings.Enums;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI.Settings
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public class ChangeSoundButton : MonoBehaviour
    {
        [SerializeField] private ChangeSoundButtonType _type;
        
        [SerializeField] private Sprite _enabled;
        [SerializeField] private Sprite _disabled;

        private ISoundManager _soundManager;
        
        private Button _button;
        private Image _image;
        
        [Inject]
        public void InjectServices(ISoundManager soundManager)
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
            SetSpriteToImage();
            
            _button.onClick.AddListener(ChangeSound);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ChangeSound);
        }

        private void SetSpriteToImage()
        {
            _image.sprite = _soundManager.IsVolumeEnabled(_type) ? _enabled : _disabled;
        }

        private void ChangeSound()
        {
            _soundManager.PlaySfx(SfxTypeEnum.Touch);
            _soundManager.SetVolume(_type, !_soundManager.IsVolumeEnabled(_type));
            SetSpriteToImage();
        }
    }
}