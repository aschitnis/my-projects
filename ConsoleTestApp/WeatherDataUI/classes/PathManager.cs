using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProjects.WeatherData.Wpf.WeatherDataUI.classes
{
    internal static class PathManager
    {
        internal static readonly string WEATHER_HTTP_PATH = @"http://api.openweathermap.org/data/2.5/weather?q=";
        internal static readonly string WEATHER_WEBSERVICE_LICENSEKEY = @"&lang=de&appid=98ec98ab7c0f616ddae7a6c4be445e58";
    }
}
