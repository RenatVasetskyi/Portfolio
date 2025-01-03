using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Shop.Pages
{
    public class MultipageWindow : MonoBehaviour
    {
        [SerializeField] private List<OpenWindowToggle> _toggles;
        
        public void OnToggleStateChanged(OpenWindowToggle notifier)
        {
            List<OpenWindowToggle> togglesToDisable = new(_toggles);
            
            togglesToDisable.Remove(notifier);

            foreach (OpenWindowToggle toggle in togglesToDisable)
                toggle.Disable();
        }

        private void Awake()
        {
            if (_toggles.Count > 0)
            {
                OpenWindowToggle firstToggle = _toggles.First();
                firstToggle.Enable();
                OnToggleStateChanged(firstToggle);
            }
        }
    }
}