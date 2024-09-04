using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Common.StateMachine
{
    public abstract class StateMachine : IStateMachine
    {
        public event Action<IExitableState> OnStateChanged;
        public IExitableState CurrentState { get; private set; }
        private readonly Dictionary<Type, IExitableState> _states = new();

        protected StateMachine(params IExitableState[] states)
        {
            foreach (var state in states)
            {
                state.Initialize(this);
                _states.Add(state.GetType(), state);
            }
        }

        public async UniTask Enter<TState>() where TState : class, IState
        {
            var newState = await ChangeState<TState>();

            Debug.Log($"[{GetType()}] Enter: {typeof(TState).Name}");
            await newState.Enter();
        }

        public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>
        {
            var newState = await ChangeState<TState>();
            
            Debug.Log($"[{GetType()}] Enter: {typeof(TState).Name}");
            await newState.Enter(payload);
        }

        private async UniTask<TState> ChangeState<TState>() where TState : class, IExitableState
        {
            if (CurrentState != null)
            {
                Debug.Log($"[{GetType()}] Exit: {CurrentState.GetType().Name}");
                await CurrentState.Exit();
            }

            var state = GetState<TState>();
            CurrentState = state;
            OnStateChanged?.Invoke(CurrentState);
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}