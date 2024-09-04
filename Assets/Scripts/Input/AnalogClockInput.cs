using System;
using UI.ViewElements;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Coordinators
{
    public class AnalogClockInput : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler, IAnalogClockInput
    {
        public event Action<DateTime?> OnChangedClock;
        public DateTime? Time => _time;

        private const float HOURS_PER_CIRCLE = 12f;
        private const float DEGREES_PER_HOUR = 30f;
        private const float DEGREES_PER_MINUTE = 6f;
        
        private AnalogClockViewElements _analogClockViewElements;
        private RectTransform _currentHand;
        private bool _isEnableInput;
        private DateTime? _time;
        private bool _isOver12Hours;
        private int _tempPreviousHours;

        public void Init(AnalogClockViewElements analogClockViewElements)
        {
            _analogClockViewElements = analogClockViewElements;
            _time = new DateTime();
        }

        public void EnableInput(bool isEnable)
        {
            _isEnableInput = isEnable;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isEnableInput)
            {
                return;
            }

            RectTransform hitTransform = null;

            if (eventData.pointerCurrentRaycast.gameObject == _analogClockViewElements.HourHand.gameObject)
            {
                hitTransform = _analogClockViewElements.HourHand.rectTransform;
            }
            else if (eventData.pointerCurrentRaycast.gameObject == _analogClockViewElements.MinuteHand.gameObject)
            {
                hitTransform = _analogClockViewElements.MinuteHand.rectTransform;
            }

            _currentHand = hitTransform;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isEnableInput ||
                _currentHand == null)
            {
                return;
            }

            var currentHandParent = _currentHand.parent as RectTransform;

            if (currentHandParent == null)
            {
                return;
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle(currentHandParent,
                                                                    eventData.position,
                                                                    null,
                                                                    out Vector2 localPoint);

            Vector2 direction = localPoint - currentHandParent.rect.center;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            _currentHand.localRotation = Quaternion.Euler(0, 0, -angle);
            OnChangedClock?.Invoke(GetTimeFromAngles24H());
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isEnableInput)
            {
                return;
            }

            _currentHand = null;
        }

        private DateTime? GetTimeFromAngles24H()
        {
            if (!_isEnableInput)
            {
                Debug.LogWarning("Инпут аналоговых часов выключен");
                return null;
            }

            float hourAngle = _analogClockViewElements.HourHand.rectTransform.localRotation.eulerAngles.z;
            float minuteAngle = _analogClockViewElements.MinuteHand.rectTransform.localRotation.eulerAngles.z;

            int hours = Mathf.FloorToInt(((360 - hourAngle) / DEGREES_PER_HOUR) % HOURS_PER_CIRCLE);
            int minutes = Mathf.FloorToInt((360 - minuteAngle) / DEGREES_PER_MINUTE);

            if (minutes == 60)
            {
                minutes = 0;
            }

            var today = DateTime.Today;

            if (_tempPreviousHours != hours)
            {
                _tempPreviousHours = hours;

                if (hours == 0 &&
                    !_isOver12Hours)
                {
                    hours = 12;
                    _isOver12Hours = true;
                    _time = today.Add(new TimeSpan(hours, minutes, 0));

                    return _time;
                }

                if (hours == 0 && _isOver12Hours)
                {
                    _isOver12Hours = false;
                }
            }

            if (_isOver12Hours)
            {
                hours += 12;
            }

            _time = today.Add(new TimeSpan(hours, minutes, 0));
            return _time;
        }
    }
}