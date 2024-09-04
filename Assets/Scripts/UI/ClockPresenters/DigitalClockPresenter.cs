using System;
using System.Text;
using Models;
using UI.ViewElements;

namespace UI.ClockPresenters
{
    public class DigitalClockPresenter : IDisposable, IClockPresenter, IDigitalClockPresenter
    {
        private readonly ICurrentTimeModel _currentTimeModel;
        private readonly StringBuilder _stringBuilder;
        private DigitalClockViewElements _digitalClockViewElements;

        public DigitalClockPresenter(ICurrentTimeModel currentTimeModel)
        {
            _currentTimeModel = currentTimeModel;
            _stringBuilder = new StringBuilder();
            _currentTimeModel.OnModelDataChange += UpdateClock;
        }

        public void Init(DigitalClockViewElements digitalClockViewElements)
        {
            _digitalClockViewElements = digitalClockViewElements;
        }

        public void UpdateClock(DateTime time)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(time.Hour.ToString("D2"));
            _stringBuilder.Append(" : ");
            _stringBuilder.Append(time.Minute.ToString("D2"));
            _stringBuilder.Append(" : ");
            _stringBuilder.Append(time.Second.ToString("D2"));

            _digitalClockViewElements.DigitalTime.text = _stringBuilder.ToString();
        }

        public void Dispose()
        {
            _currentTimeModel.OnModelDataChange -= UpdateClock;
        }
    }
}