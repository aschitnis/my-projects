using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfOrderBooksProject.Canvasses
{
    /// <summary>
    /// Interaktionslogik für PanelDock.xaml
    /// </summary>
    public partial class PanelDock : Window
    {
        public PanelDock()
        {
            InitializeComponent();
        }

        private void TbButton_Checked(object sender, RoutedEventArgs e)
        {
            txbGkkNummer.Text = "0902718573";
            txbDatum.Text = "25.03.2020";
            txbKontoNr.Text = "78393 8702256";
        }

        private void TbButton_Unchecked(object sender, RoutedEventArgs e)
        {
            txbGkkNummer.Text = "";
            txbDatum.Text = "";
            txbKontoNr.Text = "";
        }
    }
}
