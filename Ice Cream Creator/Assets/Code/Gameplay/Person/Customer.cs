using System;
using System.Collections;
using Code.Audio.Enums;
using Code.Data;
using Code.Gameplay.Candies.Data;
using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Gameplay.Person
{
    public class Customer : MonoBehaviour
    {
        private const float MoveDuration = 1.8f;

        [SerializeField] private OrderDialogWindow _orderDialogWindow;
        [SerializeField] private GameObject _scaredDialog;
        [SerializeField] private GameObject _funnyDialog;
        
        private GameData _gameData;
        private ISoundManager _soundManager;

        private Coroutine _enableScaredEmoji;

        public CandyData Order { get; private set; }

        [Inject]
        public void InjectServices(GameData gameData, ISoundManager soundManager)
        {
            _soundManager = soundManager;
            _gameData = gameData;
        }

        public void MoveAndDestroy(Transform to)
        {
            LeanTween.cancel(gameObject);
            
            if (_enableScaredEmoji != null)
                StopCoroutine(_enableScaredEmoji);
            
            _funnyDialog.SetActive(true);
            
            if (PlaySfx()) 
                _soundManager.PlaySfx(SfxTypeEnum.GoodEmotion);
            
            LeanTween.moveLocal(gameObject, to.localPosition, MoveDuration)
                .setEase(LeanTweenType.linear)
                .setOnComplete(() => Destroy(gameObject));
        }
        
        public void MoveAndOrder(Transform to)
        {
            LeanTween.moveLocal(gameObject, to.localPosition, MoveDuration)
                .setEase(LeanTweenType.linear)
                .setOnComplete(MakeOrder);
        }

        private void Awake()
        {
            _enableScaredEmoji = StartCoroutine(EnableScaredEmoji());
        }

        private void MakeOrder()
        {
            Order = _gameData.CandyDatas[Random.Range(0, _gameData.CandyDatas.Length)];
            
            _orderDialogWindow.DisplayOrder(Order.FullCandySprite);
            _orderDialogWindow.gameObject.SetActive(true);
        }

        private IEnumerator EnableScaredEmoji()
        {
            yield return new WaitForSeconds(Random.Range(4f, 8f));
            
            _scaredDialog.SetActive(true);
            
            if (PlaySfx())
                _soundManager.PlaySfx(SfxTypeEnum.BadEmotion);   
            
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            
            _scaredDialog.SetActive(false);
        }

        private bool PlaySfx()
        {
            return Random.Range(0, 3) == 0;
        }
    }
}