using System;
using Common.StateMachine;
using Cysharp.Threading.Tasks;
using Models;

namespace StateMachines.AppStateMachine.States
{
    public class MainState : State
    {
        private readonly ICurrentTimeModel _currentTimeModel;

        public MainState(ICurrentTimeModel currentTimeModel)
        {
            _currentTimeModel = currentTimeModel;
        }
        
        public override async UniTask Enter()
        {
            await _currentTimeModel.Init();
            _currentTimeModel.StartSynchronizeTimeEveryInterval(TimeSpan.FromHours(1));
        }

        public override UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}