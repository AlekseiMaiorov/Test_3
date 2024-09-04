using System;
using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;
using UnityEngine.Networking;

namespace Services.Time
{
    public abstract class TimeService : ITimeService
    {
        protected string _apiKey;
        protected string _url;

        protected TimeService(string url) : this(url, null)
        {
            _url = url;
        }

        protected TimeService(string url, string apiKey)
        {
            _url = url;
            _apiKey = apiKey;
        }

        public DateTime GetSystemTime()
        {
            return DateTime.Now;
        }

        protected virtual async UniTask<string> GetNetworkTimeAsync()
        {
            try
            {
                Debug.Log("Запрос времени c интернет сервиса");
                using (UnityWebRequest webRequest = UnityWebRequest.Get(_url))
                {
                    if (_apiKey != null)
                    {
                        webRequest.SetRequestHeader("X-Api-Key", _apiKey);
                    }

                    var response = await webRequest.SendWebRequest();

                    if (response.result == UnityWebRequest.Result.Success)
                    {
                        string json = webRequest.downloadHandler.text;

                        Debug.Log($"Получено время из сервиса, время: {json}");
                        return json;
                    }

                    Debug.LogError($"Ошибка получения времени {_url}: {webRequest.error}");
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка выполнения запроса времени {_url}: {e.Message}");
                return null;
            }
        }

        public virtual async UniTask<DateTime?> GetCurrentNetworkTimeAsync()
        {
            var json = await GetNetworkTimeAsync();

            if (json == null)
            {
                return null;
            }

            var dateTime = ParseTimeFromJson(json);
            return dateTime;
        }

        protected abstract DateTime ParseTimeFromJson(string json);
    }
}