using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Test.my.books.management;
using Wpf.Test.my.books.management.MVVM.Dialog;
using Wpf.Test.my.books.management.ViewModels;
using Wpf.Test.my.weather.classes;
using Wpf.Test.my.weather.models.json;
using Wpf.Test.Properties;

namespace Wpf.Test
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            //IDialogService dialogService = new DialogService(MainWindow);

            //dialogService.Register<DialogNewBookViewModel, DialogNewBookView>();
            //MainBooksWindowViewModel mainviewModel = new MainBooksWindowViewModel(dialogService);
            //MainBooksWindow mainbooksview = new MainBooksWindow { DataContext = mainviewModel };

            //mainbooksview.ShowDialog();
        }
        public static double IntervalInHours { get; private set; }

        private void ConvertIntervalToHours(JsonScheduler configuration)
        {
            IntervalInHours = configuration.Interval_Minutes > 0 ? System.Math.Round(Convert.ToDouble((configuration.Interval_Minutes * 60)) / 3600, 4) : 0;
        }
    }
}
