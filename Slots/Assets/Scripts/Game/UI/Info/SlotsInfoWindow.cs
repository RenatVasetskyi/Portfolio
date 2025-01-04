using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Info
{
    public class SlotsInfoWindow : MonoBehaviour
    {
        [SerializeField] private OpenWindowToggle[] _toggles;
        
        public void OnToggleStateChanged(Toggle notifier)
        {
            List<Toggle> toggles = new();
            
            foreach (OpenWindowToggle toggle in _toggles)
                toggles.Add(toggle.Toggle);
            
            toggles.Remove(notifier);
            
            foreach (Toggle toggle in toggles)
            {
                toggle.isOn = false;
            }
        }

        private void Awake()
        {
            foreach (OpenWindowToggle toggle in _toggles)
            {
                toggle.Initialize();
                toggle.Subscribe();
            }
            
            OpenWindowToggle firstToggle = _toggles.FirstOrDefault();

            if (firstToggle != null)
            {
                firstToggle.Toggle.isOn = true;
            }
        }

        private void OnDestroy()
        {
            foreach (OpenWindowToggle toggle in _toggles)
            {
                toggle.Unsubscribe();
            }   
        }
    }
}