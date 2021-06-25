using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.weather.models.json
{
    public class JsonGeocodingBaseModel
    {
        [JsonProperty("results")]
        public List<JsonGeocodingResult> Results { get; set; }
        [JsonProperty("status")]
        public string JsonStatus { get; set; }
    }

    public class JsonGeocodingResult
    {
        [JsonProperty("geometry")]
        public JsonGeometryModel GeoGeometry { get; set; }
    }
     public class JsonGeometryModel
     {
        [JsonProperty("location")]
        public JsonLocationModel Location { get; set; }
    }
     public class JsonLocationModel
     {
        [JsonProperty("lat")]
        public float Latitude { get; set; }
        [JsonProperty("lng")]
        public float Longitude { get; set; }
    }
}
