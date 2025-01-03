using System.Linq;
using Architecture.Services.Interfaces;
using Data;
using Games.Stories.Data;
using Games.Stories.Enums;

namespace Architecture.Services
{
    public class StoryProgressService : IStoryProgressService
    {
        private const string CurrentLevelToPassSaveId = "CurrentLevelToPass";
        private const string IsGamePassedSaveId = "IsGamePassed";

        private readonly ISaveService _saveService;
        private readonly GameSettings _gameSettings;

        private StoryData _storyToPass;
        
        public StoryData SelectedStory { get; private set; }

        public StoryProgressService(ISaveService saveService, GameSettings gameSettings)
        {
            _saveService = saveService;
            _gameSettings = gameSettings;
        }
        
        public void SelectStory(StoryType type)
        {
            SelectedStory = _gameSettings.Stories.FirstOrDefault(level => level.Type == type);
        }

        public void SetNextStoryToPass()
        {
            int currentLevelToPass = (int)GetCurrentStoryToPass().Type;

            if (currentLevelToPass < _gameSettings.Stories.Count - 1 & 
                currentLevelToPass == (int)SelectedStory.Type)
            {
                _storyToPass = _gameSettings.Stories[currentLevelToPass + 1];
                
                Save();
            }
            else
            {
                _saveService.SaveBool(IsGamePassedSaveId, true);
            }
        }

        public StoryData GetCurrentStoryToPass()
        {
            return _gameSettings.Stories.FirstOrDefault(level => level.Type.ToString() == _storyToPass.Type.ToString());
        }

        public void Load()
        {
            _storyToPass = _saveService.HasKey(CurrentLevelToPassSaveId) ?  _gameSettings.Stories.FirstOrDefault
                (level => level.Type.ToString() == _saveService.LoadString(CurrentLevelToPassSaveId))
                : _gameSettings.Stories.FirstOrDefault();
        }

        public void SelectNextStory()
        {
            int nextLevel = (int)SelectedStory.Type + 1;
            
            if (nextLevel < _gameSettings.Stories.Count)
                SelectedStory = _gameSettings.Stories[nextLevel];   
        }

        public bool CanSelectNextStory()
        {
            return (int)SelectedStory.Type < _gameSettings.Stories.Count - 1;
        }

        public bool IsGamePassed()
        {
            return _saveService.HasKey(IsGamePassedSaveId) && _saveService.LoadBool(IsGamePassedSaveId);
        }

        private void Save()
        {
            _saveService.SaveString(CurrentLevelToPassSaveId, _storyToPass.Type.ToString());
        }
    }
}