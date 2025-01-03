using System.Linq;
using Code.Infrastructure.Interfaces;

namespace Code.Infrastructure
{
    public class PlayerDataService : IPlayerDataSocket
    {
        private const string MovementSpeedPath = "Movement Speed";
        private const string JumpPowerPath = "Jump Power";
        
        private readonly IPlayerPrefsController _playerPrefsController;
        private readonly Data _data;

        public int MovementSpeed { get; private set; }
        public int JumpPower { get; private set; }

        public PlayerDataService(IPlayerPrefsController playerPrefsController, Data data)
        {
            _playerPrefsController = playerPrefsController;
            _data = data;
        }
        
        public void SetMovementSpeed(int speed)
        {
            MovementSpeed = speed;
            _playerPrefsController.Int(MovementSpeedPath, speed);
        }

        public void SetJumpPower(int power)
        {
            JumpPower = power;
            _playerPrefsController.Int(JumpPowerPath, power);
        }

        public void LoadData()
        {
            MovementSpeed = _playerPrefsController.Path(MovementSpeedPath) ? _playerPrefsController.Int(MovementSpeedPath) : _data.MovementSpeed.First().Value;
            JumpPower = _playerPrefsController.Path(JumpPowerPath) ? _playerPrefsController.Int(JumpPowerPath) : _data.JumpPower.First().Value;
        }
    }
}