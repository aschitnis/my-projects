using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Test.my.weather.converters;
using Wpf.Test.my.weather.models;
using Wpf.Test.my.weather.models.json;

namespace Wpf.Test.my.weather.classes.services
{

    /// <summary>
    /// read from or write to json file.
    /// </summary>
    public class JsonService
    {
        internal static T DeserializeObject<T>(string json) where T : class, new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, new JsonHelperConverter());
            }
            catch (JsonException e)
            {
                return null;
            }
        }
    }
}
