using Coordinators;
using Cysharp.Threading.Tasks;
using Interfaces;
using Services.AssetManagement;
using UI.ClockPresenters;
using UI.ViewElements;
using UnityEngine;

namespace Factories
{
    public class ClockFactory : IClockFactory<GameObject>
    {
        private readonly IAssetLoader _assetLoader;
        private readonly IDigitalClockPresenter _digitalClockPresenter;
        private readonly IAnalogClockPresenter _analogClockPresenter;

        public ClockFactory(
            IAssetLoader assetLoader,
            IAnalogClockPresenter analogClockPresenter,
            IDigitalClockPresenter digitalClockPresenter)
        {
            _analogClockPresenter = analogClockPresenter;
            _digitalClockPresenter = digitalClockPresenter;
            _assetLoader = assetLoader;
        }

        public async UniTask<GameObject> Create()
        {
            var clockCanvasPrefab = await _assetLoader.LoadAssetAsync<GameObject>(AssetKeys.CLOCK);
            var clockCanvasGameObject = Object.Instantiate(clockCanvasPrefab, null);

            var analogClockInput = clockCanvasGameObject.GetComponentInChildren<IAnalogClockInput>();
            var analogClockViewElements = clockCanvasGameObject.GetComponentInChildren<AnalogClockViewElements>();
            var digitalClockViewElements = clockCanvasGameObject.GetComponentInChildren<DigitalClockViewElements>();

            _analogClockPresenter.Init(analogClockViewElements, analogClockInput);
            analogClockInput.Init(analogClockViewElements);
            _digitalClockPresenter.Init(digitalClockViewElements);

            return clockCanvasGameObject;
        }
    }
}