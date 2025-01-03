using Code.Audio.Enums;
using Code.Data;
using Code.Gameplay.UI;
using Code.MainInfrastructure.MainGameService.Factories.Interfaces;
using Code.MainInfrastructure.MainGameService.Interfaces;
using Code.MainInfrastructure.StateMachine.States.Interfaces;
using UnityEngine;

namespace Code.MainInfrastructure.StateMachine.States
{
    public class LoadGameplayState : IState 
    {
        private readonly IAsyncSceneLoader _asyncSceneLoader;
        private readonly ISoundManager _soundManager;
        private readonly IUIFactory _uiFactory;
        private readonly IBaseComponentFactory _baseComponentFactory;

        public LoadGameplayState(IAsyncSceneLoader asyncSceneLoader, ISoundManager soundManager, 
            IUIFactory uiFactory, IBaseComponentFactory baseComponentFactory)
        {
            _asyncSceneLoader = asyncSceneLoader;
            _soundManager = soundManager;
            _uiFactory = uiFactory;
            _baseComponentFactory = baseComponentFactory;
        }
        
        public void Enter()
        {
            _soundManager.PlayMusic(MusicTypeEnum.Gameplay);
            _asyncSceneLoader.LoadAsync(Scenes.Gameplay, CreateGameUI);
        }

        public void Exit()
        {
        }

        private void CreateGameUI()
        {
            Transform parent = _baseComponentFactory.CreateParent();
            Camera camera = _baseComponentFactory.CreateCamera();

            GameUI gameUI = _uiFactory.CreateGameUI(parent, camera);
        }
    }
}