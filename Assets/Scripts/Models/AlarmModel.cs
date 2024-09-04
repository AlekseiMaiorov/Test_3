using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Models
{
    public class AlarmModel : IAlarmModel, IDisposable
    {
        public event Action<DateTime?> OnModelDataChange;
        public event Action OnAlarmRinging;
        public DateTime? AlarmTime => _alarmTime;

        private CancellationTokenSource _cts;
        private DateTime? _alarmTime;

        public void SetAlarmTime(DateTime alarmTime)
        {
            DateTime date = DateTime.Today;

            alarmTime = date.Add(alarmTime.TimeOfDay);

            DateTime now = DateTime.Now;

            if (alarmTime.TimeOfDay <= now.TimeOfDay)
            {
                alarmTime = alarmTime.AddDays(1);
            }

            _alarmTime = alarmTime;

            OnModelDataChange?.Invoke(_alarmTime);

            Debug.Log("Будильник установлен");
        }

        public async UniTaskVoid StartAlarm()
        {
            if (_cts != null)
            {
                return;
            }

            _cts = new CancellationTokenSource();

            while (!_cts.Token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: _cts.Token);

                var now = DateTime.Now;
                if (now.Hour == _alarmTime.Value.Hour &&
                    now.Minute == _alarmTime.Value.Minute)
                {
                    OnAlarmRinging?.Invoke();
                    Debug.Log("Звонит будильник!!!");
                }
            }

            _cts.Dispose();
            _cts = null;
        }

        public void StopAlarm()
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }
        }

        public void RemoveAlarm()
        {
            _alarmTime = null;
            OnModelDataChange?.Invoke(_alarmTime);
        }

        public void Dispose()
        {
            
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}