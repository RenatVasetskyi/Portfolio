using Mediator.Interfaces;
using UnityEngine;

namespace Mediator
{
    public class BaseWindow
    {
        protected IMediator _mediator;

        protected BaseWindow(IMediator mediator = null)
        {
            _mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Open()
        {
            Debug.Log($"Open: {GetType()}");
        }
        
        public void Hide()
        {
            Debug.Log($"Hide: {GetType()}");
        }
    }
}