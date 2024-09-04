using Cysharp.Threading.Tasks;
using StateMachines.AppStateMachine;
using StateMachines.AppStateMachine.States;
using VContainer.Unity;

namespace EntryPoint
{
    public class AppEntryPoint : IInitializable
    {
        private readonly AppStateMachine _appStateMachine;

        public AppEntryPoint(AppStateMachine appStateMachine)
        {
            _appStateMachine = appStateMachine;
        }

        public void Initialize()
        {
            _appStateMachine.Enter<BootstrapState>().Forget();
        }
    }
}