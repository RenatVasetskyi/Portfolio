using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay
{
    [RequireComponent(typeof(Image))]
    public class GameBackground : MonoBehaviour
    {
        private IShopService _shopService;
        private Image _image;

        [Inject]
        public void InjectServices(IShopService shopService)
        {
            _shopService = shopService;
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
            _shopService.OnItemChanged += Set;
        }

        private void OnEnable()
        {
            Set();
        }

        private void OnDestroy()
        {
            _shopService.OnItemChanged -= Set;
        }

        private void Set()
        {
            _image.sprite = _shopService.SelectedSkin.Sprite;
        }
    }
}