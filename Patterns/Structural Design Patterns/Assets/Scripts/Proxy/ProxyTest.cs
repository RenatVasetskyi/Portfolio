using UnityEngine;

namespace Proxy
{
    public class ProxyTest : MonoBehaviour
    {
        private void Awake()
        {
            IDataBase proxy = new DataBaseProxy(new DataBase());
            proxy.Connect();
        }
    }
}