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
using Wpf.Test.my.weather.classes;
using Wpf.Test.my.weather.classes.services;
using Wpf.Test.my.weather.models;
using Wpf.Test.my.weather.models.json;

namespace Wpf.Test.my.weather.viewmodels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        public static event EventHandler ErrorEvent;

        private static string JsonSchedulerTimingsString = null;
        private static bool IsJsonFileReadSuccess = false;
        private static Exception ErrorException = null;

        private int startHours;
        private int startMinutes;

        private string _starttime;
        private string _endtime;
        private int _intervalseconds;
        private int _intervalminutes;
        private string _programmessage;
        private bool _iserror;
        private List<WeatherModel> _weatherdatacontainer; 

        private DateTime dtEndTime;
        private DateTime dtStartTime;

        #region Properties
        public int IntervalSeconds { get { return _intervalseconds; } set { _intervalseconds = value; OnChanged(); } }
        public int IntervalMinutes { get { return _intervalminutes; } set { _intervalminutes = value; OnChanged(); } }
        public string StartTime { get => _starttime ?? "00:00"; set { _starttime = value; OnChanged(); } }
        public string EndTime { get => _endtime ?? "00:00"; set { _endtime = value; OnChanged(); } }
        public string ProgramMessage { get { return _programmessage; } set { _programmessage = value; OnChanged(); } }
        public bool IsError { get { return _iserror; } set { _iserror = value; OnChanged(); } }

        public List<WeatherModel> WeatherDataContainer
        {
            get 
            {
                if (_weatherdatacontainer == null)
                    _weatherdatacontainer = new List<WeatherModel>();
                return _weatherdatacontainer;
            }
            set { _weatherdatacontainer = value; OnChanged(); }
        }
        #endregion

        #region constructors
        public WeatherViewModel()
        {
            if (IsJsonFileReadSuccess)
            {
                ProgramMessage = "Die Datei mit der geplanten Ausführung der Wetterdaten erfolgreich gelesen.";
            }
            else
            {
                if (ErrorException is JsonReaderException)
                    ProgramMessage = ErrorException.Message;
                else ProgramMessage = "";
            }
                

            // ReadSchedulerConfiguration();
            // GetCurrentWeatherForCity("Rishikes");
        }

        static WeatherViewModel()
        {
            string json = null;
            ErrorException = ReadDataFromJsonSchedulerTimingsFile(out json);
            JsonSchedulerTimingsString = json;

            if (ErrorException == null)
                IsJsonFileReadSuccess = true;
            else
                IsJsonFileReadSuccess = false;
        }

        /// <summary>
        /// read the json file containing the start, the end & the interval timings
        /// </summary>
        /// <param name="json">gets the json data in StringFormat</param>
        /// <returns></returns>
        #endregion
        private static Exception ReadDataFromJsonSchedulerTimingsFile(out string json)
        {
            return PathManager.ReadFile(out json);
        }

        /// <summary>
        /// deserialize the json string to a object of class JsonScheduler.
        /// set the start, the end and the interval time for executing the task. 
        /// </summary>
        private bool GetSchedulerTimingsData()
        {
            if (IsJsonFileReadSuccess == false)
                return false;

            JsonSchedulerModel scheduleTimingsModel = JsonService.DeserializeObject<JsonSchedulerModel>(JsonSchedulerTimingsString);  // JsonManager.DeserializeToObject(typeof(JsonWeatherConfiguration));
            if (scheduleTimingsModel == null) // ToDo Error message for View
                return false;

            StartTime = scheduleTimingsModel.StartTime;
            EndTime = scheduleTimingsModel.EndTime;

            startHours = Convert.ToInt32(StartTime.Split(':')[0]);
            int startMinutes = Convert.ToInt32(StartTime.Split(':')[1]);
            int endHours = Convert.ToInt32(EndTime.Split(':')[0]);
            int endMinutes = Convert.ToInt32(EndTime.Split(':')[1]);

            this.dtStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startHours, startMinutes, 0);
            this.dtEndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, endHours, endMinutes, 0);
            this.IntervalMinutes = scheduleTimingsModel.Interval_Minutes;
            this.IntervalSeconds = scheduleTimingsModel.Interval_Seconds;
            return true;

            //double tmpSecondsInDecimal = Convert.ToDouble(configuration.Interval_Seconds);
            //double intervalInHours = configuration.Interval_Minutes > 0 ? System.Math.Round(Convert.ToDouble((configuration.Interval_Minutes * 60)) / 3600, 4) : System.Math.Round(Convert.ToDouble(tmpSecondsInDecimal / 3600), 4);
        }

        private bool Validation()
        {
            return true;
        }
        public void StartScheduler()
        {
           bool isSuccessRead = GetSchedulerTimingsData();
            if (isSuccessRead)
            {
               if(Validation() == true)
                {
                    Action<string> ActionCurrentWeather = new Action<string>(GetCurrentWeatherForCity);
                    SchedulerService.Instance.ScheduleTaskWithInterval(11, 58, 1, 0, ActionCurrentWeather,"Salzburg");
                    //GetCurrentWeatherForCity("Salzburg");
                }
                else
                {
                    //ToDo : Error message for View
                }
                {
                    
                }
            }
        }

        private async void GetCurrentWeatherForCity(string city)
        {
            WeatherApiAccess weatherApi = new WeatherApiAccess();
            WebApiException ex = await weatherApi.GetAsyncCurrentWeather(city);
            if (ex == null)
            {
                IsError = false;
                JsonWeather jsonWeatherObject = JsonService.DeserializeObject<JsonWeather>(weatherApi.JsonString);
                WeatherModel weather = jsonWeatherObject;
                WeatherDataContainer.Add(weather);
                ProgramMessage = $"Die Wetterdatenabfrage für {city} ist fehlerfrei abgeschlossen worden.";
            }
            else
            {
                IsError = true;
                ProgramMessage = $"{ex.StatusCode} - {ex.StatusMessage} - {city}";
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
