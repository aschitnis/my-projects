using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Wpf.Test.my.books.management.Classes;
using Wpf.Test.my.weather.classes;
using Wpf.Test.my.weather.classes.constants;
using Wpf.Test.my.weather.classes.Constants;
using Wpf.Test.my.weather.classes.services;
using Wpf.Test.my.weather.models;
using Wpf.Test.my.weather.models.json;

namespace Wpf.Test.my.weather.viewmodels
{
    public class WeatherViewModel : ViewModelBase //INotifyPropertyChanged
    {
        public event EventHandler<WeatherModel> weatherserviceexecutedEvent;

        private static string JsonDataString = null;
        private SchedulerModel _scheduledtimemodel;

        private int _weatherservicecount;
        private string _programmessage;
        private bool _iserror;
        private bool _isschedulerstartbuttonenabled;
        private List<CountryModel> _countrymodelslist;

        private AsyncObservableCollection<WeatherModel> _weatherdatacontainer;

        #region Properties
        public int WeatherServiceCount { get => _weatherservicecount; set { _weatherservicecount = value;OnChanged(); } }
        public SchedulerModel ScheduledTimeModel { get => _scheduledtimemodel ?? new SchedulerModel(); set { _scheduledtimemodel = value; OnChanged(); } }
        public List<CountryModel> CountryModelsList { get => _countrymodelslist; set { _countrymodelslist = value;OnChanged(); } }
        public bool IsSchedulerStartButtonEnabled { get => _isschedulerstartbuttonenabled; set { _isschedulerstartbuttonenabled = value; OnChanged(); } }
        public string ProgramMessage { get { return _programmessage; } set { _programmessage = value; OnChanged(); } }
        public bool IsError { get { return _iserror; } set { _iserror = value; OnChanged(); } }
        public JsonService JsonManager { get; set; }

        public AsyncObservableCollection<WeatherModel> WeatherDataContainer
        {
            get 
            {
                return _weatherdatacontainer;
            }
            set { _weatherdatacontainer = value; OnChanged("WeatherDataContainer"); }
        }
        #endregion

        #region constructors
        public WeatherViewModel()
        {
            JsonManager = new JsonService();

            weatherserviceexecutedEvent += HandleWeatherServiceExecutedEvent;
            WeatherDataContainer = new AsyncObservableCollection<WeatherModel>();

            Init();
            InitializeCountriesData();
        }
        #endregion

        private void InitializeCountriesData()
        {
            Exception ex = GlobalPathManager.ReadFile(JsonConstants.JsonTypes.Countries, out JsonDataString);

            if (ex == null)
            {
               Exception exc = InitializeCountriesDataFromJsonFile();
            }
            else
            {
                IsSchedulerStartButtonEnabled = false;
                ProgramMessage = "Fehler beim Lesen der Länderdaten aus Jsondatei.";
            }
        }
        /// <summary>
        /// a) read the json file containing the scheduler timings for start etc.
        /// b) deserialize the json string & display the times in the View/Window.
        /// </summary>
        private void Init()
        {
            WeatherServiceCount = 0;
            Exception ex = GlobalPathManager.ReadFile(JsonConstants.JsonTypes.ScheduleTaskConfiguration ,out JsonDataString);

            if (ex == null)
            {
                ex = SetSchedulerTimingsFromJson();
                IsSchedulerStartButtonEnabled = ex == null ? true : false;

                if (IsSchedulerStartButtonEnabled == false)
                    ProgramMessage = "Fehler beim Deserialization der Jsondatei.";
                else
                    ProgramMessage = "Laufzeiteinstellungen aus Jsondatei erfolgreich gelesen.";
            }
            else
            {
                IsSchedulerStartButtonEnabled = false;
                ProgramMessage = "Fehler beim Lesen der Laufzeiteinstellungen aus Jsondatei.";
            }
        }

        private Exception InitializeCountriesDataFromJsonFile()
        {
            Exception ex = null;
            try
            {
                ex = JsonManager.DeserializeToObject(JsonConstants.JsonTypes.Countries, JsonDataString);
                if (ex == null)
                {
                    CountryModelsList = JsonManager.ToCountryModelsList(JsonManager.JsonCountryModels);
                }
            }
            catch (Exception e)
            {
                return e;
            }
            return ex;
        }

        public void HandleWeatherServiceExecutedEvent(object sender, WeatherModel args)
        {
            WeatherDataContainer.Add(args);
        }
        public void OnWeatherServiceExecuted(WeatherModel weathermodel)
        {
            if (weatherserviceexecutedEvent != null)
                weatherserviceexecutedEvent(this, weathermodel);
        }

