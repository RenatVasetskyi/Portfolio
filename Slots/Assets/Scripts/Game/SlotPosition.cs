using Data;
using UnityEngine;
using Zenject;

namespace Game
{
    public class SlotPosition : MonoBehaviour
    {
        private GameSettings _gameSettings;
        
        public Slot CurrentSlot { get; private set; }
        public int Column { get; private set; }
        public int Row { get; private set; }

        [Inject]
        public void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        
        public void SetPosition(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public void SetCurrentSlot(Slot slot)
        {
            CurrentSlot = slot;
            
            CurrentSlot.transform.SetParent(transform);
            CurrentSlot.transform.localScale = _gameSettings.SlotsScale;
        }
    }
}