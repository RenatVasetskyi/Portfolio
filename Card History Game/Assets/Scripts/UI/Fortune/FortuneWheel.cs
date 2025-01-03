using Architecture.Services.Interfaces;
using Audio;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Fortune
{
    public class FortuneWheel : MonoBehaviour
    {
        private const float RotationDuration = 3f;
        private const int MinRotationAngle = -1200;
        private const int MaxRotationAngle = -2400;

        [SerializeField] private LeanTweenType _easing;
        
        [SerializeField] private Button _spinButton;
        [SerializeField] private Button _backButton;
        
        [SerializeField] private FortuneVictoryWindow _victoryWindow;

        private IAudioService _audioService;
        private IMelodyService _melodyService;
        
        private FortuneWheelSlice _fortuneWheelSlice;

        [Inject]
        public void Construct(IAudioService audioService, IMelodyService melodyService)
        {
            _melodyService = melodyService;
            _audioService = audioService;
        }

        public void SetCurrentSliceOnPointer(FortuneWheelSlice fortuneWheelSlice)
        {
            _fortuneWheelSlice = fortuneWheelSlice;
        }

        private void OnEnable()
        {
            _spinButton.onClick.AddListener(Spin);
        }

        private void OnDisable()
        {
            _spinButton.onClick.RemoveListener(Spin);
        }

        private void Spin()
        {
            _spinButton.interactable = false;
            _backButton.interactable = false;
            
            LeanTween.rotateZ(gameObject, transform.rotation.z + Random.Range
                    (MinRotationAngle, MaxRotationAngle), RotationDuration)
                .setEase(_easing).setOnComplete(OnStopSpin);
            
            _audioService.PlaySfx(SfxType.SpinFortune);
        }

        private void OnStopSpin()
        {
            _spinButton.interactable = true;
            _backButton.interactable = true;
            
            _audioService.PlaySfx(SfxType.WinFortune);
            
            ShowWin();
        }

        private void ShowWin()
        {
            _victoryWindow.Initialize(_fortuneWheelSlice.Icon, _fortuneWheelSlice.Name);
            _victoryWindow.Open();
            _melodyService.SelectGameMelody(_fortuneWheelSlice.Prize);
        }
    }
}