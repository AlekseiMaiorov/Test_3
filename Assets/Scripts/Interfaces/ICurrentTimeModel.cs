using System;
using Cysharp.Threading.Tasks;

namespace Models
{
    public interface ICurrentTimeModel
    {
        event Action<DateTime> OnModelDataChange;
        DateTime? CurrentTime { get; }
        UniTask Init();
        void StartSynchronizeTimeEveryInterval(TimeSpan timeInterval);
        void StopSynchronizeTimeEveryInterval();
        void Dispose();
    }
}