using Code.MainInfrastructure.StateMachine;
using Code.MainInfrastructure.StateMachine.Interfaces;
using Code.MainInfrastructure.StateMachine.States;
using Zenject;

namespace Code.MainInfrastructure
{
    public class ZenjectStateCreator : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindMyself();
            BindGameStateMachine();
            CreateAllStates();
            AddStateToStateMachine();
        }

        public void Initialize()
        {
            Container.Resolve<IStateMachine>().EnterState<AdjustSettingsState>();
        }

        private void CreateAllStates()
        {
            Container.Bind<AdjustSettingsState>().AsSingle();
            Container.Bind<LoadServicesState>().AsSingle();
            Container.Bind<LoadMenuState>().AsSingle();
            Container.Bind<LoadGameplayState>().AsSingle();
        }

        private void AddStateToStateMachine()
        {
            IStateMachine stateMachine = Container.Resolve<IStateMachine>();
            
            stateMachine.AddState<AdjustSettingsState>(Container.Resolve<AdjustSettingsState>());
            stateMachine.AddState<LoadServicesState>(Container.Resolve<LoadServicesState>());
            stateMachine.AddState<LoadMenuState>(Container.Resolve<LoadMenuState>());
            stateMachine.AddState<LoadGameplayState>(Container.Resolve<LoadGameplayState>());
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
        }

        private void BindMyself()
        {
            Container
                .BindInterfacesTo<ZenjectStateCreator>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();
        }
    }
}