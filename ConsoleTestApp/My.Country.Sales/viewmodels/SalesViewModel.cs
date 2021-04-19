using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using my.country.sales.classes;
using my.country.sales.viewmodels;
using my.country.sales.models.json;
using my.country.sales.models;
using System.Windows.Threading;

namespace my.country.sales.viewmodels
{
    public class SalesViewModel : INotifyPropertyChanged
    {
        public ICommand DisplayCountriesForSelectedRegionCommand { get; set; }

        private SingletonSalesModelsDataContainer instance;
        public MessagingSubscriber applicationMessageSubscriber;
        
        private CountryDataViewModel countrydatavm;
        private List<SalesModel> internCsvDataSalesList = new List<SalesModel>();
        private RegionModel internalCurrentRegionModel; // selected RegionModel Object
        private List<RegionModel> internalRegionsList;
        //private HashSet<JsonCountryModel> internalCountriesHashSet = new HashSet<JsonCountryModel>();

        private bool disableregionslist;
        private string message;
        private string currentcapitalcity;
        private bool isprogramerror;
        private List<IGrouping<string, SalesModel>> internalCountriesAndSalesGroupList;
        private bool hasfileerror;
        private ObservableCollection<string> regionslist;
        private string chosenregion;
        //private List<RegionDisplayModel> regionsdisplaylist; // not used

        private ObservableCollection<string> countries;
        private string currentregion;
        private string currentcountry;
        private bool? hascountries;
        private ObservableCollection<SalesModel> saleslistforcurrentcountry;
        private CurrentWeatherViewModel currentweathervm;

        #region Events
        public event EventHandler<string> CountrySelectedEvent;
        #endregion

        private async void ReadAllDataAsync()
        {
           Exception ex = await GetSalesDataAsync();
           if (ex == null)
            {
                // use of implicit operator defined in CountrydataViewModel class.
                // typeof(CountryDataViewModel) is allocated with a '=' data from HashSet of type(JsonCountryModel)
               Exception exc = await instance.GetAsyncCountriesDataFromJsonFile();
               if (exc == null)
                {
                    instance.OnRaiseMessageEvent();
                    EnableRegionsList = true;
                    CountryDataVM = instance.HashSetJsonCountryModels.Count > 1 ? instance.HashSetJsonCountryModels : new HashSet<JsonCountryModel>();
                    await instance.GetAsyncAllCurrenciesDataFromJsonFile();
                }
            }
        }
        public SalesViewModel()
        {
            this.Init();

            // RegionsList = new ObservableCollection<string>(instance.Regions);
            // string weatherJsonFileData = File.ReadAllText(@"C:\Projekte\ConsoleTestApp\Wpf.Test\bin\Debug\data\weather.json");
            // JsonTemperatureModel otemperatureModel = JsonConvert.DeserializeObject<JsonTemperatureModel>(weatherJsonFileData);

            if (instance.ValidateFilesPathSuccess())
            {
                ReadAllDataAsync();
                /***
                Task.Run(async () =>
                {
                    await GetSalesDataAsync();

                    // use of implicit operator defined in CountrydataViewModel class.
                    // typeof(CountryDataViewModel) is allocated with a '=' data from HashSet of type(JsonCountryModel)
                    //await instance.GetAsyncCountriesDataFromJsonFile();

                    //CountryDataVM = instance.HashSetJsonCountryModels.Count > 1 ? instance.HashSetJsonCountryModels : new HashSet<JsonCountryModel>();
                    //await instance.GetAsyncAllCurrenciesDataFromJsonFile();
                })
                .ContinueWith(async (c) =>
                                {
                                    // use of implicit operator defined in CountrydataViewModel class.
                                    // typeof(CountryDataViewModel) is allocated with a '=' data from HashSet of type(JsonCountryModel)
                                    await instance.GetAsyncCountriesDataFromJsonFile();
                                })
                .ContinueWith(async (c) =>
                {
                    CountryDataVM = instance.HashSetJsonCountryModels.Count > 1 ? instance.HashSetJsonCountryModels : new HashSet<JsonCountryModel>();
                    await instance.GetAsyncAllCurrenciesDataFromJsonFile();
                });
                ***/
                /******
                ContinueWith(async (x) =>
                {
                    TemperatureHttpClient = new CurrentWeatherWebClient();
                    await TemperatureHttpClient.GetAsyncHttpRequestForCurrentWeather("Salzburg");
                }, CancellationToken.None);
                ******/
            }
            else
            {
                HasFileError = true;
                instance.HandleFileNotFoundError();
            }
        }

