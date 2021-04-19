using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace my.country.sales.classes
{
    public class CurrentWeatherWebClient : IDisposable
    {
        #region Properties
        private HttpClient WeatherDataWebClient { get; set; }
        private bool isDisposed = false;
        public bool HasHttpException { get; set; }
        // public bool HasJsonErrorCodeInResponse { get; set; }
        public string JsonStringCurrentWeatherData { get; set; }
        #endregion

        public event EventHandler<WebServiceEventArgs> CurrentWeatherWebClientEvent;

        public CurrentWeatherWebClient() 
        { 
            InitializeWebClient();
        }

        #region methods
        private void InitializeWebClient()
        {
            HasHttpException = false;
            /* Optional kann ein 
             * HttpMessageHandler als Parameter übergeben werden.
             */
            WeatherDataWebClient = new HttpClient();
            WeatherDataWebClient.BaseAddress = new Uri(@"http://rate-exchange-1.appspot.com/currency");
            WeatherDataWebClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task GetAsyncHttpRequestForCurrentWeather(string city)
        {
            JsonStringCurrentWeatherData =  await GetTaskHttpRequestForCurrentWeather(city);
        }

        private async Task<string> GetTaskHttpRequestForCurrentWeather(string city)
        {
            string result = null;
            HttpResponseMessage response = null;
            HasHttpException = false;
            try
            {
                response = await WeatherDataWebClient.GetAsync("http://api.openweathermap.org/data/2.5/weather?q=" + city + "&lang=de&appid=98ec98ab7c0f616ddae7a6c4be445e58");
                if (response.IsSuccessStatusCode == true)
                {
                    // Handle Success
                    if (response.Content is object)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    HasHttpException = true;
                    // Handle failure
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    RaiseCurrentWeatherWebServiceEvent(errorMessage);
                }

                if (response != null)
                    response.Dispose();
            }
            catch (Exception ex)
            {
                HasHttpException = true;
                RaiseCurrentWeatherWebServiceEvent(ex.Message);
            }

            return result;
        }
        #endregion

        #region Event-handling methods
        public void RaiseCurrentWeatherWebServiceEvent(string message)
        {
            if (CurrentWeatherWebClientEvent != null)
                CurrentWeatherWebClientEvent.Invoke(this, new WebServiceEventArgs(message));
        }
        #endregion

        #region Dispose Methods
        public void Dispose()
        {
            Dispose(true);

            /* GC.SuppressFinalize(this) :
             * Objects that implement the IDisposable interface can call this method 
             * from the IDisposable.Dispose method to prevent the garbage collector 
             * from calling Object.Finalize on an object that does not require it.
             */
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // clean up managed objects by calling 
                    // their Dispose() method.
                    WeatherDataWebClient.Dispose();
                }
                // clean up unmanaged objects here
            }
            isDisposed = true;
        }
        #endregion
    }

    public class WebServiceEventArgs
    {
        public string Message { get; set; }
        public WebServiceEventArgs(string msg)
        {
            this.Message = msg;
        }
    }
}
