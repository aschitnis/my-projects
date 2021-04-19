using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.webservice
{
    public class CHttpTest : IDisposable, INotifyPropertyChanged
    {
        #region Properties
        public HttpClient RestClient { get; set; }
        public bool HasHttpError { get; set; }
        private string _httperror;

        public string HttpErrorMessage
        {
            get { return _httperror; }
            set { _httperror = value; RaisePropertyChanged(); }
        }

        // public HttpResponseMessage RestResponseMessageClient { get; set; }
        public string ResponseJsonString { get; set; }
        private bool isDisposed = false;
        #endregion

        public CHttpTest()
        {
            /* Optional kann ein 
             * HttpMessageHandler als Parameter übergeben werden.
             */
            RestClient = new HttpClient();
            RestClient.BaseAddress = new Uri(@"http://rate-exchange-1.appspot.com/currency");
            RestClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        ~CHttpTest()
        {
            Dispose(false);
        }


        public async Task<string> DownloadJsonStringForCurrencyConversion(string fromCurrency, string toCurrency)
        {
            string result = null;
            HttpResponseMessage response = null;
             
            response = await RestClient.GetAsync("?from=" + fromCurrency + "&to=" + toCurrency);
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
                HasHttpError = true;
                // Handle failure
                HttpErrorMessage = await response.Content.ReadAsStringAsync();
            }

            if (response != null)
                response.Dispose();

            return result ?? string.Empty;

            //string result = null;
            //HttpResponseMessage response = null;
            //try
            //{
            //    response = await RestClient.GetAsync("?from="+fromCurrency+"&to="+toCurrency);

            //    response.EnsureSuccessStatusCode();
            //    if (response.IsSuccessStatusCode == false)
            //    {
            //         Console.WriteLine("XXXXXXXXXXXXXXXXX");
            //    }
            //    if (response.Content is object)
            //    {
            //        result = await response.Content.ReadAsStringAsync();
            //    }
            //}
            //catch(HttpRequestException e)
            //{
            //    result = e.Message;
            //}
            //catch(Exception ex)
            //{
            //    result = ex.Message;
            //}
            //finally
            //{
            //    if (response != null)
            //        response.Dispose();
            //}
            //return result ?? string.Empty;
        }

        public void GetCurrencyRateFromWebservice(string fromCurr, string toCurr)
        {
           Task<string> t = Task.Run<string>(() =>
            {
               return DownloadJsonStringForCurrencyConversion(fromCurr, toCurr);
            });
            TaskAwaiter<string> taskAwaiter = t.GetAwaiter();
            if (HasHttpError)
            {
                Console.WriteLine(HttpErrorMessage);
            }
            else this.ResponseJsonString = taskAwaiter.GetResult();
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

        #region RaiseProperty event
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
        #endregion
    }
}
