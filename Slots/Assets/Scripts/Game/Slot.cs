using Game.Enums;
using Game.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public abstract class Slot : MonoBehaviour
    {
        private const float AnimationDuration = 0.15f;
        private const float ScaleMultiplayer = 1.4f;
        
        public SlotType Type;
        
        protected ISlotSystem _slotSystem;
        protected Image _image;

        public int CurrentPosition { get; set; }
        
        public abstract void WinAction();
        
        public void Initialize(ISlotSystem slotSystem)
        {
            _slotSystem = slotSystem;
        }

        public void Animate()
        {
            LeanTween.scale(gameObject, Vector3.one * ScaleMultiplayer, AnimationDuration)
                .setEase(LeanTweenType.easeOutCirc)
                .setOnComplete(() =>
                {
                    LeanTween.scale(gameObject, Vector3.one, AnimationDuration)
                        .setEase(LeanTweenType.easeInCirc);
                });
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
        }
    }
}