using System;
using System.Collections.Generic;
using Architecture.Services.Interfaces;
using Audio;
using Games.Stories.Cards;
using Games.Stories.Data;
using Games.Stories.Interfaces;
using Games.Stories.Slots;
using UI.Base;
using UI.Rect;
using UI.Rect.Enums;
using UnityEngine;
using Zenject;

namespace Games.Stories.UI
{
    public class StoryGameView : MonoBehaviour
    {
        public List<CardWithPieceOfStory> CardsWithPieceOfStory;
        public List<SlotForCardWithPieceOfStory> SlotsForCardWithPieceOfStories;
        public CompletePercentageDisplayer CompletePercentageDisplayer;
        
        private IAudioService _audioService;
        private IStoryProgressService _storyProgressService;
        private IBaseFactory _baseFactory;
        
        private IStoryGameController _gameController;

        [Inject]
        public void Construct(IAudioService audioService, IStoryProgressService storyProgressService, 
            IBaseFactory baseFactory)
        {
            _baseFactory = baseFactory;
            _storyProgressService = storyProgressService;
            _audioService = audioService;
        }

        public void Initialize(IStoryGameController gameController)
        {
            _gameController = gameController;

            Subscribe();
        }

        private void OnDestroy()
        {
            UnSubscribe();   
        }

        private void ShowVictoryWindow()
        {
            _audioService.PlaySfx(SfxType.Victory);
            StoryData storyData = _storyProgressService.GetCurrentStoryToPass();
            Fade victoryWindow = _baseFactory.CreateBaseWithContainer
                (storyData.VictoryWindowPrefab, Vector3.zero, Quaternion.identity, transform);
            victoryWindow.transform.localScale = Vector3.one;
            victoryWindow.GetComponent<RectTransform>().SetAnchorPreset(AnchorPresets.StretchAll);
            victoryWindow.Enable();
        }
        
        private void Subscribe()
        {
            _gameController.OnVictory += ShowVictoryWindow;
        }
        
        private void UnSubscribe()
        {
            if (_gameController != null) 
                _gameController.OnVictory -= ShowVictoryWindow;
        }
    }
}