using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Gaming
{
    public class Hook : MonoBehaviour
    {
        private const float MoveDownDuration = 1.3f;
        private const float MoveUpDuration = 4f;
        private const float MoveUpDelay = 0.75f;
        private const float TearDuration = 1.25f;
        
        private const float RandomOffsetX = 2f;
        private const float RandomOffsetY = 7f;

        private readonly List<Fish> _fishes = new();
        
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private HoldFishPoint[] _holdFishPoints;
        [SerializeField] private Transform _connector;
        
        private ISoundManager _soundManager;
        
        private GameController _gameController;
        private Transform _startPoint;
        private Transform _endPoint;
        private Transform _tearPoint;
        private FishingLine _fishingLine;
        
        private UnityEngine.Camera _camera;
        
        private bool _canControlPositionX;
        public Transform Connector => _connector;

        [Inject]
        public void Injector(ISoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        public void Initialize(GameController gameController, Transform startPoint, Transform endPoint,
            Transform tearPoint, FishingLine fishingLine)
        {
            _gameController = gameController;
            _startPoint = startPoint;
            _endPoint = endPoint;
            _tearPoint = tearPoint;
            _fishingLine = fishingLine;
        }

        public void MoveDown()
        {
            StartCoroutine(PlayMoveRopeDownSfx());
            
            foreach (HoldFishPoint holdFishPoint in _holdFishPoints)
                holdFishPoint.Disable();
            
            _canControlPositionX = true;
            
            _fishes.Clear();

            Vector3 pointToMove = _endPoint.position + new Vector3(Random.Range(-RandomOffsetX, RandomOffsetX),
                Random.Range(-RandomOffsetY, RandomOffsetY), 0);
            
            LeanTween.move(gameObject, pointToMove, MoveDownDuration)
                .setEase(LeanTweenType.easeInOutBack)
                .setOnComplete(MoveUp);
        }

        public void SetFish(Fish fish)
        {
            if (Vibration.HasVibrator())
                Vibration.VibrateIOS(ImpactFeedbackStyle.Soft);   
            
            _fishes.Add(fish);

            int fullWeight = _fishes.Sum(x => x.Weight);

            if (_fishingLine.MaxWeight < fullWeight)
                Tear();
        }

        public List<Fish> GetFishes()
        {
            return _fishes.ToList();
        }

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            ControlPositionX();
            ClampOnCameraBounds();
        }

        private void MoveUp()
        {
            _canControlPositionX = true;
            
            LeanTween.moveY(gameObject, _startPoint.position.y, MoveUpDuration)
                .setEase(LeanTweenType.linear)
                .setDelay(MoveUpDelay)
                .setOnStart(() =>
                {
                    foreach (HoldFishPoint holdFishPoint in _holdFishPoints)
                        holdFishPoint.Enable();
                })
                .setOnComplete(() =>
                {
                    _canControlPositionX = false;
                    _gameController.StartGameEnd();
                });
        }

        private void ClampOnCameraBounds()
        {
            float width = _spriteRenderer.bounds.extents.x / 2;
            
            Vector3 position = transform.position;
            Vector3 screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
            position.x = Mathf.Clamp(position.x, -screenBounds.x + width, screenBounds.x - width);
            transform.position = position;
        }

        private void ControlPositionX()
        {
            if (!_canControlPositionX || _camera == null || EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.touchCount > 0)
            {
                Vector2 touchPosition = Input.GetTouch(0).position;
                Vector3 position = _camera.ScreenToWorldPoint(touchPosition);
                transform.position = new Vector3(position.x, transform.position.y);
            }
        }

        private void Tear()
        {
            _soundManager.PlaySfx(Sfxes.BreakFishingLine);
            
            foreach (HoldFishPoint holdFishPoint in _holdFishPoints)
                holdFishPoint.Disable();
            
            _canControlPositionX = false;

            _fishingLine.gameObject.SetActive(false);
            
            LeanTween.cancel(gameObject);

            LeanTween.rotate(gameObject, new Vector3(0, 0, 90), 0.25f)
                .setEase(LeanTweenType.linear);

            LeanTween.move(gameObject, _tearPoint.position, TearDuration)
                .setEase(LeanTweenType.linear)
                .setOnComplete(() =>
                {
                    foreach (Fish fish in _fishes)
                        Destroy(fish.gameObject);
                    
                    _fishes.Clear();
                    
                    _fishingLine.gameObject.SetActive(true);
                    
                    transform.position = _startPoint.position;
                    transform.rotation = Quaternion.identity;
                    
                    _gameController.StartGameEnd();
                });
        }

        private IEnumerator PlayMoveRopeDownSfx()
        {
            yield return new WaitForSeconds(0.28f);
            _soundManager.PlaySfx(Sfxes.MoveRopeDown);
        }
    }
}