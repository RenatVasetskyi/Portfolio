namespace Command.Interfaces
{
    public interface ICancellableCommand : ICommand
    {
        void Undo();
    }
}