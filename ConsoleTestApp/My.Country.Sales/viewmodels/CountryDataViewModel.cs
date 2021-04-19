using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using my.country.sales.models.json;
using my.country.sales.models;

namespace my.country.sales.viewmodels
{
   public class CountryDataViewModel : INotifyPropertyChanged
    {
        private CountryDataModel currentcountrydatamodel;

        #region props
        private HashSet<CountryDataModel> HashSetCountryDataModels { get; set; }
        public CountryDataModel CurrentCountryDataModel
        {
            get { return currentcountrydatamodel; }
            set { currentcountrydatamodel = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructor
        public CountryDataViewModel()
        {
            HashSetCountryDataModels = HashSetCountryDataModels ?? new HashSet<CountryDataModel>();
        }
        public CountryDataViewModel(HashSet<JsonCountryModel> jsoncountrymodels)
        {
            HashSetCountryDataModels = new HashSet<CountryDataModel>();
            foreach (JsonCountryModel c in jsoncountrymodels)
                HashSetCountryDataModels.Add(new CountryDataModel() { Capital = c.capital, Continent = c.continentName, CountryCode = c.countryCode, CountryName = c.countryName, CurrencyCode = c.currencyCode, Population = c.population });
        }
        #endregion

        #region Methods
        public void FindCountry(string countryName)
        {
            CurrentCountryDataModel = HashSetCountryDataModels.Where(delegate (CountryDataModel c) { return c.CountryName == countryName; }).FirstOrDefault<CountryDataModel>() ?? new CountryDataModel();
           // countrydataModel = HashSetCountryDataModels.Where(new Func<CountryDataModel, bool>(x => x.CountryName == countryName)).FirstOrDefault<CountryDataModel>();
        }
        #endregion

        public static implicit operator CountryDataViewModel(HashSet<JsonCountryModel> hsCountrymodels)
        {
            CountryDataViewModel countryVM = new CountryDataViewModel(hsCountrymodels);
            return countryVM;
        }

        #region NotifyChanged Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
