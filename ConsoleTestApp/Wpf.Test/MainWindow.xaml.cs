using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Wpf.Test
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TestClassOneVM testObject { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Application app = Application.Current;
            app.Exit += App_Exit;

            testObject = new TestClassOneVM();
            this.DataContext = testObject;
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Button)
            {
                Button clickedButton = (e.Source as Button);
                clickedButton.Foreground = Brushes.Red;
            }
            
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
             //throw new NotImplementedException();
        }

        private void BtnOne_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Task.Factory.StartNew(testObject.CalculateActionAsync);
              e.Handled = true;
        }

        private void BtnTwo_Click(object sender, RoutedEventArgs e)
        {
            Task t = Task.Factory.StartNew(testObject.GetCalculatedCountAsync);
        }

        private void BtnThree_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(new Action(() => testObject.FindPrimeNumber())) ;

           // Task.Factory.StartNew(new Action(() => testObject.TestLookUp()));
        }
    }
}
