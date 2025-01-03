using System.Collections.Generic;
using System.Linq;
using Architecture.Services.Interfaces;
using Audio;
using Games.Stories.Cards.Enums;
using Games.Stories.Data;
using Games.Stories.Interfaces;
using Games.Stories.Slots;
using Games.Stories.UI;
using TMPro;
using UI.Rect;
using UI.Rect.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Games.Stories.Cards
{
    public class CardWithPieceOfStory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private const float AnimationDuration = 0.25f;
        
        private const float BaseAlpha = 1f;
        private const float DragAlpha = 0.6f;
        
        public StoryPieceType StoryPieceType;
        public RectTransform RectTransform;
        
        [Header("Canvas")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _miniCardText;
        [SerializeField] private TextMeshProUGUI _fullCardText;
        
        [Header("Images")]
        [SerializeField] private Image _miniCardImage;
        [SerializeField] private Image _fullCardImage;
        
        [Header("Buttons")]
        [SerializeField] private Button _openFullCardButton;
        [SerializeField] private Button _hideFullCardButton;
        
        [Header("Other")]
        [SerializeField] private Transform _parentWhenDrag;
        [SerializeField] private GameObject _miniCardFill;
        [SerializeField] private Fade _fade;
        
        private IStoryProgressService _storyService;
        private IAudioService _audioService;
        private IStoryGameController _storyGameController;
        
        private Transform _originalParent;
        private Vector2 _originalPosition;
        private Vector2 _touchPosition;
        private Vector2 _startSize;
        
        public SlotForCardWithPieceOfStory CurrentSlotIn { get; private set; }

        [Inject]
        public void Construct(IStoryProgressService storyService, IAudioService audioService)
        {
            _audioService = audioService;
            _storyService = storyService;
        }
        
        public void Initialize(IStoryGameController storyGameController)
        {
            _storyGameController = storyGameController;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_storyGameController.CardInDrag)
                return;
            
            _storyGameController.CardInDrag = true;
            _storyGameController.DraggingFingerId = eventData.pointerId;
            
            _canvasGroup.alpha = DragAlpha;
            _canvasGroup.blocksRaycasts = false;
            
            RectTransform.SetParent(_parentWhenDrag);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_storyGameController.CardInDrag && eventData.pointerId == _storyGameController.DraggingFingerId)
            {
                RectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
                _touchPosition = Input.touches[0].position;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_storyGameController.CardInDrag || eventData.pointerId != _storyGameController.DraggingFingerId)
                return;
            
            _storyGameController.CardInDrag = false;
            _storyGameController.DraggingFingerId = -1;
                
            _storyGameController.CardInDrag = false;
            
            SlotForCardWithPieceOfStory targetSlot = GetSlotFromFingerPosition();
            
            _canvasGroup.alpha = BaseAlpha;
            _canvasGroup.blocksRaycasts = true;

            if (targetSlot == null)
            {
                SetOriginalPosition();
                return;
            }

            SetSlot(targetSlot, true);
        }
        
        private void Awake()
        {
            _originalParent = RectTransform.parent;
            _originalPosition = RectTransform.anchoredPosition;
            _startSize = RectTransform.sizeDelta;
            StoryPieceData storyPieceData = _storyService.GetCurrentStoryToPass().StoryPieces.First(piece => piece.Type == StoryPieceType);
            _miniCardText.text = storyPieceData.Text;
            _fullCardText.text = storyPieceData.Text;
            _miniCardImage.sprite = storyPieceData.Sprite;
            _fullCardImage.sprite = storyPieceData.Sprite;
            _fullCardImage.SetNativeSize();
            
            _openFullCardButton.onClick.AddListener(OpenFullCard);
            _hideFullCardButton.onClick.AddListener(HideFullCard);
        }

        private void OnDestroy()
        {
            _openFullCardButton.onClick.RemoveListener(OpenFullCard);
            _hideFullCardButton.onClick.RemoveListener(HideFullCard);
        }

        private void SetSlot(SlotForCardWithPieceOfStory slot, bool switchCards)
        {
            if (switchCards)
            {
                _audioService.PlaySfx(SfxType.SetSubjectInSlot);
                
                if (slot.HasCard() & CurrentSlotIn != null)
                    slot.CurrentCard.SetSlot(CurrentSlotIn, false);
                else if (slot.HasCard() & CurrentSlotIn == null)
                    slot.CurrentCard.SetOriginalPosition();
            }

            CurrentSlotIn = slot;

            RectTransform.SetParent(slot.transform, false);
            RectTransform.SetAnchorPreset(AnchorPresets.StretchAll);
            RectTransform.anchoredPosition = Vector3.zero;

            _storyGameController.UpdateSlots();
        }

        private SlotForCardWithPieceOfStory GetSlotFromFingerPosition()
        {
            List<RaycastResult> results = new List<RaycastResult>();

            _graphicRaycaster.Raycast(new PointerEventData(EventSystem.current) { position = _touchPosition }, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent(out SlotForCardWithPieceOfStory slot))
                {
                    return slot;
                }
            }

            return null;
        }
        
        private void SetOriginalPosition()
        {
            if (CurrentSlotIn != null)
            {
                _storyGameController.UpdateSlots();
                CurrentSlotIn = null;
            }
            
            RectTransform.SetParent(_originalParent, false);
            RectTransform.SetAnchorPreset(AnchorPresets.BottomCentered);
            RectTransform.anchoredPosition = _originalPosition;
            RectTransform.sizeDelta = _startSize;
        }

        private void OpenFullCard()
        {
            if (_storyGameController.CardInDrag)
                return;
            
            _audioService.PlaySfx(SfxType.UIClick);
            
            _hideFullCardButton.interactable = false;
            _openFullCardButton.interactable = false;
            
            _miniCardFill.SetActive(false);
            _miniCardImage.raycastTarget = false;
            _fade.Enable();

            _fullCardImage.transform.SetParent(_miniCardImage.transform);
            _fullCardImage.rectTransform.SetAnchorPreset(AnchorPresets.Centered);
            _fullCardImage.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            _fullCardImage.gameObject.SetActive(true);
            
            _fullCardImage.transform.SetParent(_parentWhenDrag);
            _fullCardImage.rectTransform.SetAnchorPreset(AnchorPresets.Centered);
            
            LeanTween.moveLocal(_fullCardImage.gameObject, Vector2.zero, AnimationDuration)
                .setEase(LeanTweenType.linear);
            
            LeanTween.scale(_fullCardImage.gameObject, new Vector3(0.85f, 0.85f, 0.85f), AnimationDuration)
                .setEase(LeanTweenType.linear);

            _hideFullCardButton.interactable = true;
            _openFullCardButton.interactable = true;
        }

        private void HideFullCard()
        {
            if (_storyGameController.CardInDrag)
                return;
            
            _audioService.PlaySfx(SfxType.UIClick);
            
            _hideFullCardButton.interactable = false;
            _openFullCardButton.interactable = false;
            
            _fade.Disable();
            
            LeanTween.move(_fullCardImage.gameObject, _miniCardImage.transform.position, AnimationDuration)
                .setEase(LeanTweenType.linear);
            
            LeanTween.scale(_fullCardImage.gameObject, new Vector3(0.4f, 0.4f, 0.4f), AnimationDuration)
                .setEase(LeanTweenType.linear)
                .setOnComplete(() =>
                {
                    _fullCardImage.transform.SetParent(RectTransform);
                    _fullCardImage.rectTransform.SetAnchorPreset(AnchorPresets.Centered);
                    _fullCardImage.gameObject.SetActive(false);
                    _miniCardFill.SetActive(true);
                    _miniCardImage.raycastTarget = true;
                });

            _hideFullCardButton.interactable = true;
            _openFullCardButton.interactable = true;
        }
    }
}