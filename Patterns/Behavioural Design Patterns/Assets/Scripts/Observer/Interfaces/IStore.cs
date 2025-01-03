namespace Observer.Interfaces
{
    public interface IStore : ISubject
    {
        void Sale();
        void GetNewGoods();
    }
}