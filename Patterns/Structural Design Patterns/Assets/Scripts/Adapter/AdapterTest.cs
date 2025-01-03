using UnityEngine;

namespace Adapter
{
    public class AdapterTest : MonoBehaviour
    {
        private void Awake()
        {
            OtherDataBase otherDataBase = new OtherDataBase();

            IDataBase dataBaseAdapter = new DataBaseAdapter(otherDataBase);

            DataBaseConnector dataBaseConnector = new DataBaseConnector();

            dataBaseConnector.Connect(dataBaseAdapter);
        }
    }
}