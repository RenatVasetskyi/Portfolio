using Code.Infrastructure.Interfaces;
using Code.Music;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menu
{
    public class MenuModel
    {
        private readonly IGameSoundPlayer _gameSoundPlayer;
        private readonly ISceneController _sceneController;

        public MenuModel(IGameSoundPlayer gameSoundPlayer, ISceneController sceneController)
        {
            _gameSoundPlayer = gameSoundPlayer;
            _sceneController = sceneController;
        }
        
        public void OpenShopWindow(GameObject shopWindow)
        {
            shopWindow.SetActive(true);
            _gameSoundPlayer.Play(ShortSfx.Click);
        }
        
        public void HideShopWindow(GameObject shopWindow)
        {
            shopWindow.SetActive(false);
            _gameSoundPlayer.Play(ShortSfx.Click);
        }

        public void Play()
        {
            _sceneController.Load(SceneName.Game);
            _gameSoundPlayer.Play(ShortSfx.Click);
        }

        public void ChangeSound(Image image, Sprite enabled, Sprite disabled)
        {
            image.sprite = _gameSoundPlayer.InverseToggle() ? enabled : disabled;
            _gameSoundPlayer.Play(ShortSfx.Click);
        }
    }
}