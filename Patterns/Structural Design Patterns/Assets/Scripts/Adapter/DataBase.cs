using UnityEngine;

namespace Adapter
{
    public class DataBase : IDataBase
    {
        public void GetInfo()
        {
            Debug.Log("Get info from original database");
        }
    }
}