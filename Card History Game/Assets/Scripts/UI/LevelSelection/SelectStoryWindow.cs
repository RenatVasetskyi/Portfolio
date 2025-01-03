using Architecture.Services.Interfaces;
using UnityEngine;
using Zenject;

namespace UI.LevelSelection
{
    public class SelectStoryWindow : MonoBehaviour
    {
        [SerializeField] private LoadStoryButton[] _loadStoryButtons;

        private IStoryProgressService _storyService;
        
        [Inject]
        public void Construct(IStoryProgressService storyService)
        {
            _storyService = storyService;
        }
        
        private void OnEnable()
        {
            SetButtonsState();
        }

        private void SetButtonsState()
        {
            int currentLevelToPass = (int)_storyService.GetCurrentStoryToPass().Type;
            
            for (int i = 0; i <= currentLevelToPass; i++)
                _loadStoryButtons[i].Enable();

            if (_storyService.IsGamePassed())
                _loadStoryButtons[currentLevelToPass].Enable();   
            else
                _loadStoryButtons[currentLevelToPass].EnableGlow();
            
            for (int i = currentLevelToPass + 1; i < _loadStoryButtons.Length; i++)
                _loadStoryButtons[i].Disable();
        }
    }
}