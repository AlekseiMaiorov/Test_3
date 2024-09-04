using System;
using Coordinators;
using Models;
using UI.ViewElements;
using UnityEngine;

namespace UI.ClockPresenters
{
    public class AnalogClockPresenter : IDisposable, IAnalogClockPresenter
    {
        public IAnalogClockInput ClockInput => _analogClockInput;
        
        private readonly ICurrentTimeModel _currentTimeModel;
        private AnalogClockViewElements _analogClockViewElements;
        private IAnalogClockInput _analogClockInput;

        public AnalogClockPresenter(ICurrentTimeModel currentTimeModel)
        {
            _currentTimeModel = currentTimeModel;
        }


        public void Init(AnalogClockViewElements analogClockViewElements, IAnalogClockInput analogClockInput)
        {
            _analogClockInput = analogClockInput;
            _analogClockViewElements = analogClockViewElements;
            _currentTimeModel.OnModelDataChange += UpdateClock;
        }

        public void UpdateClock(DateTime time)
        {
            float hoursAngle = (time.Hour % 12) * 30f;
            float minutesAngle = time.Minute * 6f;
            float secondsAngle = time.Second * 6f;

            _analogClockViewElements.HourHand.transform.localRotation = Quaternion.Euler(0f, 0f, -hoursAngle);
            _analogClockViewElements.MinuteHand.transform.localRotation = Quaternion.Euler(0f, 0f, -minutesAngle);
            _analogClockViewElements.SecondHand.transform.localRotation = Quaternion.Euler(0f, 0f, -secondsAngle);
        }

        public void EnableManualInput()
        {
            _analogClockInput.EnableInput(true);
            _currentTimeModel.OnModelDataChange -= UpdateClock;
            _analogClockViewElements.SecondHand.gameObject.SetActive(false);
            Debug.LogWarning("Инпут аналоговых часов включен");
        }
        
        public void DisableManualInput()
        {
            _analogClockInput.EnableInput(false);
            _currentTimeModel.OnModelDataChange += UpdateClock;
            _analogClockViewElements.SecondHand.gameObject.SetActive(true);
            Debug.LogWarning("Инпут аналоговых часов выключен");
        }
        
        public void Dispose()
        {
            _currentTimeModel.OnModelDataChange -= UpdateClock;
        }
    }
}