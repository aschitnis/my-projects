using schnittstelle.http.service.currency;
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

namespace WpfForexRates
{
    /// <summary>
    /// Interaktionslogik für WindowTestPerson.xaml
    /// </summary>
    public partial class WindowTestPerson : Window
    {
        public PersonViewModel personVM { get; set; }
        public WindowTestPerson()
        {
            InitializeComponent();
            personVM = new PersonViewModel();
            this.DataContext = personVM;
        }
    }
}
