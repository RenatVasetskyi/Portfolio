using Observer.Interfaces;

namespace Observer
{
    public class Person1 : IObserver
    {
        public void GetInfo(ISubject subject)
        {
            if (subject is IStore store)
            {
                store.Sale();
                
                store.UnSubscribe(this);
            }
        }
    }
}