using Architecture.Services.Interfaces;
using Architecture.States.Interfaces;
using Data;
using Games.Stories;
using Games.Stories.Interfaces;
using Games.Stories.UI;
using UnityEngine;

namespace Architecture.States
{
    public class LoadStoriesGameState : IState
    {
        private const string GameScene = "Game";
        
        private readonly ISceneLoader _sceneLoader;
        private readonly IGamePauser _gamePauser;
        private readonly IAudioService _audioService;
        private readonly IBaseFactory _baseFactory;
        private readonly IMelodyService _melodyService;
        private IStoryProgressService _storyProgressService;

        public LoadStoriesGameState(ISceneLoader sceneLoader, IGamePauser gamePauser,
            IAudioService audioService, IBaseFactory baseFactory, 
            IMelodyService melodyService, IStoryProgressService storyProgressService)
        {
            _storyProgressService = storyProgressService;
            _sceneLoader = sceneLoader;
            _gamePauser = gamePauser;
            _audioService = audioService;
            _baseFactory = baseFactory;
            _melodyService = melodyService;
        }
        
        public void Exit()
        {
            _audioService.StopMusic();
        }

        public void Enter()
        {
            _sceneLoader.Load(GameScene, Initialize);
        }

        private void Initialize()
        {
            _gamePauser.Clear();
            _gamePauser.SetPause(false);
            
            Transform parent = _baseFactory.CreateBaseWithObject<Transform>(AssetPath.BaseParent);
            Camera camera = _baseFactory.CreateBaseWithContainer<Camera>(AssetPath.BaseCamera, parent);
            
            StoryGameView gameView = _baseFactory.CreateBaseWithContainer<StoryGameView>(AssetPath.StoriesGameView, parent);
            gameView.GetComponent<Canvas>().worldCamera = camera;

            IStoryGameController gameController = new StoryGameController(gameView.CardsWithPieceOfStory,
                gameView.SlotsForCardWithPieceOfStories, gameView.CompletePercentageDisplayer, _storyProgressService);

            gameView.Initialize(gameController);

            _audioService.PlayMusic(_melodyService.SelectedGameMelody.MusicType);
        }
    }
}