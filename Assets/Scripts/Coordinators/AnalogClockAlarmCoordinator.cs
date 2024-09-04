using System;
using UI.ClockPresenters;

namespace Coordinators
{
    public interface IAnalogClockAlarmCoordinator
    {
        void Init();
        void Dispose();
    }

    public class AnalogClockAlarmCoordinator : IDisposable, IAnalogClockAlarmCoordinator
    {
        private readonly IAlarmPresenter _alarmPresenter;
        private readonly IAnalogClockPresenter _analogClockPresenter;

        public AnalogClockAlarmCoordinator(
            IAnalogClockPresenter analogClockPresenter,
            IAlarmPresenter alarmPresenter)
        {
            _analogClockPresenter = analogClockPresenter;
            _alarmPresenter = alarmPresenter;
        }

        public void Init()
        {
            _analogClockPresenter.ClockInput.OnChangedClock += ConvertNullableDateTimeForAlarmInput;
            _alarmPresenter.OnBeginAlarmSetup += _analogClockPresenter.EnableManualInput;
            _alarmPresenter.OnEndAlarmSetup += _analogClockPresenter.DisableManualInput;
        }

        private void ConvertNullableDateTimeForAlarmInput(DateTime? time)
        {
            _alarmPresenter.SetupInputFieldText(time.Value);
        }

        public void Dispose()
        {
            _analogClockPresenter.ClockInput.OnChangedClock += ConvertNullableDateTimeForAlarmInput;
            _alarmPresenter.OnBeginAlarmSetup -= _analogClockPresenter.EnableManualInput;
            _alarmPresenter.OnEndAlarmSetup -= _analogClockPresenter.DisableManualInput;
        }
    }
}