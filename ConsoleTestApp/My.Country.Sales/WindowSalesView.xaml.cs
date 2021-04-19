using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using my.country.sales.viewmodels;

namespace my.country.sales
{
    /// <summary>
    /// Interaktionslogik für WindowSalesView.xaml
    /// </summary>
    public partial class WindowSalesView : Window
    {
        public SalesViewModel SalesVM { get; set; } 
        public WindowSalesView()
        {
            InitializeComponent();
            SalesVM = new SalesViewModel();
            DataContext = SalesVM;
         }
    }
}
