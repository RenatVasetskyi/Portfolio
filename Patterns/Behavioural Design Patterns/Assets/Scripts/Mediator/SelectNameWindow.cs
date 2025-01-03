using UnityEngine;

namespace Mediator
{
    public class SelectNameWindow : BaseWindow
    {
        public void Select()
        {
            Debug.Log("Name selected");
            
            _mediator.Notify(this, MainMenuOperationType.NameSelected);    
        }
    }
}