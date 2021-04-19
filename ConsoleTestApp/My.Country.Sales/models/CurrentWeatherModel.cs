using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace my.country.sales.models
{
    /**
    	"main":{"temp":277.01,
			"feels_like":274.43,
			"temp_min":276.15,
			"temp_max":278.15,"pressure":1023,"humidity":93},
	"visibility":10000,
    **/
    public class CurrentWeatherModel : INotifyPropertyChanged
    {
        private string description;
        public int visibility;

        public string Description 
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }
        public int Visibility
        {
            get { return visibility; }
            set { visibility = value; OnPropertyChanged(); }
        }
        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
