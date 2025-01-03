using Assets.Scripts.Architecture.States;
using Assets.Scripts.Architecture.States.Interfaces;
using Zenject;

namespace Assets.Scripts.Architecture.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInterfaces();
            BindStateMachine();
            BindStates();
            AddStatesToStateMachine();
        }

        public void Initialize() =>
            Container.Resolve<IStateMachine>().Enter<LoadMainMenuState>();

        private void BindStates()
        {
            Container.Bind<LoadMainMenuState>().AsSingle();
            Container.Bind<LoadGameState>().AsSingle();
            Container.Bind<GameOverState>().AsSingle();
        }

        private void AddStatesToStateMachine()
        {
            IStateMachine stateMachine = Container.Resolve<IStateMachine>();

            stateMachine.States.Add(typeof(LoadMainMenuState), Container.Resolve<LoadMainMenuState>());
            stateMachine.States.Add(typeof(LoadGameState), Container.Resolve<LoadGameState>());
            stateMachine.States.Add(typeof(GameOverState), Container.Resolve<GameOverState>());
        }

        private void BindStateMachine()
        {
            Container
                .Bind<IStateMachine>()
                .To<StateMachine>()
                .AsSingle();
        }

        private void BindInterfaces()
        {
            Container
                .BindInterfacesTo<BootstrapInstaller>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();
        }
    }
}