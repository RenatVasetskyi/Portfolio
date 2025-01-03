using UnityEngine;

namespace Mediator
{
    public class MenuWindow : BaseWindow
    {
        public void Update()
        {
            Debug.Log("Menu view updated");
            
            _mediator.Notify(this, MainMenuOperationType.OpenSelectNameWindow);
        }
    }
}