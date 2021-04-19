using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Test.my.weather.models;
using Wpf.Test.my.weather.models.json;

namespace Wpf.Test.my.weather.converters
{
    public class JsonHelperConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(JsonWeather))
                return (objectType == typeof(JsonWeather));
            else return (objectType == typeof(JsonScheduler));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(JsonScheduler))
            {
                JObject jo = JObject.Load(reader);
                JsonScheduler taskSchedulerconfig = jo.ToObject<JsonScheduler>();
                taskSchedulerconfig.StartTime = jo.SelectToken("schedule.start").ToString();
                taskSchedulerconfig.EndTime = jo.SelectToken("schedule.end").ToString();
                taskSchedulerconfig.Interval_Seconds = Convert.ToInt32(jo.SelectToken("schedule.interval_seconds"));
                taskSchedulerconfig.Interval_Minutes = Convert.ToInt32(jo.SelectToken("schedule.interval_minutes"));
                return taskSchedulerconfig;
            }
            else
            {
                JObject jo = JObject.Load(reader);
                JsonWeather weather = jo.ToObject<JsonWeather>();
                weather.Latitude = (double)jo.SelectToken("coord.lat");
                weather.Longitude = (double)jo.SelectToken("coord.lon");
                weather.Description = jo.SelectToken("weather.[0].description")?.ToString();
                weather.CurrentTemperature = (double)jo.SelectToken("main.temp");
                weather.FeelsLikeTemperature = (double)jo.SelectToken("main.feels_like");
                return weather;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
