using Code.Infrastructure.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private MenuView _menuView;
        
        private MenuModel _menuModel;
        
        private IGameSoundPlayer _gameSoundPlayer;
        private ISceneController _sceneController;

        [Inject]
        public void Initialize(IGameSoundPlayer gameSoundPlayer, ISceneController sceneController)
        {
            _sceneController = sceneController;
            _gameSoundPlayer = gameSoundPlayer;
        }
        
        private void Awake()
        {
            _menuModel = new(_gameSoundPlayer, _sceneController);
        }

        private void OnEnable()
        {
            _menuView.PlayButton.onClick.AddListener(_menuModel.Play);
            _menuView.OpenShopWindowButton.onClick.AddListener(() => _menuModel.OpenShopWindow(_menuView.ShopWindow));
            _menuView.HideShopWindowButton.onClick.AddListener(() => _menuModel.HideShopWindow(_menuView.ShopWindow));
            _menuView.SoundButton.onClick.AddListener(() => _menuModel.ChangeSound
                (_menuView.SoundButton.image, _menuView.SoundToggleEnabled, _menuView.SoundToggleDisabled));
            _menuView.SoundButton.image.sprite = _gameSoundPlayer.GetToggleState() ? _menuView.SoundToggleEnabled : _menuView.SoundToggleDisabled;
        }

        private void OnDisable()
        {
            _menuView.PlayButton.onClick.RemoveAllListeners();
            _menuView.OpenShopWindowButton.onClick.RemoveAllListeners();
            _menuView.HideShopWindowButton.onClick.RemoveAllListeners();
            _menuView.SoundButton.onClick.RemoveAllListeners();
        }
    }
}