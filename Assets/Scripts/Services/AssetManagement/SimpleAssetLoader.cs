using System;
using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Services.AssetManagement
{
    public class SimpleAssetLoader : IAssetLoader
    {
        public async UniTask<T> LoadAssetAsync<T>(string key, Action onSuccess = null, Action onFailure = null)
            where T : Object
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);

            try
            {
                T asset = await handle.ToUniTask();

                if (asset != null)
                {
                    onSuccess?.Invoke();
                    Debug.Log($"{asset.name} загружен");
                    return asset;
                }
                else
                {
                    onFailure?.Invoke();
                    Debug.LogError($"Ошибка при загрузке ассета с ключом: {key}");
                }
            }
            catch
            {
                onFailure?.Invoke();
                Debug.LogError($"Ошибка при загрузке ассета с ключом: {key}");
            }

            return null;
        }
    }
}