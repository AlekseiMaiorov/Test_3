using TMPro;
using UnityEngine;

namespace UI.ViewElements
{
    public class DigitalClockViewElements : MonoBehaviour
    {
        public TextMeshProUGUI DigitalTime => _digitalTime;

        [SerializeField]
        private TextMeshProUGUI _digitalTime;
    }
}