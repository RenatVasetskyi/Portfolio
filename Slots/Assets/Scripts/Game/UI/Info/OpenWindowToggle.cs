using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Info
{
    public class OpenWindowToggle : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private SlotsInfoWindow _slotsInfoWindow;

        public Toggle Toggle { get; private set; }

        public void Initialize()
        {
            Toggle = GetComponent<Toggle>();
            
            Toggle.onValueChanged.AddListener(Open);
        }
        
        public void Subscribe()
        {
            Toggle.onValueChanged.AddListener(Open);
        }

        public void Unsubscribe()
        {
            Toggle.onValueChanged.RemoveListener(Open);
        }
        
        private void Open(bool isOn)
        {
            _window.SetActive(isOn);
            
            if (isOn)
            {
                _slotsInfoWindow.OnToggleStateChanged(Toggle);
                Toggle.interactable = false;
            }
            else
            {
                Toggle.interactable = true;   
            }
        }
    }
}