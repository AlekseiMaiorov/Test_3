using Cysharp.Threading.Tasks;

namespace Common.StateMachine
{
    public abstract class State : IState
    {
        protected StateMachine _stateMachine;

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public abstract UniTask Enter();
        public abstract UniTask Exit();
    }
}