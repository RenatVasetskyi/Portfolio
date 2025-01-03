using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Factories.Interfaces;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Gaming
{
    public class GameController : MonoBehaviour
    {
        private const float GenerateFishesDelay = 0.2f;
        
        [SerializeField] private StartGameButton _startGameButton;
        [SerializeField] private Canvas _uiCanvas;
        [SerializeField] private FishMoveLine[] _fishMoveLines;
        [SerializeField] private FishingLine _fishingLine;
        [SerializeField] private Transform _hookParent;
        
        [Space]
        
        [SerializeField] private Transform _startHookPoint;
        [SerializeField] private Transform _endHookPoint;
        [SerializeField] private Transform _tearHookPoint;
        
        [SerializeField] private Transform[] _positionsToGotFishes;
        
        private IGamePlayFactory _gamePlayFactory;
        private ICoinService _coinService;
        private ISoundManager _soundManager;
        
        private Hook _hook;

        public Transform Hook => _hook.transform;
        public bool InProcess { get; private set; }

        [Inject]
        public void Injector(IGamePlayFactory gamePlayFactory, ICoinService coinService,
            ISoundManager soundManager)
        {
            _soundManager = soundManager;
            _coinService = coinService;
            _gamePlayFactory = gamePlayFactory;
        }

        public void Initialize(UnityEngine.Camera camera)
        {
            _uiCanvas.worldCamera = camera;
            _uiCanvas.planeDistance = 5;
        }
        
        public void StartGame()
        {
            if (InProcess)
                return;
            
            InProcess = true; 
            _startGameButton.gameObject.SetActive(false);
            _hook.MoveDown();
        }

        private void Awake()
        {
            StartCoroutine(GenerateFishes());
            CreateHook();
        }

        public void StartGameEnd()
        {
            StartCoroutine(EndGame());
        }

        private IEnumerator EndGame()
        {
            StartCoroutine(AnimateGotFishes(_hook.GetFishes()));
            yield return new WaitUntil(() => InProcess == false);
            _startGameButton.gameObject.SetActive(true);
        }

        private IEnumerator GenerateFishes()
        {
            while (true)
            {
                yield return new WaitUntil(() => _fishMoveLines.Any(x => !x.HasFish));
                FishMoveLine line = _fishMoveLines.First(x => !x.HasFish);
                Fish fish = _gamePlayFactory.SpawnRandomFish(transform, line.Start, line.End);
                line.SetFish(fish.transform);
                yield return new WaitForSeconds(GenerateFishesDelay);   
            }
        }

        private IEnumerator AnimateGotFishes(List<Fish> fishes)
        {
            for (int i = 0; i < fishes.Count; i++)
            {
                Fish fish = fishes[i]; 
                Transform position = _positionsToGotFishes[i];

                LeanTween.rotate(fish.gameObject, Vector3.zero, 0.15f)
                    .setEase(LeanTweenType.linear);
                
                LeanTween.move(fish.gameObject, position.position, 0.4f)
                    .setEase(LeanTweenType.linear)
                    .setOnComplete(() =>
                    {
                        _coinService.GiveCoins(fish.Prize);
                        LeanTween.scale(fish.gameObject, Vector3.zero, 0.15f)
                            .setEase(LeanTweenType.easeInBack)
                            .setOnStart(() => _soundManager.PlaySfx(Sfxes.GetFish))
                            .setOnComplete(() => Destroy(fish.gameObject));
                    });
                
                yield return new WaitForSeconds(0.4f);
            }

            InProcess = false;
        }

        private void CreateHook()
        {
            _hook = _gamePlayFactory.SpawnHook(_startHookPoint.position, _hookParent);
            _hook.Initialize(this, _startHookPoint, _endHookPoint, _tearHookPoint, _fishingLine);
            _fishingLine.SetHookConnector(_hook.Connector);
        }
    }
}