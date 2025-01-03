using Architecture.Services.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Background
{
    public class GameBackground : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        private ISkinService _skinService;
        
        [Inject]
        public void Construct(ISkinService skinService)
        {
            _skinService = skinService;
        }
        
        private void Awake()
        {
            SetSkin();

            _skinService.OnSkinChanged += SetSkin;
        }

        private void OnDestroy()
        {
            _skinService.OnSkinChanged -= SetSkin;
        }

        private void SetSkin()
        {
            _image.sprite = _skinService.SelectedSkin.Skin;
        }
    }
}