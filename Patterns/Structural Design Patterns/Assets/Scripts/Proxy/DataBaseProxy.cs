using UnityEngine;

namespace Proxy
{
    public class DataBaseProxy : IDataBase
    {
        private readonly IDataBase _dataBase;

        public DataBaseProxy(IDataBase dataBase)
        {
            _dataBase = dataBase;
        }
        
        public void Connect()
        {
            if (CheckInternet())
                _dataBase.Connect();
            else
                Debug.Log("No internet");
        }

        private bool CheckInternet()
        {
            if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                return true;

            return false;
        }
    }
}