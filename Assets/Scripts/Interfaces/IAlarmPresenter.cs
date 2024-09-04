using System;
using UI.ViewElements;

namespace UI.ClockPresenters
{
    public interface IAlarmPresenter
    {
        event Action OnBeginAlarmSetup;
        event Action OnEndAlarmSetup;
        void Init(AlarmViewElements alarmViewElements);
        void SetupInputFieldText(DateTime time);
        void SetVisibleWaitAlarmViewElements();
        void Dispose();
    }
}