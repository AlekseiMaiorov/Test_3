using System;
using System.Text;
using Models;
using UI.ViewElements;
using UnityEngine;

namespace UI.ClockPresenters
{
    public class AlarmPresenter : IDisposable, IAlarmPresenter
    {
        public event Action OnBeginAlarmSetup;
        public event Action OnEndAlarmSetup;
        private AlarmViewElements _alarmViewElements;
        private GameObject _inputGroup;
        private GameObject _alarmInfoGroup;
        private readonly IAlarmModel _alarmModel;
        private readonly StringBuilder _stringBuilder;

        public AlarmPresenter(IAlarmModel alarmModel)
        {
            _alarmModel = alarmModel;
            _stringBuilder = new StringBuilder();
        }

        public void Init(AlarmViewElements alarmViewElements)
        {
            _alarmViewElements = alarmViewElements;
            _inputGroup = alarmViewElements.InputField.transform.parent.gameObject;
            _alarmInfoGroup = alarmViewElements.AlarmText.transform.parent.gameObject;

            _alarmModel.OnAlarmRinging += DeleteAlarm;
            _alarmViewElements.BeginAlarmSetup.onClick.AddListener(BeginAlarmSetup);
            _alarmViewElements.SetupAlarm.onClick.AddListener(SetupAlarm);
            _alarmViewElements.DeleteAlarm.onClick.AddListener(DeleteAlarm);

            SetVisibleWaitAlarmViewElements();
        }

        private void DeleteAlarm()
        {
            _alarmModel.StopAlarm();
            SetVisibleWaitAlarmViewElements();
        }

        public void SetupInputFieldText(DateTime time)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(time.Hour.ToString("D2"));
            _stringBuilder.Append(":");
            _stringBuilder.Append(time.Minute.ToString("D2"));

            _alarmViewElements.InputField.text = _stringBuilder.ToString();
        }

        private void SetupAlarm()
        {
            bool isSetAlarmTime = TryParseExact(_alarmViewElements.InputField.text, out DateTime alarmTime);

            if (!isSetAlarmTime)
            {
                return;
            }

            _alarmModel.SetAlarmTime(alarmTime);
            _alarmModel.StartAlarm().Forget();
            _alarmViewElements.AlarmText.text = _alarmViewElements.InputField.text;
            _alarmViewElements.InputField.text = "";
            SetVisibleDeleteViewElements();
            OnEndAlarmSetup?.Invoke();
        }

        private void SetVisibleInputViewElements()
        {
            _alarmViewElements.BeginAlarmSetup.gameObject.SetActive(false);
            _inputGroup.SetActive(true);
            _alarmInfoGroup.SetActive(false);
        }

        private static bool TryParseExact(string timeString, out DateTime dateTime)
        {
            bool isParseSuccess = DateTime.TryParseExact(timeString,
                                                         "HH:mm",
                                                         null,
                                                         System.Globalization.DateTimeStyles.None,
                                                         out dateTime);

            if (!isParseSuccess)
            {
                Debug.LogError($"Ошибка преобразования строки в DateTime. Входная строка: {timeString}");
            }

            return isParseSuccess;
        }

        private void BeginAlarmSetup()
        {
            OnBeginAlarmSetup?.Invoke();
            SetVisibleInputViewElements();
        }

        private void SetVisibleDeleteViewElements()
        {
            _alarmViewElements.BeginAlarmSetup.gameObject.SetActive(false);
            _inputGroup.SetActive(false);
            _alarmInfoGroup.SetActive(true);
        }

        public void SetVisibleWaitAlarmViewElements()
        {
            _alarmViewElements.BeginAlarmSetup.gameObject.SetActive(true);
            _inputGroup.SetActive(false);
            _alarmInfoGroup.SetActive(false);
        }

        public void Dispose()
        {
            _alarmModel.OnAlarmRinging -= DeleteAlarm;
            _alarmViewElements.SetupAlarm.onClick.RemoveAllListeners();
            _alarmViewElements.SetupAlarm.onClick.RemoveAllListeners();
            _alarmViewElements.BeginAlarmSetup.onClick.RemoveAllListeners();
        }
    }
}