using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.weather.models.json
{
    public class JsonCountryBaseModel
    {
        [JsonProperty("countries")]
        public JsonCountriesModel CountryBase { get; set; }
    }

    public class JsonCountriesModel
    {
        [JsonProperty("country")]
        public List<JsonCountryModel> JsonCountries { get; set; }
    }

    public class JsonCountryModel
    {
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
        [JsonProperty("countryName")]
        public string CountryName { get; set; }
        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }
        [JsonProperty("population")]
        public string Population { get; set; }
        [JsonProperty("capital")]
        public string CapitalCity { get; set; }
        [JsonProperty("continentName")]
        public string Continent { get; set; }
    }
}
