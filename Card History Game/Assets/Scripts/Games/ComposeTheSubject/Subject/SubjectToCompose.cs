using System.Collections.Generic;
using Architecture.Services.Interfaces;
using Audio;
using Games.ComposeTheSubject.Enums;
using Games.ComposeTheSubject.Interfaces;
using Games.ComposeTheSubject.Slots;
using Games.ComposeTheSubject.Subject.Animation;
using UI.Rect;
using UI.Rect.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Games.ComposeTheSubject.Subject
{
    public class SubjectToCompose : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private const float BaseAlpha = 1f;
        private const float DragAlpha = 0.6f;
        
        public SubjectType Type;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _parentWhenDrag;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        
        private IAudioService _audioService;
        private IComposeTheSubjectGameController _gameController;
        
        private SubjectAnimator _subjectAnimator;
        
        private Transform _originalParent;
        private Vector2 _touchPosition;

        public SlotForSubject Slot { get; set; }
        public RectTransform RectTransform => _rectTransform;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public void Initialize(IComposeTheSubjectGameController gameController)
        {
            _gameController = gameController;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_gameController.CardInDrag)
                return;
            
            _gameController.CardInDrag = true;
            _gameController.DraggingFingerId = eventData.pointerId;
            
            _canvasGroup.alpha = DragAlpha;
            _canvasGroup.blocksRaycasts = false; 
            transform.SetParent(_parentWhenDrag);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_gameController.CardInDrag && eventData.pointerId == _gameController.DraggingFingerId)
            {
                _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
                _touchPosition = Input.touches[0].position;   
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_gameController.CardInDrag || eventData.pointerId != _gameController.DraggingFingerId)
                return;
            
            _gameController.CardInDrag = false;
            _gameController.DraggingFingerId = -1;
                
            SlotForSubject targetSlot = GetSlotFromFingerPosition();
            
            _canvasGroup.alpha = BaseAlpha;
            _canvasGroup.blocksRaycasts = true;

            if (targetSlot == null)
            {
                SetOriginalPosition();
                return;
            }

            SetSlot(targetSlot);
        }
        
        public void SetOriginalPosition()
        {
            transform.SetParent(_originalParent);
            RectTransform.SetAnchorPreset(AnchorPresets.Centered);
            transform.localPosition = Vector2.zero;
            transform.localScale = Vector3.one;
            _image.SetNativeSize();

            if (Slot != null)
            {
                Slot.Clear();
                Slot = null;   
            }
        }
        
        public void DoHintAnimation()
        {
            _subjectAnimator.PlayHintAnimation();
        }
        
        public void PlayIdleAnimation()
        {
            _subjectAnimator.PlayIdleAnimation();
        }

        private void Awake()
        {
            _subjectAnimator = new(_animator);
            PlayIdleAnimation();
        }

        private void Start()
        {
            _originalParent = transform.parent;
        }

        private void SetSlot(SlotForSubject slotForSubject)
        {
            _audioService.PlaySfx(SfxType.SetSubjectInSlot);
            slotForSubject.SetSubject(this, true);
        }

        private SlotForSubject GetSlotFromFingerPosition()
        {
            List<RaycastResult> results = new List<RaycastResult>();

            _graphicRaycaster.Raycast(new PointerEventData(EventSystem.current) { position = _touchPosition }, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent(out SlotForSubject slot))
                {
                    return slot;
                }
            }

            return null;
        }
    }
}