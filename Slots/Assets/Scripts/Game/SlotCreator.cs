using Architecture.Services.Interfaces;
using Game.Interfaces;
using UnityEngine;

namespace Game
{
    public class SlotCreator : ISlotCreator
    {
        private readonly IBaseFactory _baseFactory;

        public SlotCreator(IBaseFactory baseFactory)
        {
            _baseFactory = baseFactory;
        }

        public Slot CreateSlot(ISlotSystem slotSystem, Vector3 at, SlotPosition slotPosition, int currentSlotPosition)
        {
            Slot randomSlotPrefab = slotSystem.SlotPack[Random.Range(0, slotSystem.SlotPack.Count)];

            Slot createdSlot = _baseFactory.CreateBaseWithContainer
                (randomSlotPrefab, at, Quaternion.identity, slotPosition.transform);
            createdSlot.Initialize(slotSystem);

            createdSlot.CurrentPosition = currentSlotPosition;

            slotPosition.SetCurrentSlot(createdSlot);

            return createdSlot;
        }
    }
}