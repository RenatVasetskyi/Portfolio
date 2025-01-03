using Code.MainInfrastructure.MainGameService.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Code.Gameplay.Tutorial
{
    public class OnceOpenWindow : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private string _saveId;
        
        private ISaveToPlayerPrefs _saveToPlayerPrefs;

        [Inject]
        public void InjectServices(ISaveToPlayerPrefs saveToPlayerPrefs)
        {
            _saveToPlayerPrefs = saveToPlayerPrefs;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Destroy(gameObject);
        }

        private void Awake()
        {
            if (_saveToPlayerPrefs.HasKey(_saveId))
                Destroy(gameObject);   
            else
                _saveToPlayerPrefs.SetBool(_saveId, true);
        }
    }
}