        private void Init()
        {
            EnableRegionsList = false;
            HasCountries = false;
           // RegionsDisplaylist = new List<RegionDisplayModel>(); // not used
            instance = SingletonSalesModelsDataContainer.Instance;
            CurrentWeatherVM = new CurrentWeatherViewModel();
            CountryDataVM = new CountryDataViewModel();

            CountrySelectedEvent += SearchAllSalesForCurrentCountry_CountrySelectedEvent;
            DisplayCountriesForSelectedRegionCommand = new SalesModelCommand(DisplayCountriesForSelectedRegion, IsQueryCountriesForSelectedRegionValid);

            instance.SingletonDataErrorMessagingPublisherEvent -= ApplicationMessageSubscriber.RaiseSingletonSalesDataErrorEvent;
            instance.SingletonDataErrorMessagingPublisherEvent += ApplicationMessageSubscriber.RaiseSingletonSalesDataErrorEvent;

            instance.SingletonDataMessagingPublisherEvent -= ApplicationMessageSubscriber.RaiseSingletonSalesDataEvent;
            instance.SingletonDataMessagingPublisherEvent += ApplicationMessageSubscriber.RaiseSingletonSalesDataEvent;
        }

        #region NotifyChanged Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properties
        public bool EnableRegionsList 
        {
            get { return disableregionslist; }
            set { disableregionslist = value; OnPropertyChanged(); }
        }
        public CountryDataViewModel CountryDataVM
        {
            get { return countrydatavm; }
            set { countrydatavm = value; OnPropertyChanged(); }
        }
        public string CurrentCapitalCity 
        {
            get { return currentcapitalcity; }
            set { currentcapitalcity = value; OnPropertyChanged(); }
        }
        public CurrentWeatherViewModel CurrentWeatherVM
        {
            get { return currentweathervm; }
            private set 
            {
                if (currentweathervm == null)
                {
                    currentweathervm = new CurrentWeatherViewModel();
                }
                currentweathervm = value;
                OnPropertyChanged();
            }
        }
        public bool HasFileError
        {
            get { return hasfileerror; }
            set { hasfileerror = value; OnPropertyChanged(); }
        }
        public MessagingSubscriber ApplicationMessageSubscriber 
        {
            get 
            {
                if (applicationMessageSubscriber == null)
                    applicationMessageSubscriber = new MessagingSubscriber();
                return applicationMessageSubscriber;
            }
            set { applicationMessageSubscriber = value; OnPropertyChanged(); }
        }
        public bool IsProgramError
        {
            get { return isprogramerror; }
            set { isprogramerror = value; OnPropertyChanged(); }
        }
        public bool? HasCountries 
        {
            get { return hascountries; }
            set {
                    hascountries = value;
                    OnPropertyChanged();
                }
        }
        public string ChosenRegion 
        {
            get { return chosenregion; }
            set { chosenregion = value; OnPropertyChanged(); }
        }
        //public List<RegionDisplayModel> RegionsDisplaylist
        //{
        //    get 
        //    {
        //        if (regionsdisplaylist == null)
        //            regionsdisplaylist = new List<RegionDisplayModel>();
        //        return regionsdisplaylist;
        //    }
        //    set 
        //    {
        //        regionsdisplaylist = value;
        //        OnPropertyChanged();
        //    }
        //}
        public string Message 
        {
            get { return message; }
            set { message = value;OnPropertyChanged(); }
        }
        public ObservableCollection<string> RegionsList 
        { get 
            {
                if (regionslist == null)
                    regionslist = new ObservableCollection<string>();
                return regionslist;
            } 
          set { regionslist = value; OnPropertyChanged(); }
        }
        public string CurrentRegion 
        {
            get { return currentregion; }
            set { 
                    currentregion = value;
                if (!string.IsNullOrEmpty(value))
                    // GetCountriesbelongingToCurrentRegion(value);
                    OnPropertyChanged();
                }
        }
        public string CurrentCountry 
        {
            get { return currentcountry; }
            set 
            { 
                currentcountry = value;

                CountrySelectedEvent?.Invoke(this, value);
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> Countries
        {
            get 
            {
                if (countries == null)
                    countries = new ObservableCollection<string>();
                return countries;
            }
            set
            {
                countries = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<SalesModel> SalesListForCurrentCountry 
        {
            get 
            {
                if (saleslistforcurrentcountry == null)
                    saleslistforcurrentcountry = new ObservableCollection<SalesModel>();
                return saleslistforcurrentcountry; 
            }
            set { saleslistforcurrentcountry = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods

        private void SearchAllSalesForCurrentCountry_CountrySelectedEvent(object sender, string country)
        {
            GetSalesModelsBelongingToCurrentCountry(country);

            // CurrentWeatherData async () => await GetSalesDataAsync(), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext()

            CountryDataVM.FindCountry(country);

            if (!string.IsNullOrEmpty(CountryDataVM.CurrentCountryDataModel.CountryName))
            {
                Task.Run(async () =>
                {
                    await CurrentWeatherVM.GetAsyncCurrentWeatherDataFromWebService(country);
                });
            }
            //CurrentCapitalCity = instance.HashSetJsonCountryModels.Where((c) => c.countryName == country).Select(s => s.capital).FirstOrDefault();
        }

        private void GetSalesModelsBelongingToCurrentCountry(string selectedcountry)
        {
            SalesListForCurrentCountry.Clear();
            if (selectedcountry == null)
            {
                SalesListForCurrentCountry.Clear();
                return;
            }
            IGrouping<string,SalesModel> selectedCountrySalesModelGroup = internalCountriesAndSalesGroupList.Where(x => x.Key.StartsWith(selectedcountry)).FirstOrDefault();
            if (selectedCountrySalesModelGroup != null)
            {
                SalesListForCurrentCountry = new ObservableCollection<SalesModel>();

                foreach (SalesModel sm in selectedCountrySalesModelGroup.Select(x => x))
                {
                    SalesListForCurrentCountry.Add(sm);
                }

                Message = $"Stand der Verkäufe(Anzahl): {SalesListForCurrentCountry.Count()}";
            }
        }

        // CurrentRegion is the Selected region (string).
        private void GetCountriesBelongingToCurrentRegion(string selectedregionname)
        {
            if (selectedregionname == null)
            {
                Countries.Clear();
                return;
            }

            internalCurrentRegionModel = internalRegionsList.Where(i => i.Region
                                                            .StartsWith(selectedregionname))
                                                            .FirstOrDefault();

            // List<key: country name, value: List<SalesModel>> 
            internalCountriesAndSalesGroupList = internalCurrentRegionModel
                                               .RegionalSalesModels
                                               .GroupBy(g => g.Country)
                                               .ToList<IGrouping<string, SalesModel>>();
            Countries = new ObservableCollection<string>();
            foreach (string key in internalCountriesAndSalesGroupList.Select(x => x.Key).OrderBy(a => a))
                Countries.Add(key);

            Message = $"Anzahl der Ländern:{Countries.Count()}";
        }

        private async Task<Exception> GetSalesDataAsync()
        {
            Exception ex = null;
            try
            {
                Task<List<SalesModel>> t = Task.Run(() => { return instance.GetTaskReadCsvFileAllSalesData(); });
                await t;
                
                internCsvDataSalesList = t.Result;
 
                 Message =  $"Regionen:  {internCsvDataSalesList.Count()}";

                // Each of the Regions along with their corresponding Sales objects(List<SalesModel>)
                internalRegionsList = internCsvDataSalesList.GroupBy(sales => sales.Region)
                                                       .Select(grouping => new RegionModel
                                                       {
                                                           Region = grouping.Key,
                                                           RegionalSalesModels = grouping.ToList<SalesModel>()
                                                       }).ToList<RegionModel>();


                // fill the Regions to be displayed as a List in the ListBox
                foreach (string name in internalRegionsList.Select(x => x.Region))
                    RegionsList.Add(name);
                return ex;
            }
            catch(Exception exc)
            {
                await HandleError(exc);
                return exc;
            }
        }

        private Task HandleError(Exception exception)
        {
            Task t = Task.Run(() => 
            { 
                IsProgramError = true; 
                Message = exception.Message;
            });
            return t;
        }
#endregion

#region Command Methods
        private void DisplayCountriesForSelectedRegion(object parameter)
        {
            string regionName = parameter == null ? null : (parameter as string).StartsWith("Regio") ? null : parameter as string;
            ChosenRegion = regionName == null ? "N.V." : regionName;
            HasCountries = regionName == null ? false : true;
            GetCountriesBelongingToCurrentRegion(regionName);
        }

        private bool IsQueryCountriesForSelectedRegionValid(object parameter)
        {
            string regionName = parameter == null ? null : (parameter as string).StartsWith("Regio") ? null : parameter as string;
 
            if (string.IsNullOrEmpty(regionName))
                return false;
            else
            {
                return true;
            }
        }
#endregion
    }
}


/**************** NOT USED  *********************************
var query = internSalesList.GroupBy(sales => sales.Region)
                           .Select(grouping => new RegionModel
                           {
                               Region = grouping.Key,
                               RegionalSalesModels = grouping.ToList<SalesModel>()
                           });
***********************************************/

//RegionModel region = internalRegionsList.Where(i => i.Region
//                                        .StartsWith("Sub-Saharan"))
//                                        .FirstOrDefault();

//List<IGrouping<string, SalesModel>> internalSalesPerCountryGroupList = region.RegionalSalesModels
//                                                                             .GroupBy(g => g.Country)
//                                                                             .ToList<IGrouping<string, SalesModel>>();

//IEnumerable<SalesModel> tmpSalesModelsOfCountry = internalSalesPerCountryGroupList.Where(x => x.Key == "Namibia")
//                                                                         .SelectMany(g => g);
//List<SalesModel> SalesModelsOfCountry = tmpSalesModelsOfCountry.ToList();

//List<SalesModel> SalesModelsOfCountry =
//                                        region.RegionalSalesModels.GroupBy(g => g.Country)
//                                                                  .ToList<IGrouping<string, SalesModel>>()
//                                                                  .Where(x => x.Key == "Namibia")
//                                                                  .SelectMany(g => g).ToList();