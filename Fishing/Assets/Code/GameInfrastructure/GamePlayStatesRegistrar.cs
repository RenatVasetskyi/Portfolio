using Code.GameInfrastructure.GameStatesManaging;
using Code.GameInfrastructure.GameStatesManaging.GameStates;
using Code.GameInfrastructure.GameStatesManaging.Interfaces;
using Zenject;

namespace Code.GameInfrastructure
{
    public class GamePlayStatesRegistrar : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            RegisterAllInterfacesToMe();
            RegisterGameStateMachine();
            RegisterAllStates();
            AddStates();
        }

        public void Initialize()
        {
            Container.Resolve<IStateMachine>().EnterState<SetPreloadSettingsState>();
        }

        private void RegisterAllStates()
        {
            Container.Bind<SetPreloadSettingsState>().AsSingle();
            Container.Bind<InitializeServicesGameState>().AsSingle();
            Container.Bind<LoadMenuGameState>().AsSingle();
            Container.Bind<LoadPlayingGameState>().AsSingle();
        }

        private void AddStates()
        {
            IStateMachine stateMachine = Container.Resolve<IStateMachine>();
            
            stateMachine.AddState<SetPreloadSettingsState>(Container.Resolve<SetPreloadSettingsState>());
            stateMachine.AddState<InitializeServicesGameState>(Container.Resolve<InitializeServicesGameState>());
            stateMachine.AddState<LoadMenuGameState>(Container.Resolve<LoadMenuGameState>());
            stateMachine.AddState<LoadPlayingGameState>(Container.Resolve<LoadPlayingGameState>());
        }

        private void RegisterGameStateMachine()
        {
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
        }

        private void RegisterAllInterfacesToMe()
        {
            Container
                .BindInterfacesTo<GamePlayStatesRegistrar>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();
        }
    }
}