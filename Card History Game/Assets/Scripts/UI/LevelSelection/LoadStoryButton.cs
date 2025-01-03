using Architecture.Services.Interfaces;
using Games.Stories.Enums;
using UI.Base;
using UnityEngine;
using Zenject;

namespace UI.LevelSelection
{
    public class LoadStoryButton : StateTransferButton
    {
        [SerializeField] private StoryType _type;

        [Space] 
        
        [SerializeField] private GameObject _openedState; 
        [SerializeField] private GameObject _closedState;
        
        private IStoryProgressService _storyService;
        
        [Inject]
        public void Construct(IStoryProgressService storyService)
        {
            _storyService = storyService;
        }

        public void Enable()
        {
            _closedState.SetActive(false);
            _openedState.SetActive(true);

            _button.interactable = true;
        }

        public void Disable()
        {
            _openedState.SetActive(false);
            _closedState.SetActive(true);
            
            _button.interactable = false;
        }
        
        public void EnableGlow()
        {
            _closedState.SetActive(false);
            _openedState.SetActive(true);
            
            _button.interactable = true;
        }
        
        protected override void ChangeState()
        {
            _storyService.SelectStory(_type);
            
            base.ChangeState();
        }
    }
}