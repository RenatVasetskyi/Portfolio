using Architecture.Services.Interfaces;
using Audio;
using Zenject;

namespace Game.Effects
{
    public class HeartEffect : Effect
    {
        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }
        
        protected override void PlaySound()
        {
        }
    }
}