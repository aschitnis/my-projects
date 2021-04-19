using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my.country.sales.models.json
{
    public class JsonCountryModel 
    {
        private string _countryCode;
        private string _countryName;
        private string _currencyCode;
        private string _population;
        private string _capital;
        private string _continentName;

        public string countryCode { get { return _countryCode; } set { _countryCode = value;  } }
        public string countryName { get { return _countryName; } set { _countryName = value;  } }
        public string currencyCode { get { return _currencyCode; } set { _currencyCode = value;  } }
        public string population { get { return _population; } set { _population = value;  } }
        public string capital { get { return _capital; } set { _capital = value;  } }
        public string continentName { get { return _continentName; } set { _continentName = value;  } }

        #region Event
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        #endregion
    }
}
