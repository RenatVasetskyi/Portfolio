using UnityEngine;

namespace Proxy
{
    public class DataBase : IDataBase
    {
        public void Connect()
        {
            Debug.Log("Data base connected");            
        }
    }
}