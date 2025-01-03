using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menu
{
    [Serializable]
    public class MenuView
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _openShopWindowButton;
        [SerializeField] private Button _hideShopWindowButton;
        [SerializeField] private GameObject _shopWindow;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Sprite _soundToggleEnabled;
        [SerializeField] private Sprite _soundToggleDisabled;
        
        public Button PlayButton => _playButton;
        public Button OpenShopWindowButton => _openShopWindowButton;
        public Button HideShopWindowButton => _hideShopWindowButton;
        public GameObject ShopWindow => _shopWindow;
        public Button SoundButton => _soundButton;
        public Sprite SoundToggleEnabled => _soundToggleEnabled;
        public Sprite SoundToggleDisabled => _soundToggleDisabled;
    }
}