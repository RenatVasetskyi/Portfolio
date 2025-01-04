using UnityEngine;

namespace Game.Interfaces
{
    public interface ISlotCreator
    {
        Slot CreateSlot(ISlotSystem slotSystem, Vector3 at,
            SlotPosition slotPosition, int currentSlotPosition);
    }
}