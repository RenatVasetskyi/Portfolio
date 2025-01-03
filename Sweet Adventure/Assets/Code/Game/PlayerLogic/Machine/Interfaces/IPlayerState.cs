namespace Code.Game.PlayerLogic.Machine.Interfaces
{
    public interface IPlayerState
    {
        void Enter();
        void Exit();
        void OnUpdate();
        void OnFixedUpdate();
    }
}