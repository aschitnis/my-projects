using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using my.country.sales.models.json;
using my.country.sales.classes;
using my.country.sales.models;

namespace my.country.sales.viewmodels
{
    public class CurrentWeatherViewModel : INotifyPropertyChanged
    {
        public CurrentWeatherWebClient WeatherWebServiceClient { get; set; }
        //public JsonCurrentWeatherModel JsonWeatherData { get; set; }
        public CurrentWeatherModel CurrentWeatherData { get; set; }
        public CurrentWeatherViewModel() 
        {
            WeatherWebServiceClient = new CurrentWeatherWebClient();
            CurrentWeatherData = new CurrentWeatherModel();
        }

        #region async methods
        public async Task GetAsyncCurrentWeatherDataFromWebService(string city)
        {
            await WeatherWebServiceClient.GetAsyncHttpRequestForCurrentWeather(city);
            if (!WeatherWebServiceClient.HasHttpException)
            {
                JsonCurrentWeatherModel jsonWeatherModel = JsonConvert.DeserializeObject<JsonCurrentWeatherModel>(WeatherWebServiceClient.JsonStringCurrentWeatherData);
                CurrentWeatherData.Description = jsonWeatherModel.weatherList[0].description;
            }
        }
        #endregion

        #region NotifyChanged Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
