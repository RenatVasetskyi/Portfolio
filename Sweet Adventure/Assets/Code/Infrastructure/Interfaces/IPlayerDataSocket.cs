namespace Code.Infrastructure.Interfaces
{
    public interface IPlayerDataSocket
    {
        int MovementSpeed { get; }
        int JumpPower { get; }
        void SetMovementSpeed(int speed);
        void SetJumpPower(int power);
        void LoadData();
    }
}