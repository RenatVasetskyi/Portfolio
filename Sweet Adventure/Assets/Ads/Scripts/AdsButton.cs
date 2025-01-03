using System;
using Ads.Scripts.Connection;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Ads.Scripts
{
    public class AdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string AndroidAdvertisementUnitId = "Rewarded_Android";
        private const string IOSAdvertisementUnitId = "Rewarded_iOS";

        public event Action OnShowAdvertisementComplete;
        
        [SerializeField] private Button _button;

        private string _advertisementId;

        private void Awake()
        {   
#if UNITY_IOS
            _advertisementId = IOSAdvertisementUnitId;
#elif UNITY_ANDROID
        _advertisementId = AndroidAdvertisementUnitId;
#endif
            _button.interactable = false;

            if (GameInitializer.Instance.IsInitialized)
                LoadAd(true);   
            
            GameInitializer.Instance.OnInitialized += LoadAd;
            GameInitializer.Instance.OnInternetConnectionChange += SetInteractionState;
        }
 
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);
 
            if (adUnitId.Equals(_advertisementId))
            {
                _button.onClick.AddListener(ShowAd);
                
                _button.interactable = true;
            }
        }
 
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_advertisementId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
                OnShowAdvertisementComplete?.Invoke();
        }
 
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }
 
        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }
        
        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }
 
        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            
            GameInitializer.Instance.OnInitialized -= LoadAd;   
            GameInitializer.Instance.OnInternetConnectionChange -= SetInteractionState;
        }
        
        private void ShowAd()
        {
            _button.interactable = false;
            
            Advertisement.Show(_advertisementId, this);
        }
        
        private void LoadAd(bool isInitialized)
        {
            if (isInitialized)
                Advertisement.Load(_advertisementId, this);
            else
                _button.interactable = false;
        }

        private void SetInteractionState(bool isNetworkConnected)
        {
            _button.interactable = GameInitializer.Instance.IsInitialized & isNetworkConnected;
        }
    }
}
