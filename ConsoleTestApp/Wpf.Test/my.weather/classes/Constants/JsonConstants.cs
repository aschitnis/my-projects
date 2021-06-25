using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.weather.classes.Constants
{
    public abstract class JsonConstants
    {
        public enum JsonTypes: int { ScheduleTaskConfiguration = 0, CurrentWeather = 1, Countries = 2, Currencies = 3, Geocoding = 4 }
    }
}
