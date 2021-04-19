using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using my.country.sales.models.json;
using my.country.sales.models;

namespace my.country.sales.classes
{
    public class SingletonSalesModelsDataContainer
    {
        public enum DataFileContent { Countries, Currencies, Sales, Unknown}
        public DataFileContent enumFileContentType { get; set; } = DataFileContent.Unknown;

        internal PathManager FileManager { get; private set; }
        private string jsonCountriesData;
        private string jsonCurrenciesData;
        public string messageString;
        
        public List<RegionModel> RegionModels = new List<RegionModel>();
        public HashSet<JsonCountryModel> HashSetJsonCountryModels { get; set; } = new HashSet<JsonCountryModel>();
        public HashSet<JsonCurrencyModel> HashSetJsonDataCurrencies { get; set; } = new HashSet<JsonCurrencyModel>();
        private List<SalesModel> SalesModelsFromCsvFile { get; set; } = new List<SalesModel>();
        public List<string> Regions { get; set; } = new List<string>();

        private static SingletonSalesModelsDataContainer instance = new SingletonSalesModelsDataContainer();
        public static SingletonSalesModelsDataContainer Instance => instance;

        public event EventHandler<string> SingletonDataMessagingPublisherEvent;
        public event EventHandler<string> SingletonDataErrorMessagingPublisherEvent;
        
        private SingletonSalesModelsDataContainer()
        {

            //if (ValidateDataFiles())
            //{
            //    Task t = Task.Factory.StartNew(async () => await GetAsyncAllSalesData(), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            //    //Task.Factory.StartNew(async () => await GetAsyncAllCurrencies(), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

            //    t.ContinueWith(async (x) =>
            //    {
            //        await GetAsyncCountriesDataFromJsonFile();
            //    }, CancellationToken.None).
            //    ContinueWith(async (x) =>
            //    {
            //        await GetAsyncAllCurrenciesDataFromJsonFile();
            //    }, CancellationToken.None); 

            //}
            //else 
            //{ 
            //    this.HandleFileNotFoundError();
            //}
        }
        
        #region Events
        public void OnRaiseErrorMessageEvent()
        {
            if (SingletonDataErrorMessagingPublisherEvent != null)
            {
                SingletonDataErrorMessagingPublisherEvent.Invoke(this, messageString);
            }
        }

        public void OnRaiseMessageEvent()
        {
            if (SingletonDataMessagingPublisherEvent != null)
            {
                SingletonDataMessagingPublisherEvent.Invoke(this, messageString);
            }
        }
        #endregion

        /// <summary>
        /// Check if the csv & json data files exist.
        /// </summary>
        /// <returns></returns>
        public bool ValidateFilesPathSuccess()
        {
            FileManager = new PathManager();
            Exception ex = FileManager.CheckIfFilesExist();
            if (ex == null)
            {
                return true;
            }
            else
            {
                messageString = $"{ex.Message}";
                return false;
            }
        }

        public async Task<Exception> GetAsyncCountriesDataFromJsonFile()
        {
            Exception ex = null;
            try
            {
                enumFileContentType = DataFileContent.Countries;
                Task<HashSet<JsonCountryModel>> t = Task.Run<HashSet<JsonCountryModel>>(() =>
                {
                    return GetTaskCountriesDataFromJsonFile();
                });
                await t;

                HashSetJsonCountryModels = t.Result;
   
                if (!string.IsNullOrEmpty(messageString))
                    messageString = messageString + $" - Anzahl der Länderdaten: {HashSetJsonCountryModels.Count} - ";
                else messageString = $" - Anzahl der Länderdaten: {HashSetJsonCountryModels.Count} - ";

                return ex;
            }
            catch (TaskCanceledException e)
            {
                return e;
            }
            catch (Exception exception)
            {
                await HandleSingletonDataError(exception);
                return exception;
            }
        }
        private Task<HashSet<JsonCountryModel>> GetTaskCountriesDataFromJsonFile()
        {
            //Thread.Sleep(7000);
            HashSet<JsonCountryModel> hsCountries = new HashSet<JsonCountryModel>();
            Task<HashSet<JsonCountryModel>> t = Task.Run<HashSet<JsonCountryModel>>(() =>
                                               {
                                                   jsonCountriesData = File.ReadAllText(FileManager.JsonFilePathCountries);
                                                   jsonCountriesData = jsonCountriesData.Substring(jsonCountriesData.IndexOf("{", 0) + 1, jsonCountriesData.Count() - 2);
                                                   jsonCountriesData = jsonCountriesData.Substring(jsonCountriesData.IndexOf(":") + 1, jsonCountriesData.Count() - 20);

                                                   JsonCountryRootModel rootjsonObject = JsonConvert.DeserializeObject<JsonCountryRootModel>(jsonCountriesData);
                                                   foreach (JsonCountryModel countryobject in rootjsonObject.JsonCountriesModels)
                                                   {
                                                       hsCountries.Add(countryobject);
                                                       //Thread.Sleep(5);
                                                   }

                                                   return hsCountries;
                                               });
            return t;
        }
        public async Task GetAsyncAllCurrenciesDataFromJsonFile()
        {
            try
            {
                enumFileContentType = DataFileContent.Currencies;
                Task<HashSet<JsonCurrencyModel>> t = Task.Run<HashSet<JsonCurrencyModel>>(() =>
                {
                    return GetTaskAllCurrenciesDataFromJsonFile();
                });
                await t;
                HashSetJsonDataCurrencies = t.Result;

                if (!string.IsNullOrEmpty(messageString))
                    messageString = messageString + $"Anzahl der Währungen: {HashSetJsonDataCurrencies.Count}";
                else messageString = $"Anzahl der Währungen: {HashSetJsonDataCurrencies.Count}";

                OnRaiseMessageEvent();
            }
            catch(TaskCanceledException)
            {

            }
            catch(Exception ex)
            {
                await HandleSingletonDataError(ex);
            }
        }
        private Task<HashSet<JsonCurrencyModel>> GetTaskAllCurrenciesDataFromJsonFile()
        {
            HashSet<JsonCurrencyModel> hscurrencies = new HashSet<JsonCurrencyModel>();

            Task<HashSet<JsonCurrencyModel>> t = Task.Run(() =>
                                   {
                                       jsonCurrenciesData = File.ReadAllText(FileManager.JsonFilePathCurrencies);
                                       Newtonsoft.Json.Linq.JToken tokencontainer = Newtonsoft.Json.Linq.JObject.Parse(jsonCurrenciesData);

                                       foreach (Newtonsoft.Json.Linq.JToken token in tokencontainer.Children())
                                       {
                                           JsonCurrencyModel currencyModel = new JsonCurrencyModel();
                                           currencyModel.ShortName = token.First.Path;
                                           currencyModel.DisplayName = token.First.Path + "-" + (string)token.First.Parent.First.SelectToken("name");
                                           currencyModel.Name = (string)token.First.Parent.First.SelectToken("name"); // US Dollar
                                           currencyModel.NamePlural = (string)token.First.Parent.First.SelectToken("name_plural");
                                           hscurrencies.Add(currencyModel);
                                       }
                                       return hscurrencies;
                                   });
            return t;
        }

