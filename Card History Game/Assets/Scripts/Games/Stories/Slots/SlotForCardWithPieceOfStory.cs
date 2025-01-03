using Games.Stories.Cards;
using Games.Stories.Cards.Enums;
using Games.Stories.Interfaces;
using UnityEngine;

namespace Games.Stories.Slots
{
    public class SlotForCardWithPieceOfStory : MonoBehaviour
    {
        public StoryPieceType StoryPieceType;
        
        [SerializeField] private RectTransform _rectTransform;
        
        private IStoryGameController _storyGameController;
        public CardWithPieceOfStory CurrentCard { get; private set; }

        public void Initialize(IStoryGameController storyGameController)
        {
            _storyGameController = storyGameController;
        }

        public void SetCurrentCardByChildren()
        {
            CardWithPieceOfStory card = transform.GetComponentInChildren<CardWithPieceOfStory>();
            CurrentCard = card;
            _storyGameController.CheckCompletePercentage();
        }

        public bool HasCard()
        {
            return CurrentCard != null;
        }
    }
}