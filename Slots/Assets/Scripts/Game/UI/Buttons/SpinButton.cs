using Architecture.Services.Interfaces;
using Game.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Upgrade.Enums;
using Zenject;

namespace Game.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class SpinButton : MonoBehaviour
    {
        private const int MoveCellsToStopCount = 8;
        private const float RotateAngle = 360f;
        
        [SerializeField] private float _spinSpeed;
        [SerializeField] private bool _playAnimation;
       
        private IUpgradeService _upgradeService;
        private ISlotSystem _slotSystem;
        private Button _button;

        public float BaseSpinSpeed => _spinSpeed;
        public float SpinSpeed { get; private set; }

        [Inject]
        public void Construct(IUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
        }
        
        public void Initialize(ISlotSystem slotSystem)
        {
            _slotSystem = slotSystem;
        }
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Spin);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Spin);
        }

        private void Spin()
        {
            float spinDuration = _spinSpeed / _upgradeService.GetUpgradeValue(UpgradeableType.Speed);
            SpinSpeed = spinDuration;
            _slotSystem.Spin(spinDuration);

            float animationDuration = spinDuration * MoveCellsToStopCount; 
            PlayAnimation(animationDuration);
        }

        private void PlayAnimation(float duration)
        {
            if (_playAnimation)
            {
                LeanTween.rotateAroundLocal(gameObject,  Vector3.forward, RotateAngle,duration)
                    .setEase(LeanTweenType.linear);   
            }
        }
    }
}