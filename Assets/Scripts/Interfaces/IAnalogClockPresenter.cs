using System;
using Coordinators;
using UI.ViewElements;

namespace UI.ClockPresenters
{
    public interface IAnalogClockPresenter
    {
        IAnalogClockInput ClockInput { get; }
        void Init(AnalogClockViewElements analogClockViewElements, IAnalogClockInput analogClockInput);
        void UpdateClock(DateTime time);
        void EnableManualInput();
        void DisableManualInput();
        void Dispose();
    }
}