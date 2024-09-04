using System;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace Interfaces
{
    public interface IAssetLoader
    {
        UniTask<T> LoadAssetAsync<T>(string key, Action onSuccess = null, Action onFailure = null)
            where T : Object;
    }
}