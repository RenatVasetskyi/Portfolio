namespace Observer.Interfaces
{
    public interface ISubject
    {
        void Subscribe(IObserver observer);
        void UnSubscribe(IObserver observer);
        void Notify();
    }
}