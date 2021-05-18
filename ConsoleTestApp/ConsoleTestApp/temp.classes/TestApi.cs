using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.temp.classes
{
    public abstract class BaseApiAccess
    {
        public enum Endpoint { LatestAllCurrenciesRates }
        protected string CurrencyBaseUrl { get => "https://api.currencyscoop.com/v1"; }
        protected string CURRENCY_API_KEY { get => "e765634f4c80f76ea733e1e5da897a39"; }
    }

    // api-key:  e765634f4c80f76ea733e1e5da897a39
    // user : abhijit , aschitnis@hotmail.com
    // password : elfriede51
    // link : https://currencyscoop.com/login
    // z.b. https://api.currencyscoop.com/v1/latest?base=INR&api_key=e765634f4c80f76ea733e1e5da897a39
    public class ApiAccess : BaseApiAccess
    {
        public Exception GetAllCurrencyExchangeRatesAsJsonString(string basecurrency)
        {
            try
            {
                HttpWebRequest webRequest = GetWebRequestForLatestAllCurrenciesRates(basecurrency);
                //using (Stream writer = webRequest.GetRequestStream())
                //{
                //    string payload = "?base=" + basecurrency + "&api_key=" + base.CURRENCY_API_KEY;
                //    byte[] requestData = Encoding.UTF8.GetBytes(payload);
                //    writer.Write(requestData, 0, requestData.Length);
                //}
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                var responseStream = new StreamReader(webResponse.GetResponseStream());
                string responseString = responseStream.ReadToEnd();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private HttpWebRequest GetWebRequestForLatestAllCurrenciesRates(string basecurrency)
        {
            string endPoint = @"https://api.currencyscoop.com/v1/latest?base=EUR&api_key=e765634f4c80f76ea733e1e5da897a38"; // 9
                // GetEndpoint(Endpoint.LatestAllCurrenciesRates);
            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(endPoint);
            webRequest.Method = "POST";
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip; //this requests dynamic content compression from the server (cuts down the response size by about 70%)
            webRequest.ContentType = "application/json; charset=utf-8";

            return webRequest;
        }

        private string GetEndpoint(Endpoint endpoint)
        {
            if (endpoint == Endpoint.LatestAllCurrenciesRates)
            {
                return CurrencyBaseUrl + "/latest";
            }
            return null;
        }
    }
}
