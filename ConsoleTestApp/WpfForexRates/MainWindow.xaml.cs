using schnittstelle.http.service.currency;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfForexRates
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event EventHandler applicationClose;

        public CurrencyConversionVM CurrencyConverterVM
        {
            get;
            set;
        }

        public MainWindow()
        {
            InitializeComponent();

            CurrencyConverterVM = new CurrencyConversionVM();
            this.DataContext = CurrencyConverterVM;

            applicationClose += ((sender, args) => { Application current = Application.Current; current.Shutdown(0); });
        }

        public void OnApplicationClosing()
        {
            applicationClose?.Invoke(this, EventArgs.Empty);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.OnApplicationClosing();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
