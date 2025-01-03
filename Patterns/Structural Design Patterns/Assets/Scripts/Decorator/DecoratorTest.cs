using UnityEngine;

namespace Decorator
{
    public class DecoratorTest : MonoBehaviour
    {
        private void Awake()
        {
            ISender emailSender = new EmailNotifySender();

            ISender notifyDecorator = new EmailNotifyDecorator(emailSender);
            
            notifyDecorator.Send();

            ISender smsSender = new SmsSender(notifyDecorator);
            
            smsSender.Send();
        }
    }
}