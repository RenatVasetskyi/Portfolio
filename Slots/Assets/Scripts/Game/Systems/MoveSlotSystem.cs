using System;
using System.Collections.Generic;
using Architecture.Services.Interfaces;
using Data;
using Game.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Systems
{
    public class MoveSlotSystem : IMoveSlotSystem
    {
        private readonly GameSettings _gameSettings;
        private readonly List<SlotPosition> _slotMovementPositions;
        private readonly ISlotSystem _slotSystem;
        private readonly Transform _spawnSlotPosition;
        private readonly Transform _destroySlotPosition;
        private readonly Column _previousColumn;
        private readonly int _minCreatedSlotsToStopSpin;
        private readonly List<Slot> _createdSlots = new();
        private readonly ISlotCreator _slotCreator;
        
        private int _createdSlotsCount;

        public bool IsStopped { get; private set; } = true;

        public MoveSlotSystem(GameSettings gameSettings, IBaseFactory baseFactory,
            List<SlotPosition> slotMovementPositions, ISlotSystem slotSystem,
            Transform spawnSlotPosition, Transform destroySlotPosition, 
            Column previousColumn, int minCreatedSlotsToStopSpin)
        {
            _gameSettings = gameSettings;
            _slotMovementPositions = slotMovementPositions;
            _slotSystem = slotSystem;
            _spawnSlotPosition = spawnSlotPosition;
            _destroySlotPosition = destroySlotPosition;
            _previousColumn = previousColumn;
            _minCreatedSlotsToStopSpin = minCreatedSlotsToStopSpin;
            _slotCreator = new SlotCreator(baseFactory);

            CreateStartSlots();
        }

        public void Spin(float moveSlotDuration)
        {
            IsStopped = false;
            _createdSlotsCount = 0;
            
            CreateNewSlotAndMove(moveSlotDuration);
        }
        
        private void CreateStartSlots()
        {
            int currentSlotPosition = 0;
            
            foreach (SlotPosition slotPosition in _slotMovementPositions)
            {
                Slot createdSlot = _slotCreator.CreateSlot(_slotSystem, 
                    slotPosition.transform.position, slotPosition, currentSlotPosition);
                _createdSlots.Add(createdSlot);
                currentSlotPosition++;
            }
        }

        private void CreateNewSlotAndMove(float moveSlotDuration)
        {
            TryToStopSpin();
            
            if (IsStopped)
                return;
            
            SetSlotsNextPosition(moveSlotDuration);
            
            SlotPosition positionToMove = _slotMovementPositions[0];
            Slot createdSlot = _slotCreator.CreateSlot(_slotSystem, _spawnSlotPosition.position, positionToMove, 0);
            _createdSlots.Add(createdSlot);
            positionToMove.SetCurrentSlot(createdSlot);
            _createdSlotsCount++;
            
            MoveSlot(createdSlot.gameObject, Vector3.zero, moveSlotDuration, () => CreateNewSlotAndMove(moveSlotDuration));
        }

        private void TryToStopSpin()
        {
            if (_previousColumn == null)
            {
                if (_createdSlotsCount < _minCreatedSlotsToStopSpin)
                    return;    
            }
            else
            {
                if (!_previousColumn.IsStopped | _createdSlotsCount < _minCreatedSlotsToStopSpin)
                    return;    
            }
            
            StopSpin();
        }

        private void StopSpin()
        {
            IsStopped = true;
            
            _slotSystem.Stop();
        }

        private void MoveSlot(GameObject slot, Vector3 to, float duration, Action onComplete = null)
        {
            LeanTween.moveLocal(slot, to, duration)
                .setEase(_gameSettings.MoveSlotEase)
                .setOnComplete(onComplete);
        }

        private void SetSlotsNextPosition(float moveSlotDuration)
        {
            for (int i = 0; i < _createdSlots.Count; i++)
            {
                Slot currentSlot = _createdSlots[i]; 
                currentSlot.CurrentPosition++;
                
                if (currentSlot.CurrentPosition < _slotMovementPositions.Count)
                {
                    _slotMovementPositions[currentSlot.CurrentPosition].SetCurrentSlot(currentSlot);
                    MoveSlot(currentSlot.gameObject, Vector3.zero, moveSlotDuration);
                }
                else
                {
                    currentSlot.transform.SetParent(_destroySlotPosition.transform);
                    
                    MoveSlot(currentSlot.gameObject, Vector3.zero, moveSlotDuration, () =>
                    {
                        _createdSlots.Remove(currentSlot);
                        Object.Destroy(currentSlot.gameObject);
                    });
                }
            }
        }
    }
}