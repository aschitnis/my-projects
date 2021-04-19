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

        private static string JsonScheduledTaskTimeString = null;
        private static bool IsJsonFileReadSuccess = false;
        private static Exception ErrorException = null;

        private string _starttime;
        private string _endtime;
        private int _intervalseconds;
        private int _intervalminutes;
        private string _programmessage;
        private bool _iserror;
        private ObservableCollection<Weather> _weatherdatacontainer; 

        private DateTime dtEndTime;
        private DateTime dtStartTime;

        #region Properties
        public int IntervalSeconds { get { return _intervalseconds; } set { _intervalseconds = value; OnChanged(); } }
        public int IntervalMinutes { get { return _intervalminutes; } set { _intervalminutes = value; OnChanged(); } }
        public string StartTime { get { return _starttime; } set { _starttime = value; OnChanged(); } }
        public string EndTime { get { return _endtime; } set { _endtime = value; OnChanged(); } }
        public string ProgramMessage { get { return _programmessage; } set { _programmessage = value; OnChanged(); } }
        public bool IsError { get { return _iserror; } set { _iserror = value; OnChanged(); } }

        public ObservableCollection<Weather> WeatherDataContainer
        {
            get 
            {
                if (_weatherdatacontainer == null)
                    _weatherdatacontainer = new ObservableCollection<Weather>();
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
            ErrorException = ReadJsonFile(out json);
            JsonScheduledTaskTimeString = json;

            if (ErrorException == null)
                IsJsonFileReadSuccess = true;
            else
                IsJsonFileReadSuccess = false;
        }
        #endregion
        private static Exception ReadJsonFile(out string json)
        {
            return PathManager.ReadFile(out json);
        }

        /// <summary>
        /// deserialize the json string to a object of class JsonScheduler.
        /// set the start, the end and the interval time for running a task. 
        /// </summary>
        private bool DeserializeSchedulerData()
        {
            if (IsJsonFileReadSuccess == false)
                return false;

            JsonScheduler taskscheduler = JsonService.DeserializeObject<JsonScheduler>(JsonScheduledTaskTimeString);  // JsonManager.DeserializeToObject(typeof(JsonWeatherConfiguration));
            if (taskscheduler == null) // ToDo Error message for View
                return false;

            this.StartTime = taskscheduler.StartTime;
            this.EndTime = taskscheduler.EndTime;

            int startHours = Convert.ToInt32(StartTime.Split(':')[0]);
            int startMinutes = Convert.ToInt32(StartTime.Split(':')[1]);
            int endHours = Convert.ToInt32(EndTime.Split(':')[0]);
            int endMinutes = Convert.ToInt32(EndTime.Split(':')[1]);

            this.dtStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startHours, startMinutes, 0);
            this.dtEndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, endHours, endMinutes, 0);
            this.IntervalMinutes = taskscheduler.Interval_Minutes;
            this.IntervalSeconds = taskscheduler.Interval_Seconds;
            return true;

            //double tmpSecondsInDecimal = Convert.ToDouble(configuration.Interval_Seconds);
            //double intervalInHours = configuration.Interval_Minutes > 0 ? System.Math.Round(Convert.ToDouble((configuration.Interval_Minutes * 60)) / 3600, 4) : System.Math.Round(Convert.ToDouble(tmpSecondsInDecimal / 3600), 4);
        }

        private bool Validation()
        {
            return true;
        }
        private void StartTask()
        {
           bool isSuccessRead = DeserializeSchedulerData();
            if (isSuccessRead)
            {
               if(Validation() == true)
                {

                   // SchedulerService.Instance.ScheduleTaskWithInterval()
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
            ApiResponseException ex = await weatherApi.GetAsyncCurrentWeather(city);
            if (ex == null)
            {
                IsError = false;
                JsonWeather jsonWeatherObject = JsonService.DeserializeObject<JsonWeather>(weatherApi.JsonString);
                Weather weather = jsonWeatherObject;
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
