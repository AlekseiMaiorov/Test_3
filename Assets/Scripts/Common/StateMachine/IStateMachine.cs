using System;
using Cysharp.Threading.Tasks;

namespace Common.StateMachine
{
    public interface IStateMachine
    {
        event Action<IExitableState> OnStateChanged; 
        UniTask Enter<TState>() where TState : class, IState;
        UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>;
    }
}