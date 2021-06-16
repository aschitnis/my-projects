using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.weather.models.json
{
    public class JsonSchedulerModel
    {
        [JsonProperty("interval_seconds")]
        public int Interval_Seconds { get; set; }
        [JsonProperty("interval_minutes")]
        public int Interval_Minutes { get; set; }
        [JsonProperty("start")]
        public string StartTime { get; set; }

        [JsonProperty("end")]
        public string EndTime { get; set; }
    }
}
