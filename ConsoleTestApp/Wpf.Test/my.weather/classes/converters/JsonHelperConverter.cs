using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Test.my.weather.models;
using Wpf.Test.my.weather.models.json;

namespace Wpf.Test.my.weather.classes.converters
{
    public class JsonHelperConverter : JsonConverter
    {
        public string json { get; set; }
        public string GetJsonString() { return json; }
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(JsonWeatherModel))
                return (objectType == typeof(JsonWeatherModel));
            else return (objectType == typeof(JsonSchedulerModel));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo;
            try
            {
                jo = JObject.Load(reader);
            }
            catch(JsonReaderException)
            {
                return null;
            }

            if (objectType == typeof(JsonSchedulerModel))
            {
                JsonSchedulerModel taskSchedulerconfig = jo.ToObject<JsonSchedulerModel>();
                taskSchedulerconfig.StartTime = jo.SelectToken("schedule.start").ToString();
                taskSchedulerconfig.EndTime = jo.SelectToken("schedule.end").ToString();
                taskSchedulerconfig.Interval_Seconds = Convert.ToInt32(jo.SelectToken("schedule.interval_seconds"));
                taskSchedulerconfig.Interval_Minutes = Convert.ToInt32(jo.SelectToken("schedule.interval_minutes"));
                return taskSchedulerconfig;
            }
            else // if (objectType == typeof(JsonWeatherModel))
            {
                JsonWeatherModel weather = jo.ToObject<JsonWeatherModel>();
                weather.Latitude = (double)jo.SelectToken("coord.lat");
                weather.Longitude = (double)jo.SelectToken("coord.lon");
                weather.Description = jo.SelectToken("weather.[0].description")?.ToString();
                weather.CurrentTemperature = (double)jo.SelectToken("main.temp");
                weather.FeelsLikeTemperature = (double)jo.SelectToken("main.feels_like");
                weather.TimeZoneInSeconds = (double)jo.SelectToken("timezone");
                return weather;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is JsonSchedulerModel)
            {
                JsonSchedulerModel jsonschedulemodel = value as JsonSchedulerModel;
                StringBuilder sb = new StringBuilder();
                using(StringWriter sw = new StringWriter(sb))
                {
                    JsonWriter jw = new JsonTextWriter(sw);
                    jw.Formatting = Formatting.None;

                    jw.WriteStartObject();
                    jw.WritePropertyName("schedule");
                        jw.WriteStartObject();
                            jw.WritePropertyName("start");
                            jw.WriteValue(jsonschedulemodel.StartTime);
                            jw.WritePropertyName("end");
                            jw.WriteValue(jsonschedulemodel.EndTime);
                            jw.WritePropertyName("interval_seconds");
                            jw.WriteValue(jsonschedulemodel.Interval_Seconds);
                            jw.WritePropertyName("interval_minutes");
                            jw.WriteValue(jsonschedulemodel.Interval_Minutes);
                       jw.WriteEndObject();
                    jw.WriteEndObject();

                    json = sb.ToString();
                }
            }
            else if (value is JsonWeatherModel)
            {
                JsonWeatherModel jsonmodel = value as JsonWeatherModel;
                StringBuilder sb = new StringBuilder();
            }
        }
    }
}
