using Newtonsoft.Json;
using schnittstelle.http.service.currency.custom.errorevent;
using schnittstelle.http.service.currency.customeventargs;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace schnittstelle.http.service.currency
{
    public class CurrencyMarketRateJsonWebServiceClient : IDisposable
    {
        #region Properties
        public CWebRequestEventHandler WebRequestEvent { get; set; } 
        public HttpClient RestClient { get; set; }
        private int WEBREQUESTCOUNT = 1;
        private System.Timers.Timer webRequestTimer = new System.Timers.Timer();
        public decimal ExchangeRate { get; set; }
        public string JsonErrorMessage { get; set; }
        public string HttpErrorMessage { get; set; }
        private string ResponseJsonString { get; set; }
        private string FromCurrency { get; set; } = "EUR";
        private string ToCurrency { get; set; } = "INR";

        public bool HasHttpException { get; set; }
        public bool HasJsonErrorCodeInResponse { get; set; }
        private bool isDisposed = false;
        #endregion

        public CurrencyMarketRateJsonWebServiceClient()
        {
            HasJsonErrorCodeInResponse = false;
            HasHttpException = false;
            /* Optional kann ein 
             * HttpMessageHandler als Parameter übergeben werden.
             */
            RestClient = new HttpClient();
            RestClient.BaseAddress = new Uri(@"http://rate-exchange-1.appspot.com/currency");
            RestClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            WebRequestEvent = new CWebRequestEventHandler();

            webRequestTimer.Interval = 8000;
            webRequestTimer.AutoReset = true;
            webRequestTimer.Elapsed += OnMakeWebServiceRequestTimedEvent;
        }

        ~CurrencyMarketRateJsonWebServiceClient()
        {
            Dispose(false);
        }

        private void OnMakeWebServiceRequestTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            // WebRequestEvent.OnWebserviceMessage($"Timer started: {this.WEBREQUESTCOUNT}");
            HttpGetExchangeRateAsJsonString(FromCurrency, ToCurrency);
        }

        private async Task<string> DownloadAsyncHttpGetCurrencyRateJson(string fromCurrency, string toCurrency)
        {
            string result                = null;
            HttpResponseMessage response = null;
            HasHttpException             = false;
            HasJsonErrorCodeInResponse   = false;
            try
            {
                response = await RestClient.GetAsync("?from=" + FromCurrency + "&to=" + ToCurrency);
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
                    HttpErrorMessage = await response.Content.ReadAsStringAsync();
                    WebRequestEvent.OnWebserviceError(HttpErrorMessage);
                }

                if (response != null)
                    response.Dispose();
            }
            catch (Exception ex)
            {
                HasHttpException = true;
                WebRequestEvent.OnWebserviceError(ex.Message);
            }

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromCurr"></param>
        /// <param name="targetCurrency">the exchangerate will be shown in this currency</param>
        public void HttpGetExchangeRateAsJsonString(string fromCurr, string targetCurrency)
        {
            FromCurrency = fromCurr;
            ToCurrency = targetCurrency;

            Task<string> t1 = Task.Run<string>(() =>
            {
                return DownloadAsyncHttpGetCurrencyRateJson(fromCurr, targetCurrency);
            });
            Task.WaitAll(t1);

            //TaskAwaiter<string> taskAwaiter = task.GetAwaiter();
            //this.ResponseJsonString = taskAwaiter.GetResult();

            if (HasHttpException == false)
            {
                if (webRequestTimer.Enabled)
                    webRequestTimer.Enabled = false;

                this.ResponseJsonString = t1.Result;
                ReadExchangeRateFromJsonString();
            }
            else
            {
                if (WEBREQUESTCOUNT == 1)
                    webRequestTimer.Enabled = true;
                WebRequestEvent.OnWebserviceMessage($"Timer started: {this.WEBREQUESTCOUNT}");
                WEBREQUESTCOUNT++;
            }
        }

        private void ReadExchangeRateFromJsonString()
        {
            Newtonsoft.Json.Linq.JToken token = Newtonsoft.Json.Linq.JObject.Parse(this.ResponseJsonString);
            string errdata = (string)token.SelectToken("err");
            if (errdata != null)
            {
                this.HasJsonErrorCodeInResponse = true;
                this.JsonErrorMessage = errdata;
                WebRequestEvent.OnWebserviceError(this.JsonErrorMessage);
            }
            else this.ExchangeRate = (decimal)token.SelectToken("rate");
        }
        
 
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
                    RestClient.Dispose();
                }
                // clean up unmanaged objects here
            }
            isDisposed = true;
        }
        #endregion
    }
}
