using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.weather.models.json
{
    public class JsonWeatherApiError
    {
        [JsonProperty("cod")]
        public string StatusCode { get; set; }
        [JsonProperty("message")]
        public string ErrorMessage { get; set; }

        public JsonWeatherApiError() {   }
    }
}
