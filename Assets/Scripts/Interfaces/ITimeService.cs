using System;
using Cysharp.Threading.Tasks;

namespace Interfaces
{
    public interface ITimeService
    {
        public UniTask<DateTime?> GetCurrentNetworkTimeAsync();

        public DateTime GetSystemTime();
    }
}