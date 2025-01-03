using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.MainUI.Store.Pages
{
    public class StorePages : MonoBehaviour
    {
        [SerializeField] private Button _ropesButton;
        [SerializeField] private Button _hooksButton;
        
        [SerializeField] private Sprite _enabledRopesSprite;
        [SerializeField] private Sprite _disabledRopesSprite;
        
        [SerializeField] private Sprite _enabledHooksSprite;
        [SerializeField] private Sprite _disabledHooksSprite;
        
        [SerializeField] private GameObject _ropesWindow;
        [SerializeField] private GameObject _hooksWindow;
        
        private ISoundManager _soundManager;

        [Inject]
        public void Injector(ISoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        private void Awake()
        {
            RopesWindow();
        }

        private void OnEnable()
        {
            _ropesButton.onClick.AddListener(RopesWindow);
            _hooksButton.onClick.AddListener(HooksWindow);
        }

        private void OnDisable()
        {
            _ropesButton.onClick.RemoveListener(RopesWindow);
            _hooksButton.onClick.RemoveListener(HooksWindow);
        }

        private void RopesWindow()
        {
            _soundManager.PlaySfx(Sfxes.Click);
            
            _ropesWindow.SetActive(true);
            _hooksWindow.SetActive(false);

            _ropesButton.image.sprite = _enabledRopesSprite;
            _hooksButton.image.sprite = _disabledHooksSprite;
        }
        
        private void HooksWindow()
        {
            _soundManager.PlaySfx(Sfxes.Click);
            
            _hooksWindow.SetActive(true);
            _ropesWindow.SetActive(false);

            _ropesButton.image.sprite = _disabledRopesSprite;
            _hooksButton.image.sprite = _enabledHooksSprite;
        }
    }
}