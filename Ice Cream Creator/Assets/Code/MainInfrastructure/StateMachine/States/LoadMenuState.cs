using Code.Audio.Enums;
using Code.Data;
using Code.MainInfrastructure.MainGameService.Factories.Interfaces;
using Code.MainInfrastructure.MainGameService.Interfaces;
using Code.MainInfrastructure.StateMachine.States.Interfaces;
using UnityEngine;

namespace Code.MainInfrastructure.StateMachine.States
{
    public class LoadMenuState : IState
    {
        private readonly IAsyncSceneLoader _asyncSceneLoader;
        private readonly ISoundManager _soundManager;
        private readonly IUIFactory _uiFactory;
        private readonly IBaseComponentFactory _baseComponentFactory;

        public LoadMenuState(IAsyncSceneLoader asyncSceneLoader, ISoundManager soundManager, 
            IUIFactory uiFactory, IBaseComponentFactory baseComponentFactory)
        {
            _asyncSceneLoader = asyncSceneLoader;
            _soundManager = soundManager;
            _uiFactory = uiFactory;
            _baseComponentFactory = baseComponentFactory;
        }
        
        public void Enter()
        {
            _soundManager.PlayMusic(MusicTypeEnum.Menu);
            _asyncSceneLoader.LoadAsync(Scenes.Menu, CreateMenuUI);
        }

        public void Exit()
        {
        }

        private void CreateMenuUI()
        {
            Transform parent = _baseComponentFactory.CreateParent();
            Camera camera = _baseComponentFactory.CreateCamera();

            Canvas menuUI = _uiFactory.CreateMenuUI(parent, camera);
        }
    }
}