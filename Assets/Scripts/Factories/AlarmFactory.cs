using Cysharp.Threading.Tasks;
using Interfaces;
using Services.AssetManagement;
using UI.ClockPresenters;
using UI.ViewElements;
using UnityEngine;

namespace Factories
{
    public class AlarmFactory : IAlarmFactory<GameObject>
    {
        private readonly IAssetLoader _assetLoader;
        private readonly IAlarmPresenter _alarmPresenter;

        public AlarmFactory(
            IAssetLoader assetLoader,
            IAlarmPresenter alarmPresenter)
        {
            _alarmPresenter = alarmPresenter;
            _assetLoader = assetLoader;
        }

        public async UniTask<GameObject> Create()
        {
            var alarmCanvasPrefab = await _assetLoader.LoadAssetAsync<GameObject>(AssetKeys.ALARM_CLOCK);
            var alarmCanvasGameObject = Object.Instantiate(alarmCanvasPrefab, null);

            var alarmViewElements = alarmCanvasGameObject.GetComponentInChildren<AlarmViewElements>();

            _alarmPresenter.Init(alarmViewElements);

            return alarmCanvasGameObject;
        }
    }
}