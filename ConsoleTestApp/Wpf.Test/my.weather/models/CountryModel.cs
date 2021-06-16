using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wpf.Test.my.weather.models.json;

namespace Wpf.Test.my.weather.models
{
    public class CountryModel : INotifyPropertyChanged
    {
        private string _countrycode;
        private string _countryname;
        private string _currencycode;
        private string _population;
        private string _capitalcity;
        private string _continent;
        public string CountryCode { get => _countrycode; set { _countrycode = value; } }
        public string CountryName { get => _countryname; set { _countryname = value; } }
        public string CurrencyCode { get => _currencycode; set { _currencycode = value; } }
        public string Population { get => _population; set { _population = value; } }
        public string CapitalCity { get => _capitalcity; set { _capitalcity = value; } }
        public string Continent { get => _continent; set { _continent = value; } }

        #region constructors
        public CountryModel()  { }
        public CountryModel(JsonCountryModel jsoncountrymodel)
        {
            CountryCode = jsoncountrymodel.CountryCode;
            CountryName = jsoncountrymodel.CountryName;
            CurrencyCode = jsoncountrymodel.CurrencyCode;
            Population = jsoncountrymodel.Population;
            CapitalCity = jsoncountrymodel.CapitalCity;
            Continent = jsoncountrymodel.Continent;
        }
        #endregion

        public static implicit operator CountryModel(JsonCountryModel jsoncountrymodel)
        {
            CountryModel countrymodel = new CountryModel(jsoncountrymodel);
            return countrymodel;
        }

        #region NotifyChanged Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
