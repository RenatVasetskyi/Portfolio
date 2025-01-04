using System.Collections.Generic;
using Architecture.Services.Interfaces;
using Data;
using Game.Interfaces;
using Game.Systems;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Column : MonoBehaviour
    {
        [SerializeField] private List<SlotPosition> _slotMovementPositions;
        [SerializeField] private Column _previousColumn;
        
        [Space]
        
        [SerializeField] private Transform _spawnSlotPosition;
        [SerializeField] private Transform _destroySlotPosition;
        
        [Space]
        
        [SerializeField] private int _minCreatedSlotsToStopSpin;

        private IMoveSlotSystem _moveSlotSystem;

        private GameSettings _gameSettings;
        private IBaseFactory _baseFactory;

        private ISlotSystem _slotSystem;
        public bool IsStopped => _moveSlotSystem.IsStopped;

        [Inject]
        public void Construct(GameSettings gameSettings, IBaseFactory baseFactory)
        {
            _gameSettings = gameSettings;
            _baseFactory = baseFactory;
        }

        public void Initialize(ISlotSystem slotSystem)
        {
            _slotSystem = slotSystem;
            
            _moveSlotSystem = new MoveSlotSystem(_gameSettings, _baseFactory, _slotMovementPositions, _slotSystem,
                _spawnSlotPosition, _destroySlotPosition, _previousColumn, _minCreatedSlotsToStopSpin);
        }

        public void Spin(float moveSlotDuration)
        {
            _moveSlotSystem.Spin(moveSlotDuration);
        }
    }
}