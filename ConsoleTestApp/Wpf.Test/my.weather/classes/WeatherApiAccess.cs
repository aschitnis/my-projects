using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wpf.Test.my.weather.models.json;

namespace Wpf.Test.my.weather.classes
{
    internal sealed class WeatherApiAccess
    {
        private HttpClient WeatherDataWebClient;

        public string JsonString { get; set; }
        public WeatherApiAccess()
        {
            WeatherDataWebClient = new HttpClient();
            WeatherDataWebClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Exception GetCurrentWeather(string city)
        {
            Exception exc = null;
            HttpResponseMessage response = null;

            Task.Run(() =>
                       {
                           try
                           {
                               string endpointUrl = PathManager.GetWebServiceUrl(city);
                               response = WeatherDataWebClient.GetAsync(endpointUrl).Result;
                               if (response.IsSuccessStatusCode == true)
                               {
                                   // Handle Success
                                   if (response.Content is object)
                                   {
                                       JsonString = response.Content.ReadAsStringAsync().Result;
                                   }
                               }
                               else
                               {
                                   // Handle failure
                                   string errorMsg = response.Content.ReadAsStringAsync().Result;
                                   exc = new Exception(errorMsg);
                                   //RaiseCurrentWeatherWebServiceEvent(errorMessage);
                               }

                               if (response != null)
                                   response.Dispose();
                           }
                           catch (Exception ex)
                           {
                               exc = new Exception(ex.Message);
                               // RaiseCurrentWeatherWebServiceEvent(ex.Message);
                           }
                       });
            return exc;
        }

        public async Task<ApiResponseException> GetAsyncCurrentWeather(string city)
        {
            string endpointUrl = PathManager.GetWebServiceUrl(city);

            // return response as soon as the headers have been read. Do not wait for the Content to be read completely in the stream.
            HttpResponseMessage response = await WeatherDataWebClient.GetAsync(endpointUrl, HttpCompletionOption.ResponseHeadersRead);

            // response.EnsureSuccessStatusCode(); // ensure that the request succeeded and has a 2xx status code
            ApiResponseException exc = null;
            
            try
            {
                if (response.IsSuccessStatusCode == true)
                {
                    // Handle Success
                    if (response.Content is object)
                    {
                        JsonString = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        exc = new ApiResponseException($"Invalid response: {response.StatusCode}");
                    }
                }
                else
                {
                    // Handle failure
                    string jsonresult = await response.Content.ReadAsStringAsync();
                    JsonWeatherApiError errordata = JsonConvert.DeserializeObject<JsonWeatherApiError>(jsonresult);
                    
                    string errorMsg = $"Statuscode: {errordata.StatusCode} - {errordata.ErrorMessage} ";
                    exc = new ApiResponseException(errordata.ErrorMessage, errordata.StatusCode);

                    //RaiseCurrentWeatherWebServiceEvent(errorMessage);
                }
            }
            catch (Exception ex)
            {
                exc = new ApiResponseException(ex.Message);
                // RaiseCurrentWeatherWebServiceEvent(ex.Message);
            }
            finally
            {
                response.Dispose();
            }
            return exc;
        }
    }

    public class ApiResponseException : Exception
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public ApiResponseException() {  }
        public ApiResponseException(string message, string statuscode = null)
        {
            StatusCode = statuscode;
            StatusMessage = message;
        }
    }
}
