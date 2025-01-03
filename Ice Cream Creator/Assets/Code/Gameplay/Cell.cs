using Code.Gameplay.Candies.Data;
using Code.Gameplay.Candies.Enums;
using Code.MainInfrastructure.MainGameService.Factories.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay
{
    public class Cell : MonoBehaviour
    {
        private const float MoveDuration = 0.2f;
        
        [Header("Components")]
        public PositionInGrid PositionInGrid;
        
        private IUIFactory _uiFactory;
        private PlayingField _playingField;
        
        private Image _candyHalfImage;

        public HalfOfCandyType HalfOfCandyType { get; private set; }
        public FullCandyType FullCandyType { get; private set; }
        
        public float SwapDuration => MoveDuration;

        [Inject]
        public void InjectServices(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Init(PlayingField playingField)
        {
            _playingField = playingField;
            SetRandomCandy();
        }

        public void SetPositionInGrid(PositionInGrid positionInGrid)
        {
            PositionInGrid.X = positionInGrid.X;
            PositionInGrid.Y = positionInGrid.Y;

            Vector2 positionToMove = _playingField.GetLocalPositionFromGridPosition(PositionInGrid.X, PositionInGrid.Y);

            LeanTween.moveLocal(gameObject, positionToMove, MoveDuration);
        }

        public void Match()
        {
            _candyHalfImage.transform.SetParent(_uiFactory.GameUI.transform);

            GameObject candy = _candyHalfImage.gameObject;
            _candyHalfImage = null;

            CanvasGroup canvasGroup = candy.GetComponent<CanvasGroup>();
            LTDescr tween = LeanTween.value(1, 0, 0.5f)
                .setOnUpdate((value) =>
                {
                    if (canvasGroup != null)
                        canvasGroup.alpha = value;   
                })
                .setEase(LeanTweenType.linear);
            
            LeanTween.moveLocal(candy, Vector3.zero, 0.5f)
                .setOnComplete(() =>
                {
                    LeanTween.cancel(tween.id);
                    Destroy(candy);
                });

            SetRandomCandy();
        }

        private void SetRandomCandy()
        {
            _candyHalfImage = _uiFactory.CreateCandyImage(transform);

            CandyData fullData = _playingField.GetUnusedFullCandy();
            HalfCandyData halfData = _playingField.GetUnusedHalfCandy(fullData);
            
            FullCandyType = fullData.FullCandyType;
            HalfOfCandyType = halfData.Type;

            _candyHalfImage.sprite = halfData.Sprite;
            _candyHalfImage.SetNativeSize();
        }
    }
}