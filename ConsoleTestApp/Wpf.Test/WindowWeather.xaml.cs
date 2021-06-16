using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Wpf.Test.my.weather.viewmodels;

namespace Wpf.Test
{
    /// <summary>
    /// Interaktionslogik für WindowWeather.xaml
    /// </summary>
    public partial class WindowWeather : Window
    {
        private event EventHandler<ulong> LongRunningTaskEvent;

        private WeatherViewModel weatherVM;
        public WeatherViewModel WeatherVM
        {
            get { return weatherVM; }
            set { weatherVM = value; }
        }
        public WindowWeather()
        {
            InitializeComponent();
            WeatherVM = new WeatherViewModel();
            this.DataContext = WeatherVM;

            LongRunningTaskEvent += WindowWeather_LongRunningTaskEvent;
        }

        private void WindowWeather_LongRunningTaskEvent(object sender, ulong e)
        {
           DispatcherOperation d = tbMessage.Dispatcher.BeginInvoke(
                                    (Action)(() => { 
                                                    tbMessage.Text = Convert.ToString(e); 
                                                   }));
           d.Completed += delegate (object s, EventArgs a) {  };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WeatherVM.StartScheduler();
        }

        private void dgExtendedData_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var se = sender;
        }

        //public void GetAllPrimes(ulong inputnumber)
        //{
        //    var primes = new List<ulong>();
        //    primes.Add(2);
        //    primes.Add(3);
        //    bool isprime = false;
        //    double result = 0;
        //    for (ulong i = 4; i < inputnumber; i++)
        //    {
        //        isprime = true;
        //        foreach (ulong prime in primes)
        //        {
        //            result = i % prime;
        //            if (result == 0)
        //            {
        //                isprime = false;
        //                break;
        //            }
        //        }
        //        if (isprime == true)
        //        {
        //            LongRunningTaskEvent.Invoke(this, i);
        //            primes.Add(i);
        //        }
        //    }
        //    int numberofprimes = primes.Count;
        //}
    }
}
