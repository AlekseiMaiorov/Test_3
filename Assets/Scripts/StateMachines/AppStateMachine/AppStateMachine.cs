using Common.StateMachine;
using StateMachines.AppStateMachine.States;

namespace StateMachines.AppStateMachine
{
    public class AppStateMachine : StateMachine
    {
        public AppStateMachine(BootstrapState bootstrapState, MainState mainState) :
            base(states: new IExitableState[] {bootstrapState, mainState})
        {
        }
    }
}