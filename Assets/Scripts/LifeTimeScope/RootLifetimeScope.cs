using Coordinators;
using EntryPoint;
using Factories;
using Models;
using Services.AssetManagement;
using Services.Time;
using StateMachines.AppStateMachine;
using StateMachines.AppStateMachine.States;
using UI.ClockPresenters;
using VContainer;
using VContainer.Unity;

namespace LifeTimeScope
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<AppEntryPoint>();
            builder.Register<AnalogClockAlarmCoordinator>(Lifetime.Singleton).AsImplementedInterfaces();
            RegisterAppStateMachine(builder);
            RegisterServices(builder);
            RegisterPresenters(builder);
            RegisterModels(builder);
            RegisterFactories(builder);
        }

        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<ClockFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AlarmFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterModels(IContainerBuilder builder)
        {
            builder.Register<CurrentTimeModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AlarmModel>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterPresenters(IContainerBuilder builder)
        {
            builder.Register<AnalogClockPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<DigitalClockPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AlarmPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterAppStateMachine(IContainerBuilder builder)
        {
            builder.Register<AppStateMachine>(Lifetime.Singleton);
            builder.Register<BootstrapState>(Lifetime.Singleton);
            builder.Register<MainState>(Lifetime.Singleton);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<SimpleAssetLoader>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimeApiService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ApiNinjasService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}