using UnityEngine;

namespace UI.Fortune
{
    public class FortuneWheelWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _victoryWindow;

        private void OnEnable()
        {
            _victoryWindow.SetActive(false);
        }
    }
}