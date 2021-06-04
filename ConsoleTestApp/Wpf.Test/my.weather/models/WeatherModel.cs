using Newtonsoft.Json;
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
    /**
     * Ort         name
Latitude	coord.lat 
Longitude   coord.lon
Description weather.0.description 
CurrentTemperature		main.temp
FeelsLikeTemperature	main.feels_like
MinTemperature          main.temp_min
MaxTemperature          main.temp_max
AirPressure             main.pressure
Humidity                main.humidity
Visibility              visibility
Windspeed				wind.speed
UtcTimeZone             timezone
Sunrise                 sys.sunrise
Sunset                  sys.sunset 
CurrentTime
     * **/
    public class WeatherModel : INotifyPropertyChanged
    {
        private string _city;
        private double _latitude;
        private double _longitude;
        private string _description;
        private double _currenttemperature;
        private double _feelsliketemperature;
        private double _mintemperature;
        private double _maxtemperature;

        public string City { get { return _city; } set { _city = value; OnPropertyChanged(); } }
        public double Latitude { get { return _latitude; } set { _latitude = value; OnPropertyChanged(); } }
        public double Longitude { get { return _longitude; } set { _longitude = value; OnPropertyChanged(); } }
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged(); } }
        public double CurrentTemperature { get { return _currenttemperature; } set { _currenttemperature = value; OnPropertyChanged(); } }
        public double FeelsLikeTemperature { get { return _feelsliketemperature; } set { _feelsliketemperature = value; OnPropertyChanged(); } }
        public double MinTemperature { get { return _mintemperature; } set { _mintemperature = value; OnPropertyChanged(); } }
        public double MaxTemperature { get { return _maxtemperature; } set { _maxtemperature = value; OnPropertyChanged(); } }

        #region Constructors
        public WeatherModel() { }
        // Instance Constructor
        public WeatherModel(string city,double latitude,double longitude, string description, double currenttemperature,double feelsliketemperature,double mintemperature, double maxtemperature) 
        {
            this.City = city;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Description = description;
            this.CurrentTemperature = currenttemperature;
            this.FeelsLikeTemperature = feelsliketemperature;
            this.MinTemperature = mintemperature;
            this.MaxTemperature = maxtemperature;
        }
        
        // Copy constructor
        public WeatherModel(JsonWeather jsonweather)
                    :this(jsonweather.City,jsonweather.Latitude,jsonweather.Longitude,jsonweather.Description,jsonweather.CurrentTemperature,jsonweather.FeelsLikeTemperature,jsonweather.MinTemperature,jsonweather.MaxTemperature)
        {

        }
        #endregion

        public static implicit operator WeatherModel(JsonWeather jsonmodel)
        {
            WeatherModel w = new WeatherModel(jsonmodel);
            return w;
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
