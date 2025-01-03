using UnityEngine;

namespace Adapter
{
    public class DataBaseConnector
    {
        public void Connect(IDataBase dataBase)
        { 
            Debug.Log("DataBase connected");
            
            dataBase.GetInfo();
        }
    }
}