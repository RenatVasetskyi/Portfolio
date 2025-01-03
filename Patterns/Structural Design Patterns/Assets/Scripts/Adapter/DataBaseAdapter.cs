namespace Adapter
{
    public class DataBaseAdapter : IDataBase
    {
        private readonly OtherDataBase _otherDataBase;

        public DataBaseAdapter(OtherDataBase otherDataBase)
        {
            _otherDataBase = otherDataBase;
        }

        public void GetInfo()
        {
            _otherDataBase.GetInfo();            
        }
    }
}