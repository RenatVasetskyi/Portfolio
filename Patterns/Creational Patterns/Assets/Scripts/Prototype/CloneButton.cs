using UnityEngine;
using UnityEngine.UI;

namespace Prototype
{
    public class CloneButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [SerializeField] private Player _playerPrefab;

        private ICloneable _createdPlayer;

        private void Awake()
        {
            _createdPlayer = Instantiate(_playerPrefab);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Clone);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Clone);
        }

        private void Clone()
        {
            _createdPlayer.Clone();
        }
    }
}