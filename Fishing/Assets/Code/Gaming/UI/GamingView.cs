using UnityEngine;

namespace Code.Gaming.UI
{
    public class GamingView : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        
        public GameController GameController => _gameController;
    }
}