using Code.GameInfrastructure.AllBaseServices.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Gaming.Onboarding
{
    public class GameOnboarding : MonoBehaviour
    {
        private const string IsOnboardingShownKey = "IsOnboardingShown";
        
        [SerializeField] private GameObject _firstPage;
        [SerializeField] private StartGameButton _tapToStart;
        
        private IPlayerPrefsFunctiousWrapper _playerPrefsFunctiousWrapper;

        [Inject]
        public void Injector(IPlayerPrefsFunctiousWrapper playerPrefsFunctiousWrapper)
        {
            _playerPrefsFunctiousWrapper = playerPrefsFunctiousWrapper;
        }

        private void Awake()
        {
            if (_playerPrefsFunctiousWrapper.HasKey(IsOnboardingShownKey))
            {
                Destroy(gameObject);   
                _tapToStart.Show();
            }
            else
            {
                _playerPrefsFunctiousWrapper.SetBool(IsOnboardingShownKey, true);
                _firstPage.SetActive(true);
                _tapToStart.Hide();
            }
        }
    }
}