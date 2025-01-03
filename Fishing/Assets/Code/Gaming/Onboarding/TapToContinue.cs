using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gaming.Onboarding
{
    public class TapToContinue : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private List<GameObject> _pages;
        [SerializeField] private GameObject _onboarding;
        [SerializeField] private StartGameButton _startGameButton;

        private int _nextPage;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OpenNextPage();
        }

        private void OpenNextPage()
        {
            foreach (GameObject page in _pages)
                page.SetActive(false);

            if (_nextPage >= _pages.Count)
            {
                _startGameButton.Show();
                Destroy(_onboarding);
                return;
            }
            
            _pages[_nextPage].SetActive(true);
            _nextPage++;
        }
    }
}