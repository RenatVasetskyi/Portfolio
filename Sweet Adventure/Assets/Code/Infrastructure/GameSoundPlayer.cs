using Code.Infrastructure.Interfaces;
using Code.Music;
using UnityEngine;

namespace Code.Infrastructure
{
    public class GameSoundPlayer : IGameSoundPlayer
    {
        private const string ToggleStatePath = "Toggle State";
        
        private readonly IPlayerPrefsController _playerPrefsController;
        private readonly ICreator _creator;
        private readonly Data _data;

        private AudioSource _sfxer;
        private AudioSource _musisher;

        private bool _toggleState = true;

        public GameSoundPlayer(IPlayerPrefsController playerPrefsController, ICreator creator, Data data)
        {
            _playerPrefsController = playerPrefsController;
            _creator = creator;
            _data = data;
        }

        public void Load()
        {
            if (_playerPrefsController.Path(ToggleStatePath))
                _toggleState = _playerPrefsController.Bool(ToggleStatePath);

            Create();
            SetToggle();
        }

        public bool InverseToggle()
        {
            _toggleState = !_toggleState;
            SetToggle();
            SetBool();
            return _toggleState;
        }

        public bool GetToggleState()
        {
            return _toggleState;
        }

        public void Play(ShortSfx type)
        {
            _sfxer.PlayOneShot(_data.Sfx[type]);
        }

        public void Play(Music.Music type)
        {
            _musisher.clip = _data.Music[type];
            _musisher.Play();
        }

        private void SetBool()
        {
            _playerPrefsController.Bool(ToggleStatePath, _toggleState);
        }

        private void SetToggle()
        {
            if (_toggleState)
            {
                _sfxer.volume = 1;
                _musisher.volume = 1;
            }
            else
            {
                _sfxer.volume = 0;
                _musisher.volume = 0;
            }
        }

        private void Create()
        {
            _sfxer = _creator.Do(_data.SfxPrefab);
            _musisher = _creator.Do(_data.MusicPrefab);
        }
    }
}