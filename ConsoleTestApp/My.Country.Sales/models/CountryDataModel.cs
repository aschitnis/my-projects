using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace my.country.sales.models
{
    public class CountryDataModel : INotifyPropertyChanged
    {
        private string countrycode;
        private string countryname;
        private string currencycode;
        private string population;
        private string capital;
        private string continentname;

        public string CountryCode
        {
            get { return countrycode; }
            set { countrycode = value; OnPropertyChanged(); }
        }
        public string CountryName
        {
            get { return countryname; }
            set { countryname = value; OnPropertyChanged(); }
        }
        public string CurrencyCode
        {
            get { return currencycode; }
            set { currencycode = value; OnPropertyChanged(); }
        }
        public string Population
        {
            get { return population; }
            set { population = value; OnPropertyChanged(); }
        }
        public string Capital
        {
            get { return capital; }
            set { capital = value; OnPropertyChanged(); }
        }
        public string Continent
        {
            get { return continentname; }
            set { continentname = value; OnPropertyChanged(); }
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
