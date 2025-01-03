using Code.Data;
using Code.Gameplay.Candies.Enums;
using Code.Gameplay.Person;
using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Candies
{
    public class DoneCandy : MonoBehaviour
    {
        private const float MoveDuration = 0.5f;
        
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private ICoinService _coinService;
        private GameData _gameData;

        public FullCandyType Type { get; private set; }
        public Image Image => _image;

        private LTDescr _tween;

        [Inject]
        public void InjectServices(ICoinService coinService, GameData gameData)
        {
            _gameData = gameData;
            _coinService = coinService;
        }

        public void Init(FullCandyType type)
        {
            Type = type;
            
            _tween = LeanTween.value(0, 1, 0.25f)
                .setOnUpdate((value) =>
                {
                    if (_canvasGroup != null)
                        _canvasGroup.alpha = value;   
                })
                .setEase(LeanTweenType.linear);
        }

        public void Move(Customer customer, Transform customerEndPoint)
        {
            LeanTween.move(gameObject, customer.transform.position, MoveDuration)
                .setEase(LeanTweenType.linear)
                .setOnComplete(() =>
                {
                    LeanTween.cancel(_tween.id);
                    _coinService.AddCoins(_gameData.MakeOrderPrize);
                    customer.MoveAndDestroy(customerEndPoint);
                    Destroy(gameObject);
                });
        }
    }
}