namespace Mediator.Interfaces
{
    public interface IMediator
    {
        void Notify(BaseWindow window, MainMenuOperationType operationType);
    }
}