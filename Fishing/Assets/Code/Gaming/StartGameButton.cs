using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gaming
{
    public class StartGameButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private GameObject _text;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _gameController.StartGame();
        }

        public void Hide()
        {
            _text.SetActive(false);   
        }

        public void Show()
        {
            _text.SetActive(true);
        }
    }
}