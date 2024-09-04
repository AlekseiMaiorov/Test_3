using System;
using UnityEngine;

namespace Services.Time
{
    public class ApiNinjasService : TimeService
    {
        public ApiNinjasService() : base("https://api.api-ninjas.com/v1/worldtime?timezone=Europe/Moscow",
                                         "6zqKFJXVj3IUm4+/12+taw==IHFqIvYS1y6BqERx")
        {
        }

        protected override DateTime ParseTimeFromJson(string json)
        {
            var timeData = JsonUtility.FromJson<DateTimeData>(json);
            var dateTime = DateTime.Parse(timeData.datetime);
            return dateTime;
        }

        [Serializable]
        private class DateTimeData
        {
            public string datetime;
        }
    }
}