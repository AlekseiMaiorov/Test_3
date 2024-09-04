using System;
using UI.ViewElements;

namespace Coordinators
{
    public interface IAnalogClockInput
    {
        event Action<DateTime?> OnChangedClock;
        DateTime? Time { get; }
        void Init(AnalogClockViewElements analogClockViewElements);
        void EnableInput(bool isEnable);
    }
}