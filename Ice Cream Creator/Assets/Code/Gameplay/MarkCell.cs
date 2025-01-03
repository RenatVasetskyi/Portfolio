using UnityEngine;

namespace Code.Gameplay
{
    public class MarkCell : MonoBehaviour
    {
        [SerializeField] private PositionInGrid _positionInGrid;

        public PositionInGrid PositionInGrid => _positionInGrid;
    }
}