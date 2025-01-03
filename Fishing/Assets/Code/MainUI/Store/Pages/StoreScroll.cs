using System.Collections.Generic;
using Code.GameAudio.Enums;
using Code.GameInfrastructure.AllBaseServices.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.MainUI.Store.Pages
{
    public class StoreScroll : MonoBehaviour
    {
        [SerializeField] private List<CanvasGroup> _pages;

        [SerializeField] private Button _rightArrow;
        [SerializeField] private Button _leftArrow;
        
        private ISoundManager _soundManager;
        
        private int _currentPage;

        private bool _processing;

        [Inject]
        public void Injector(ISoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        private void OnEnable()
        {
            _rightArrow.onClick.AddListener(OpenNextPage);
            _leftArrow.onClick.AddListener(OpenPreviousPage);

            OpenCurrentPage();
            SetButtonsInteraction();
        }

        private void OnDisable()
        {
            _rightArrow.onClick.RemoveListener(OpenNextPage);
            _leftArrow.onClick.RemoveListener(OpenPreviousPage);
        }

        private void OpenNextPage()
        {
            if (_processing)
                return;

            _processing = true;
            
            _soundManager.PlaySfx(Sfxes.Click);
            
            CanvasGroup page = _pages[_currentPage];
            LeanTween.alphaCanvas(page, 0, 0.2f)
                .setOnComplete(() => page.gameObject.SetActive(false));
            
            _currentPage++;
            
            CanvasGroup nextPage = _pages[_currentPage];
            nextPage.alpha = 0;
            nextPage.gameObject.SetActive(true);
            LeanTween.alphaCanvas(nextPage, 1, 0.2f)
                .setOnComplete(() => _processing = false);

            SetButtonsInteraction();
        }

        private void OpenPreviousPage()
        {
            if (_processing)
                return;

            _processing = true;
            
            _soundManager.PlaySfx(Sfxes.Click);
            
            CanvasGroup page = _pages[_currentPage];
            LeanTween.alphaCanvas(page, 0, 0.2f)
                .setOnComplete(() => page.gameObject.SetActive(false));;
            _currentPage--;

            CanvasGroup nextPage = _pages[_currentPage]; 

            nextPage.alpha = 0;
            nextPage.gameObject.SetActive(true);
            LeanTween.alphaCanvas(nextPage, 1, 0.2f)
                .setOnComplete(() => _processing = false);

            SetButtonsInteraction();
        }

        private void OpenCurrentPage()
        {
            _pages[_currentPage].alpha = 0;
            _pages[_currentPage].gameObject.SetActive(true);
            LeanTween.alphaCanvas(_pages[_currentPage], 1, 0.2f);
        }

        private void SetButtonsInteraction()
        {
            _rightArrow.interactable = _currentPage < _pages.Count - 1;
            _leftArrow.interactable = _currentPage > 0;
        }
    }
}