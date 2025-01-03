using UnityEngine;

namespace Decorator
{
    public class EmailNotifySender : ISender
    {
        public void Send()
        {
            Debug.Log("Base notify sender");
        }
    }
}