namespace Game.Interfaces
{
    public interface IUIMediator
    {
        void Notify<T>(T state);
    }
}