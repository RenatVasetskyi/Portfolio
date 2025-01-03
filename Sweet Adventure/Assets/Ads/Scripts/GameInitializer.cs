using System;
using Ads.Scripts.Connection.Enums;
using Code;
using Code.Infrastructure.Interfaces;
using Code.Music;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Serialization;
using Zenject;

namespace Ads.Scripts
{
    public class GameInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        public event Action<bool> OnInitialized;
        public event Action<bool> OnInternetConnectionChange;

        [SerializeField] string _androidId;
        [SerializeField] string _iOSId;
        
        [SerializeField] bool _testMode = true;
    
        private ICandyHandler _candyHandler;
        private IGameSoundPlayer _gameSoundPlayer;
        private ISceneController _sceneController;
        private IPlayerDataSocket _playerDataSocket;
        
        private string _gameId;

        private Coroutine _initializationCoroutine;

        private InternetType _type = InternetType.NotSelected;

        public static GameInitializer Instance { get; private set; }
        public bool IsInitialized => Advertisement.isInitialized;
        public bool HasInternetOnDevice { get; private set; }

        [Inject]
        public void GetSockets(ICandyHandler candyHandler, IGameSoundPlayer gameSoundPlayer, 
            ISceneController sceneController, IPlayerDataSocket playerDataSocket)
        {
            _playerDataSocket = playerDataSocket;
            _sceneController = sceneController;
            _gameSoundPlayer = gameSoundPlayer;
            _candyHandler = candyHandler;
        }

        private void Update()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable &
                _type != InternetType.Connected)
            {
                _type = InternetType.Connected;

                HasInternetOnDevice = true;

                OnInternetConnectionChange?.Invoke(HasInternetOnDevice);
            }
            else if (Application.internetReachability == NetworkReachability.NotReachable &
                     _type != InternetType.NotConnected)
            {
                _type = InternetType.NotConnected;

                HasInternetOnDevice = false;

                OnInternetConnectionChange?.Invoke(HasInternetOnDevice);
            }
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Ads initialized.");

            OnInitialized?.Invoke(IsInitialized);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Ads Initialization Error: {error.ToString()} - {message}");
        }
    
        private void Awake()
        {
            AdjustSettings();
            InitializeSockets();
            
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);   
            }
            else
            {
                Instance = this;
                
                DontDestroyOnLoad(gameObject);
            }

            if (HasInternetOnDevice)
                InitializeAds();
            
            OnInternetConnectionChange += ReInitialize;
            
            _sceneController.Load(SceneName.Menu);
        }

        private void OnDestroy()
        {
            OnInternetConnectionChange -= ReInitialize;
        }

        private void ReInitialize(bool isNetworkConnected)
        {
            if (isNetworkConnected)
                InitializeAds();   
        }
        
        private void InitializeAds()
        {
#if UNITY_IOS
            _gameId = _iOSId;
#elif UNITY_ANDROID
                _gameId = _androidGameId;
#elif UNITY_EDITOR
                _gameId = _androidId;
#endif
            if (!Advertisement.isInitialized && Advertisement.isSupported)
                Advertisement.Initialize(_gameId, _testMode, this);
        }

        private void InitializeSockets()
        {
            _candyHandler.LoadCandiesFromSaves();
            _gameSoundPlayer.Load();
            _playerDataSocket.LoadData();
            _gameSoundPlayer.Play(Music.Music);
        }

        private void AdjustSettings()
        {
            Application.targetFrameRate = 120;
            
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
        }
    }
}
