using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Test.my.weather.classes.converters;
using Wpf.Test.my.weather.classes.Constants;
using Wpf.Test.my.weather.models.json;

namespace Wpf.Test.my.weather.classes
{
    public class JsonService
    {
        public JsonSchedulerModel JsonScheduledTimeModel { get; set; }
        public JsonWeatherModel JsonWeatherModel { get; set; }
        public List<JsonCountryModel> JsonCountryModels { get; set; }
        
        public string JsonString {get;set;}

        #region serialization methods
        private string SerializeObject<T>(T jsonobject) where T : class, new()
        {
            string json = null;
            try
            {
                JsonHelperConverter jsonconverter = new JsonHelperConverter();
                JsonConvert.SerializeObject(jsonobject, jsonconverter);
                json = jsonconverter.GetJsonString();
            }
            catch(Exception e)
            {
                throw e;
            }
            return json;
        }

        public Exception SerializeToString(JsonConstants.JsonTypes jsontype, object json)
        {
            try
            {
                switch(jsontype)
                {
                    case JsonConstants.JsonTypes.ScheduleTaskConfiguration:
                        JsonSchedulerModel schedulermodel = json as JsonSchedulerModel;
                        JsonString = SerializeObject<JsonSchedulerModel>(schedulermodel);
                        break;
                    case JsonConstants.JsonTypes.CurrentWeather:
                        JsonWeatherModel weathermodel = json as JsonWeatherModel;
                        JsonString = SerializeObject<JsonWeatherModel>(weathermodel);
                        break;
                }
            }
            catch(Exception e)
            {
                return e;
            }
            return null;
        }
        #endregion
        
        #region deserialization methods
        public Exception DeserializeToObject(JsonConstants.JsonTypes jsontype, string jsonstring)
        {
            try
            {
                if(jsontype == JsonConstants.JsonTypes.ScheduleTaskConfiguration)
                {
                    JsonScheduledTimeModel = DeserializeObject<JsonSchedulerModel>(jsonstring);
                }
                else if (jsontype == JsonConstants.JsonTypes.CurrentWeather)
                {
                    JsonWeatherModel = DeserializeObject<JsonWeatherModel>(jsonstring);
                }
                else if (jsontype == JsonConstants.JsonTypes.Countries)
                {
                    JsonCountryBaseModel JsonRootModel = DeserializeObjectWithoutHelperConverter<JsonCountryBaseModel>(jsonstring);
                    JsonCountryModels = JsonRootModel.CountryBase.JsonCountries;
                }
            }
            catch (Exception e)
            {
                return e;
            }
            return null;
        }

        private T DeserializeObject<T>(string json) where T : class, new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, new JsonHelperConverter());
            }
            catch (JsonException e)
            {
                throw e;
            }
        }

        private T DeserializeObjectWithoutHelperConverter<T>(string json) where T : class, new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonException e)
            {
                throw e;
            }
        }
        #endregion
    }
}
