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
        private string _currenttimeatcity;
        private string _currenttime;
        private double _timezonefordestinationcityinseconds;

        public string City { get { return _city; } set { _city = value; OnPropertyChanged(); } }
        public double Latitude { get { return _latitude; } set { _latitude = value; OnPropertyChanged(); } }
        public double Longitude { get { return _longitude; } set { _longitude = value; OnPropertyChanged(); } }
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged(); } }
        public double CurrentTemperature { get { return _currenttemperature; } set { _currenttemperature = value; OnPropertyChanged(); } }
        public double FeelsLikeTemperature { get { return _feelsliketemperature; } set { _feelsliketemperature = value; OnPropertyChanged(); } }
        public double MinTemperature { get { return _mintemperature; } set { _mintemperature = value; OnPropertyChanged(); } }
        public double MaxTemperature { get { return _maxtemperature; } set { _maxtemperature = value; OnPropertyChanged(); } }
        public string CurrentTimeAtCity { get => _currenttimeatcity ?? "00:00"; set { _currenttimeatcity = value;OnPropertyChanged(); } }
        public string CurrentTime { get => _currenttime ?? "00:00"; set { _currenttime = value; OnPropertyChanged(); } }
        public double TimezoneForDestinationCityInSeconds { get { return _timezonefordestinationcityinseconds; } set { _timezonefordestinationcityinseconds = value; OnPropertyChanged(); } }

        #region Constructors
        public WeatherModel() { }
        // Instance Constructor
        public WeatherModel(string city,double latitude,double longitude, string description, double currenttemperature,double feelsliketemperature,double mintemperature, double maxtemperature, double timezonefordestinationcityinseconds) 
        {
            this.City = city;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Description = description;
            this.CurrentTemperature = currenttemperature;
            this.FeelsLikeTemperature = feelsliketemperature;
            this.MinTemperature = mintemperature;
            this.MaxTemperature = maxtemperature;
            this.TimezoneForDestinationCityInSeconds = timezonefordestinationcityinseconds;
            // this.CurrentTime = "00:00";
        }
        
        // Copy constructor
        public WeatherModel(JsonWeatherModel jsonweather)
                    :this(jsonweather.City,jsonweather.Latitude,jsonweather.Longitude,jsonweather.Description,jsonweather.CurrentTemperature,jsonweather.FeelsLikeTemperature,jsonweather.MinTemperature,jsonweather.MaxTemperature,jsonweather.TimeZoneInSeconds)
        {

        }
        #endregion

        public static implicit operator WeatherModel(JsonWeatherModel jsonmodel)
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
