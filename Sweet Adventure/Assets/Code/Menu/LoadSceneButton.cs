using Code.Infrastructure.Interfaces;
using Code.Music;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Menu
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        
        private ISceneController _sceneController;
        private IGameSoundPlayer _gameSoundPlayer;
        
        private Button _button;
        private bool _isLocked;

        [Inject]
        public void Initialize(ISceneController sceneController, IGameSoundPlayer gameSoundPlayer)
        {
            _gameSoundPlayer = gameSoundPlayer;
            _sceneController = sceneController;
        }

        private void Awake()
        { 
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Load);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Load);
        }

        private void Load()
        {
            if (_isLocked)
                return;

            _isLocked = true;
            _sceneController.Load(_sceneName);
            _gameSoundPlayer.Play(ShortSfx.Click);
        }
    }
}