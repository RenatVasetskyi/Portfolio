using UnityEngine;

namespace Mediator
{
    public class SettingsWindow : BaseWindow  
    {
        public void SetSettings()
        {
            Debug.Log("Settings set");

            _mediator.Notify(this, MainMenuOperationType.OpenMainMenu);
        }
    }
}