        /// <summary>
        /// a) Deserialize the json string to a object of class JsonScheduler.
        /// b) Set the start, the end and the interval-time properties for executing the task.
        /// c) These properties display the timings on the View & could be changed by the User.
        /// </summary>
        private Exception SetSchedulerTimingsFromJson()
        {
            Exception ex = null;
             try
            {
                ex = JsonManager.DeserializeToObject(JsonConstants.JsonTypes.ScheduleTaskConfiguration, JsonDataString);
                if (ex == null)
                {
                    ScheduledTimeModel = JsonManager.JsonScheduledTimeModel;
                }
            }
            catch(Exception e)
            {
                return e;
            }
            return ex; 

            //double tmpSecondsInDecimal = Convert.ToDouble(configuration.Interval_Seconds);
            //double intervalInHours = configuration.Interval_Minutes > 0 ? System.Math.Round(Convert.ToDouble((configuration.Interval_Minutes * 60)) / 3600, 4) : System.Math.Round(Convert.ToDouble(tmpSecondsInDecimal / 3600), 4);
        }

        private Exception SaveSchedulerTimingsToJsonFile()
        {
            Exception ex = null;

            // save data from ScheduledTimeModel into JsonScheduledTimeModel
            JsonManager.JsonScheduledTimeModel.StartTime = ScheduledTimeModel.StartTime;
            JsonManager.JsonScheduledTimeModel.EndTime = ScheduledTimeModel.EndTime;
            JsonManager.JsonScheduledTimeModel.Interval_Minutes = ScheduledTimeModel.IntervalMinutes;
            JsonManager.JsonScheduledTimeModel.Interval_Seconds = ScheduledTimeModel.IntervalSeconds;

            ex = JsonManager.SerializeToString(JsonConstants.JsonTypes.ScheduleTaskConfiguration, JsonManager.JsonScheduledTimeModel);
            if (ex == null)
            {
               Exception exe = GlobalPathManager.WriteFile(JsonManager.JsonString);
               if (exe != null)
               {
                    // Error message and treatment..
                    return exe;
               }
            }
            else
            {
                // Error message and treatment...
                return ex;
            }
            return null;
        }

        private bool Validation()
        {
            return true;
        }
        public void StartScheduler()
        {
            // save the data to the json file and read it once more
            Exception ex = SaveSchedulerTimingsToJsonFile();
            if (ex == null)
            {
                int startHours = Convert.ToInt32(ScheduledTimeModel.StartTime.Split(':').First());
                int startMinutes = Convert.ToInt32(ScheduledTimeModel.StartTime.Split(':').Last());
                int endHours = Convert.ToInt32(ScheduledTimeModel.EndTime.Split(':').First());
                int endMinutes = Convert.ToInt32(ScheduledTimeModel.EndTime.Split(':').Last());

                Action<string> ActionCurrentWeather = new Action<string>(GetWeatherDataFromWebService);
                SchedulerService.Instance.ScheduleTaskWithInterval(startHours, startMinutes, ScheduledTimeModel.IntervalMinutes, ScheduledTimeModel.IntervalSeconds, ActionCurrentWeather, "Siliguri");
            }
        }
        private async void GetWeatherDataFromWebService(string city)
        {
            WeatherApiAccess weatherApi = new WeatherApiAccess();
            WebApiException ex = await weatherApi.GetAsyncCurrentWeather(city);
            if (ex == null)
            {
                IsError = false;
                Exception exe = JsonManager.DeserializeToObject(JsonConstants.JsonTypes.CurrentWeather, weatherApi.JsonString);
                if (exe == null)
                {
                    ProgramMessage = $"Die Wetterdatenabfrage für {city} ist fehlerfrei abgeschlossen worden.";
                    WeatherModel weatherModel = JsonManager.JsonWeatherModel;
                    WeatherServiceCount++;

                    OnWeatherServiceExecuted(weatherModel);
                }
                else
                {
                    IsError = true;
                    ProgramMessage = $"Die Wetterdatenabfrage für {city} ist Fehlerhaft. Method:DeserializeFromJsonString()  ";
                }
            }
            else
            {
                IsError = true;
                ProgramMessage = $"{ex.StatusCode} - {ex.StatusMessage} - {city}";
            }
        }

        #region INotifyPropertyChanged implementation
        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        #endregion
    }
}
