using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wpf.Test.my.weather.models.json;
using Wpf.Test.my.weather.classes.constants;

namespace Wpf.Test.my.weather.classes
{
    internal sealed class WeatherApiAccess
    {
        public string JsonString { get; set; }
        public WeatherApiAccess()
        {
            //WeatherDataWebClient = new HttpClient();
            //WeatherDataWebClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        /// <summary>
        /// call the REST webservice to get the current weather condition. 
        /// </summary>
        /// <param name="city">current weather for the City</param>
        /// <returns>a Exception derived from the Exception class. 
        /// If there are no errors the exception object is NULL</returns>
        internal Task<WebApiException> GetAsyncCurrentWeather(string city)
        {
            WebApiException exc = null;
            HttpResponseMessage response = null;

            Task<WebApiException> t = Task.Run(async () =>
                                {
                                    try
                                    {
                                        string endpointUrl = GlobalPathManager.GetWebServiceUrl(city);
                                        using(HttpClient weatherApi = new HttpClient())
                                        {
                                            weatherApi.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                            response = await weatherApi.GetAsync(endpointUrl);
                                            
                                            if (response.IsSuccessStatusCode == true)
                                            {
                                                // Handle Success
                                                if (response.Content is object)
                                                {
                                                    JsonString = response.Content.ReadAsStringAsync().Result;
                                                }
                                                else
                                                {
                                                    JsonString = null;
                                                }
                                            }
                                            else
                                            {
                                                // Handle failure
                                                string errorMsg = response.Content.ReadAsStringAsync().Result;
                                                exc = new WebApiException(errorMsg, response.StatusCode.ToString());
                                                //RaiseCurrentWeatherWebServiceEvent(errorMessage);
                                            }

                                            if (response != null)
                                                response.Dispose();
                                        }
                                    }
                                    catch(HttpRequestException ex)
                                    {
                                        exc = new WebApiException("HttpRequest Error - " + ex.Message);
                                    }
                                    catch (Exception ex)
                                    {
                                        exc = new WebApiException(ex.Message);
                                       // RaiseCurrentWeatherWebServiceEvent(ex.Message);
                                    }
                                    return exc;
                                });
            return t;
        }
    }

    public class WebApiException : Exception
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public WebApiException() {  }
        public WebApiException(string message, string statuscode = null)
        {
            StatusCode = statuscode;
            StatusMessage = message;
        }
    }
}
