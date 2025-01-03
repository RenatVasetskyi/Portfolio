using UnityEngine;

namespace Decorator
{
    public class SmsSender : ISender
    {
        private readonly ISender _sender;

        public SmsSender(ISender sender)
        {
            _sender = sender;
        }
        
        public void Send()
        {
            _sender.Send();

            Debug.Log("Sms sender");
        }
    }
}