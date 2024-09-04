using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;

namespace Models
{
    public class CurrentTimeModel : IDisposable, ICurrentTimeModel
    {
        public event Action<DateTime> OnModelDataChange;
        public DateTime? CurrentTime => _currentTime;

        private DateTime? _currentTime;
        private readonly ITimeService[] _timeServices;
        private CancellationTokenSource _ctsSynchronizeTime;
        private CancellationTokenSource _ctsUpdateEverySecondTime;
        private bool _isSynchronizeTimeEveryInterval;

        public CurrentTimeModel(IEnumerable<ITimeService> timeServices)
        {
            _timeServices = timeServices.ToArray();
            _ctsUpdateEverySecondTime = new CancellationTokenSource();
        }

        public async UniTask Init()
        {
            await SynchronizeTime();
            UpdateCurrentTimeEverySecond().Forget();
        }

        private async UniTaskVoid UpdateCurrentTimeEverySecond()
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: _ctsUpdateEverySecondTime.Token);

                if (_currentTime.HasValue)
                {
                    _currentTime = _currentTime.Value.AddSeconds(1);
                    OnModelDataChange?.Invoke((DateTime) _currentTime);
                }
            }
        }

        private async UniTask SynchronizeTime()
        {
            foreach (ITimeService timeService in _timeServices)
            {
                DateTime? currentTimeAsync = await timeService.GetCurrentNetworkTimeAsync();

                if (currentTimeAsync == null)
                {
                    continue;
                }

                _currentTime = currentTimeAsync;
                OnModelDataChange?.Invoke((DateTime) _currentTime);

                // Если достаточно одного валидног запроса
                // return;
            }

            _currentTime = _timeServices[0].GetSystemTime();
            OnModelDataChange?.Invoke((DateTime) _currentTime);
        }

        public void StartSynchronizeTimeEveryInterval(TimeSpan timeInterval)
        {
            if (_isSynchronizeTimeEveryInterval)
            {
                return;
            }

            _isSynchronizeTimeEveryInterval = true;
            _ctsSynchronizeTime = new CancellationTokenSource();
            SynchronizeTimeEveryInterval(timeInterval).Forget();
        }

        public void StopSynchronizeTimeEveryInterval()
        {
            if (_isSynchronizeTimeEveryInterval == false)
            {
                return;
            }

            _isSynchronizeTimeEveryInterval = false;
            _ctsSynchronizeTime.Cancel();
            _ctsSynchronizeTime.Dispose();
        }

        private async UniTask SynchronizeTimeEveryInterval(TimeSpan timeInterval)
        {
            while (_isSynchronizeTimeEveryInterval)
            {
                await UniTask.Delay(timeInterval, cancellationToken: _ctsSynchronizeTime.Token);
                await SynchronizeTime();
            }
        }

        public void Dispose()
        {
            if (_ctsSynchronizeTime != null)
            {
                _ctsSynchronizeTime.Cancel();
                _ctsSynchronizeTime.Dispose();
            }

            if (_ctsUpdateEverySecondTime != null)
            {
                _ctsUpdateEverySecondTime.Cancel();
                _ctsUpdateEverySecondTime.Dispose();
            }
        }
    }
}