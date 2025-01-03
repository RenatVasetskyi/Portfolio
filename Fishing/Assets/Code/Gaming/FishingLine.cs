using Code.GameInfrastructure.AllBaseServices.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Gaming
{
    public class FishingLine : MonoBehaviour
    {
        [SerializeField] private Transform _fishingRod;
        [SerializeField] private LineRenderer _lineRenderer;
        
        private IStoreService _storeService;
        private Transform _hookConnector;

        public int MaxWeight { get; private set; }

        [Inject]
        public void Inject(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public void SetHookConnector(Transform hookConnector)
        {
            _hookConnector = hookConnector;
        }
        
        private void Awake()
        {
            MaxWeight =  _storeService.SelectedRope.Weight;
            _lineRenderer.positionCount = 2;
        }

        private void LateUpdate()
        {
            if (_hookConnector == null)
                return;
            
            _lineRenderer.SetPosition(0, _fishingRod.position);
            _lineRenderer.SetPosition(1, _hookConnector.position);
        }
    }
}