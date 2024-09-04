using System;
using Cysharp.Threading.Tasks;

namespace Models
{
    public interface IAlarmModel
    {
        event Action<DateTime?> OnModelDataChange;
        event Action OnAlarmRinging;
        DateTime? AlarmTime { get; }
        void SetAlarmTime(DateTime alarmTime);
        UniTaskVoid StartAlarm();
        void StopAlarm();
        void RemoveAlarm();
    }
}