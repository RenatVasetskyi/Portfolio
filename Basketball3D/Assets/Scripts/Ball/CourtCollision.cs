using Assets.Scripts.Architecture.Services.Interfaces;
using Assets.Scripts.Audio;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Ball
{
    public class CourtCollision : MonoBehaviour
    {
        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService) =>
            _audioService = audioService;

        private void OnCollisionEnter(Collision other) =>
            _audioService.PlaySfx(SfxType.BallKnock);
    }
}