        /// <summary>
        /// 1) reads all the sales data from csv file
        /// 2) creates & populates a SalesModel object List<>
        /// </summary>
        /// <returns></returns>
        public Task<List<SalesModel>> GetTaskReadCsvFileAllSalesData()
        {
            List<SalesModel> salesmodels = new List<SalesModel>();
            Task<List<SalesModel>> t = Task.Run(() =>
            {
                int x = 0;
                //Thread.Sleep(7000);
                using (StreamReader reader = new StreamReader(File.OpenRead(FileManager.CsvFilePathSales)))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        if (!String.IsNullOrWhiteSpace(line) && (!line.StartsWith("Region")))
                        {
                            string[] values = line.Split(',');
                            SalesModel salesModel = new SalesModel();
                            salesModel.Region = values[0] ?? "unbekannt";   //Region
                            salesModel.Country = values[1] ?? "unbekannt";  //Country
                            salesModel.ItemType = values[2] ?? "unbekannt"; //Item Type
                            salesModel.SalesChannel = values[3] ?? "unbekannt"; // Sales Channel
                            salesModel.OrderId = values[6] == null ? 0 :  Convert.ToInt64(values[6]);
                            salesModel.OrderDate = new DateTime();
                            salesModel.OrderDate = values[5] == null ? default(DateTime) : DateTime.Parse(values[5], CultureInfo.GetCultureInfo("en"));    // values[5] Order Date
                            salesmodels.Add(salesModel);
                            // Thread.Sleep(3); 
                            x++;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(messageString))
                 messageString = messageString + $" , CSV-Datensätze: {x}";
                else messageString =  $" , CSV-Datensätze: {x}";
                OnRaiseMessageEvent();

                return salesmodels;
            });
            return t;
        }

        #region Error-Message methods
        public void HandleFileNotFoundError()
        {
            OnRaiseErrorMessageEvent();
        }
        private Task HandleSingletonDataError(Exception ex)
        {
            string sErrorInformation = null;
            switch(enumFileContentType)
            {
                case DataFileContent.Countries:
                    sErrorInformation = "-Fehler beim Lesen der Datei 'countries.json' -";
                    break;
                case DataFileContent.Currencies:
                    sErrorInformation = "-Fehler beim Lesen der Datei 'currencies.json' -";
                    break;
                case DataFileContent.Sales:
                    sErrorInformation = "-Fehler beim Lesen der Datei '500000_Sales_Records.csv' -";
                    break;
                case DataFileContent.Unknown:
                    break;
            }

            messageString = string.IsNullOrEmpty(messageString) == false ? messageString + $" {sErrorInformation} " : $" {sErrorInformation} ";
            
            //if (!string.IsNullOrEmpty(messageString))
            //    messageString = messageString + $" {sErrorInformation} ";
            //else messageString = $" {sErrorInformation} ";

            Task t = Task.Run(() =>
            {
                OnRaiseErrorMessageEvent();
            });
            return t;
        }
        #endregion



/***********************************************************************************************************
 ***********************************************************************************************************
 *****/

        #region  UNUSED FUNCTIONS
        /// <summary>
        /// 1) reads all the sales data from csv file
        /// 2) creates & populates a SalesModel object List<>
        /// </summary>
        /// <returns></returns>
        public async Task GetAsyncAllSalesData()
        {
            try
            {
                Task<List<SalesModel>> t = Task.Run(() =>
                {
                    return GetTaskReadCsvFileAllSalesData();
                });
                await t;
                SalesModelsFromCsvFile = t.Result;
                // Message = $"Regionen:  {internCsvDataSalesList.Count()}";
                RegionModels = SalesModelsFromCsvFile.GroupBy(sales => sales.Region)
                                       .Select(grouping => new RegionModel
                                       {
                                           Region = grouping.Key,
                                           RegionalSalesModels = grouping.ToList<SalesModel>()
                                       }).ToList<RegionModel>();

                // fill the Regions to be displayed as a List in the ListBox
                foreach (string name in RegionModels.Select(x => x.Region))
                    Regions.Add(name);
            }
            catch (TaskCanceledException)
            {

            }
            catch (Exception exception)
            {
                await HandleSingletonDataError(exception);
            }
        }
        #endregion
    }
}
