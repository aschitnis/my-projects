using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Newtonsoft.Json.Linq;
using schnittstelle.http.service.currency.viewmodel;

namespace schnittstelle.http.service.currency
{
    public class CurrencyConversionVM : ViewModelBase
    {
        #region constructors
        public CurrencyConversionVM()
        {
           WebRequestEventSubscriberVM = new WebRequestEventsSubscriberVM();
           RestApi = new CurrencyMarketRateJsonWebServiceClient();
           RestApi.WebRequestEvent.webserviceError += WebRequestEventSubscriberVM.HandleWebserviceErrorEvent;
           RestApi.WebRequestEvent.webserviceMessage += WebRequestEventSubscriberVM.HandleWebserviceMessageEvent;
            CurrentDatetime = DateTime.Now.ToString(new CultureInfo("de-DE"));
           FillCurrencyLists(); 
            GetExchangeRateCommand = new CurrencyConversionCommand(CalculateExchangeRate, OnCalculateExchangeRateCanExecute);
        }
        #endregion

        #region commands
        public ICommand GetExchangeRateCommand { get; private set; }
        #endregion

        #region properties
        private string currencyprefixtitlemessage;
        private string currencypostfixtitlemessage;
        private decimal targetcurrencycalculatedvalue;
        private decimal exchangeratevalue;
        private string currentdatetime;

        public CurrencyMarketRateJsonWebServiceClient RestApi { get; set; }

        public WebRequestEventsSubscriberVM WebRequestEventSubscriberVM { get; set; }

        public string CurrencyPrefixTitleMessage
        {
            get { return currencyprefixtitlemessage; }
            set { currencyprefixtitlemessage = value;OnChanged(); }
        }

        public string CurrencyPostfixTitleMessage
        {
            get { return currencypostfixtitlemessage; }
            set
            {
                currencypostfixtitlemessage = value;
                OnChanged();
            }
        }

        public string CurrentDatetime
        {
            get { return currentdatetime; }
            set { currentdatetime = value;OnChanged(); }
        }

        public string SelectedTargetCurrShortName { get; set; }

        public string SelectedSourceCurrShortName { get; set; }

        public CCurrencyModel SelectedTargetCurrency { get; set; }

        public CCurrencyModel SelectedSourceCurrency { get; set; }

        public decimal TargetCurrencyValue { get; set; }

        public decimal TargetCurrencyExchangeResultValue { get; set; }

        public decimal ExchangeRateValue
        {
            get { return exchangeratevalue; }
            set { exchangeratevalue = value;OnChanged(); }
        }

        public decimal SourceCurrencyValue { get; set; }

        public decimal TargetCurrencyCalculatedValue
        {
            get { return targetcurrencycalculatedvalue; }
            set { targetcurrencycalculatedvalue = value;OnChanged(); }
        }

        public List<CCurrencyModel> SourceCurrenciesList { get; set; }

        public List<CCurrencyModel> TargetCurrenciesList
        {
            get;set;
        }
        #endregion

        private async Task<List<CCurrencyModel>> ReadAsyncJsonCurrenciesFile()
        {
            List<CCurrencyModel> currencyNamesList = new List<CCurrencyModel>();
            Task t = Task.Run(() =>
            {
                // ThinkPad: C:\Users\Mustermann\source\repos\ConsoleTestApp\schnittstelle.http.service.currency
                // DGH: C:\Projekte\ConsoleTestApp\schnittstelle.http.service.currency
                string jsonstring = File.ReadAllText(@"C:\Users\Mustermann\source\repos\ConsoleTestApp\schnittstelle.http.service.currency\Common-Currency.json");
                Newtonsoft.Json.Linq.JToken tokencontainer = Newtonsoft.Json.Linq.JObject.Parse(jsonstring);
                int index = 0;
                foreach (Newtonsoft.Json.Linq.JToken token in tokencontainer.Children())
                {
                    // currencyNamesList.Add( token.First.Path+"-"+ (string)token.First.Parent.First.SelectToken("name") );
                    CCurrencyModel currencyModel = new CCurrencyModel();
                    currencyModel.ShortName = token.First.Path;
                    currencyModel.DisplayName = token.First.Path + "-" + (string)token.First.Parent.First.SelectToken("name");
                    currencyModel.Name = (string)token.First.Parent.First.SelectToken("name"); // US Dollar
                    currencyModel.NamePlural = (string)token.First.Parent.First.SelectToken("name_plural");
                    currencyModel.Symbol = (string)token.First.Parent.First.SelectToken("symbol");
                    currencyNamesList.Add(currencyModel);
                    
                    /*
                     * Invoke vs BeginInvoke
                       Use Invoke if you want the current thread to wait until the UI thread has processed 
                       the dispatch code or BeginInvoke if you want current thread to continue without waiting for operation 
                       to complete on UI thread.
                     */
                    //Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal,
                    //                                     new Action(() =>
                    //                                        {
                    //                                            CountryDetails = currencyNamesList[index];
                    //                                        }));
                    // CountryDetails = currencyNamesList[index];
                    index++;
                }
            });
            
            await t;
            return currencyNamesList;
        }

        private void FillCurrencyLists()
        {
            Task<List<CCurrencyModel>> t = Task.Run<List<CCurrencyModel>>( () =>
                                                {
                                                    return ReadAsyncJsonCurrenciesFile();
                                                }  );
            t.Wait();
           // TaskAwaiter<List<string>> taskawaiter = t.GetAwaiter();
            SourceCurrenciesList = t.Result; 
            TargetCurrenciesList = t.Result; 
        }

        private bool OnCalculateExchangeRateCanExecute(object parameter)
        {
            int comboboxIndex = (int)parameter;
            if (this.RestApi.HasHttpException && this.RestApi.HasJsonErrorCodeInResponse)
                return false;
            if (SourceCurrencyValue == 0)
                return false;
            if (comboboxIndex < 0)
                return false;
            else return true;
        }

        public void CalculateExchangeRate(object parameter)
        {
            RestApi.HttpGetExchangeRateAsJsonString(this.SelectedSourceCurrShortName,
                                                    this.SelectedTargetCurrShortName);

 
            if (!this.RestApi.HasHttpException && !this.RestApi.HasJsonErrorCodeInResponse)
            {
                this.ExchangeRateValue = RestApi.ExchangeRate;
                this.TargetCurrencyCalculatedValue = this.SourceCurrencyValue * this.ExchangeRateValue;

                this.CurrencyPrefixTitleMessage = "1 " + SelectedSourceCurrency.Name + " entspricht ";
                
                this.CurrencyPostfixTitleMessage = ExchangeRateValue.ToString("G") + " " + SelectedTargetCurrency.Name;

                this.CurrentDatetime = DateTime.Now.ToString(new CultureInfo("de-DE"));
            }
        }
    }
}
