using System;
using UnityEngine;

namespace Services.Time
{
    public class TimeApiService : TimeService
    {
        public TimeApiService() : base("https://timeapi.io/api/time/current/zone?timeZone=Europe%2FMoscow")
        {
        }

        protected override DateTime ParseTimeFromJson(string json)
        {
            var timeData = JsonUtility.FromJson<DateTimeData>(json);
            var dateTime = DateTime.Parse(timeData.dateTime);
            return dateTime;
        }

        [Serializable]
        private class DateTimeData
        {
            public string dateTime;
        }
    }
}