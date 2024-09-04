using System;
using UI.ViewElements;

namespace UI.ClockPresenters
{
    public interface IDigitalClockPresenter
    {
        void Init(DigitalClockViewElements digitalClockViewElements);
        void UpdateClock(DateTime time);
        void Dispose();
    }
}