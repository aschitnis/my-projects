using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wpf.Test.my.weather.models.json;

namespace Wpf.Test.my.weather.models
{
    public class SchedulerModel : INotifyPropertyChanged
    {
        private string _starttime;
        private string _endtime;
        private int _intervalseconds;
        private int _intervalminutes;

        public int IntervalSeconds { get { return _intervalseconds; } set { _intervalseconds = value; OnChanged(); } }
        public int IntervalMinutes { get => _intervalminutes; set { _intervalminutes = value; OnChanged(); } }
        public string StartTime { get => _starttime ?? "00:00"; set { _starttime = value; OnChanged(); } }
        public string EndTime { get => _endtime ?? "00:00"; set { _endtime = value; OnChanged(); } }

        #region constructors
        public SchedulerModel() { }
        // copy constructors
        public SchedulerModel(JsonSchedulerModel jsonmodel) 
        {
            StartTime = jsonmodel.StartTime;
            EndTime = jsonmodel.EndTime;
            IntervalMinutes = jsonmodel.Interval_Minutes;
            IntervalSeconds = jsonmodel.Interval_Seconds;
        }
        #endregion

        public static implicit operator SchedulerModel(JsonSchedulerModel jsonmodel)
        {
            SchedulerModel model = new SchedulerModel(jsonmodel);
            return model;
        }

        #region NotifyChanged Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
