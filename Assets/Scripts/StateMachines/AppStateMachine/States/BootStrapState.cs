using Common.StateMachine;
using Coordinators;
using Cysharp.Threading.Tasks;
using Factories;
using UnityEngine;

namespace StateMachines.AppStateMachine.States
{
    public class BootstrapState : State
    {
        private readonly IClockFactory<GameObject> _clockFactory;
        private readonly IAlarmFactory<GameObject> _alarmFactory;
        private IAnalogClockAlarmCoordinator _analogClockAlarmInput;

        public BootstrapState(
            IClockFactory<GameObject> clockFactory,
            IAlarmFactory<GameObject> alarmFactory,
            IAnalogClockAlarmCoordinator analogClockAlarmInput)
        {
            _analogClockAlarmInput = analogClockAlarmInput;
            _alarmFactory = alarmFactory;
            _clockFactory = clockFactory;
        }

        public override async UniTask Enter()
        {
            await _clockFactory.Create();
            await _alarmFactory.Create();
            _analogClockAlarmInput.Init();
            _stateMachine.Enter<MainState>().Forget();
        }

        public override UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}