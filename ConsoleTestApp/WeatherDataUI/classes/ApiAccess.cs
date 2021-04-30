using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyProjects.WeatherData.Wpf.WeatherDataUI.classes
{
    internal sealed class ApiAccess
    {
        string city = null;

        public ApiAccess(string _city)
        {
            city = _city;
        }
        private string GetEndpoint()
        {
            return PathManager.WEATHER_HTTP_PATH + city + PathManager.WEATHER_WEBSERVICE_LICENSEKEY;
        }

        private HttpWebRequest GetHttpWebRequest()
        {
            //HttpResponseMessage response = null;
            //HttpClient httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string endPointUrl = GetEndpoint();
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(endPointUrl);
            webRequest.Method = "POST";
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip; //this requests dynamic content compression from the server (cuts down the response size by about 70%)
            webRequest.ContentType = "application/json; charset=utf-8";
            return webRequest;
        }

        public Exception GetWeather(out string json)
        {
            HttpWebResponse webResponse = null;
            json = null;

            try
            {
                HttpWebRequest webRequest = GetHttpWebRequest();

                //using (var writer = webRequest.GetRequestStream())
                //{
                //    string jsonRequest = JsonConvert.SerializeObject(new { request = request });
                //    byte[] requestData = Encoding.UTF8.GetBytes(jsonRequest);
                //    writer.Write(requestData, 0, requestData.Length);
                //}

                webResponse = (HttpWebResponse)webRequest.GetResponse();
                HttpStatusCode statusCode = webResponse.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    var responseStream = new StreamReader(webResponse.GetResponseStream());
                    json = responseStream.ReadToEnd();
                    return null;
                }
                else
                {
                    webResponse.Dispose();
                    return new Exception($"GetWeather failed: {statusCode.ToString()}");
                }


                //JsonSerializerSettings serializerSettings = new JsonSerializerSettings() { Converters = new List<JsonConverter> { new JsonApiUtils.JsonTypeMapper<JsonResult, JsonCustomerDataResponse>() } }; //custom serializer settings which makes it possible to deserialize JSON objects into their corresponding child object
                //JsonCustomerDataResponse customerResponse = JsonConvert.DeserializeObject<JsonCustomerDataResponse>(responseString, serializerSettings);

                //if (customerResponse.IsSuccessful)
                //{
                //    response = customerResponse;
                //    return null;
                //}
                //else
                //{
                //    response = null;
                //    return new Exception("GetAllCustomers() failed: ", customerResponse?.Exception.ToException());
                //}
            }
            catch (Exception ex)
            {
                webResponse.Dispose();
                return new Exception("GetWeather() threw an exception: ", ex);
            }
        }
    }
}
