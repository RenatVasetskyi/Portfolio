using UnityEngine;

namespace Decorator
{
    public class EmailNotifyDecorator : ISender
    {
        private readonly ISender _sender;

        public EmailNotifyDecorator(ISender sender)
        {
            _sender = sender;
        }
        
        public void Send()
        {
            _sender.Send();
            
            Debug.Log("Decorated email notify sender");
        }
    }
}