using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ViewElements
{
    public class AlarmViewElements: MonoBehaviour
    {
        public TextMeshProUGUI AlarmText => _alarmText;
        public Button DeleteAlarm => _deleteAlarm;
        public TMP_InputField  InputField => _inputInputField;
        public Button SetupAlarm => _setupAlarm;
        public Button BeginAlarmSetup => _beginAlarmSetup;
        
        [SerializeField]
        private TextMeshProUGUI _alarmText;
        [SerializeField]
        private Button _deleteAlarm;
        [SerializeField]
        private TMP_InputField  _inputInputField;
        [SerializeField]
        private Button _setupAlarm;
        [SerializeField]
        private Button _beginAlarmSetup;
    }
}