using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Test.my.weather.models;
using Wpf.Test.my.weather.models.json;

namespace Wpf.Test.my.weather.classes
{
    public static class JsonServiceExtensions
    {
        public static List<CountryModel> ToCountryModelsList(this JsonService service, List<JsonCountryModel> jsoncountrymodels)
        {
            List<CountryModel> listOfCountryModels = new List<CountryModel>();
            jsoncountrymodels.ForEach((m) =>
            {
               CountryModel model = m;
                listOfCountryModels.Add(model);
            });
            return listOfCountryModels;
        }
    }
}
