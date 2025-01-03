using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Services.Interfaces;
using Games.Stories.Cards;
using Games.Stories.Interfaces;
using Games.Stories.Slots;
using Games.Stories.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.Stories
{
    public class StoryGameController : IStoryGameController
    {
        private const int MaxPercents = 100;
        
        private readonly List<CardWithPieceOfStory> _cardsWithPieceOfStory;
        private readonly List<SlotForCardWithPieceOfStory> _slotForCardWithPieceOfStories;
        private readonly CompletePercentageDisplayer _completePercentageDisplayer;
        private readonly IStoryProgressService _storyProgressService;

        public event Action OnVictory;

        private bool _isGameOver;

        public bool CardInDrag { get; set; }
        public int DraggingFingerId { get; set; } = -1;

        public StoryGameController(List<CardWithPieceOfStory> cardsWithPieceOfStory, List<SlotForCardWithPieceOfStory> slotForCardWithPieceOfStories, 
            CompletePercentageDisplayer completePercentageDisplayer, IStoryProgressService storyProgressService)
        {
            _cardsWithPieceOfStory = cardsWithPieceOfStory;
            _slotForCardWithPieceOfStories = slotForCardWithPieceOfStories;
            _completePercentageDisplayer = completePercentageDisplayer;
            _storyProgressService = storyProgressService;

            InitializeComponents();
            ShuffleCards();
            CheckCompletePercentage();
        }

        public void CheckCompletePercentage()
        {
            List<SlotForCardWithPieceOfStory> slotWithCards = _slotForCardWithPieceOfStories.Where(slot => slot.HasCard()).ToList();
            int cardsWithRightType = slotWithCards.Where(slot => slot.CurrentCard.StoryPieceType == slot.StoryPieceType).Count();
            
            int rightPercent = (int)((float)cardsWithRightType / _cardsWithPieceOfStory.Count * MaxPercents);
            _completePercentageDisplayer.Display(rightPercent);

            if (cardsWithRightType == _slotForCardWithPieceOfStories.Count & !_isGameOver)
            {
                _isGameOver = true;
                OnVictory?.Invoke();   
                _storyProgressService.SetNextStoryToPass();
            }
        }

        public void UpdateSlots()
        {
            foreach (SlotForCardWithPieceOfStory slot in _slotForCardWithPieceOfStories)
                slot.SetCurrentCardByChildren();
        }

        private void InitializeComponents()
        {
            foreach (CardWithPieceOfStory card in _cardsWithPieceOfStory)
                card.Initialize(this);
            
            foreach (SlotForCardWithPieceOfStory slot in _slotForCardWithPieceOfStories)
                slot.Initialize(this);
        }

        private void ShuffleCards()
        {
            foreach (CardWithPieceOfStory card in _cardsWithPieceOfStory)
                card.RectTransform.SetSiblingIndex(Random.Range(0, _cardsWithPieceOfStory.Count));
        }
    }
}