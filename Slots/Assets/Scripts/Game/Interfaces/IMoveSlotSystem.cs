namespace Game.Interfaces
{
    public interface IMoveSlotSystem
    {
        bool IsStopped { get; }
        void Spin(float moveSlotDuration);
    }
}