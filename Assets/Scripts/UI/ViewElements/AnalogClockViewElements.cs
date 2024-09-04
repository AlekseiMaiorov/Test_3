using UnityEngine;
using UnityEngine.UI;

namespace UI.ViewElements
{
    public class AnalogClockViewElements : MonoBehaviour
    {
        public Image HourHand => _hourHand;
        public Image MinuteHand => _minuteHand;
        public Image SecondHand => _secondHand;
        
        [SerializeField]
        private Image _hourHand;
        [SerializeField]
        private Image _minuteHand;
        [SerializeField]
        private Image _secondHand;
    }
}