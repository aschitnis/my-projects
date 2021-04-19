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

namespace WpfOrderBooksProject.Canvasses
{
    /// <summary>
    /// Interaktionslogik für PanelGrid.xaml
    /// </summary>
    public partial class PanelGrid : Window
    {
        public PanelGrid()
        {
            InitializeComponent();
        }

        private void BtnLayer_Click(object sender, RoutedEventArgs e)
        {
           if ( layer0.Visibility == Visibility.Collapsed )
            {
                layer0.Visibility = Visibility.Visible;
                layer1.Visibility = Visibility.Collapsed;
            }
           else if (layer1.Visibility == Visibility.Collapsed)
            {
                layer1.Visibility = Visibility.Visible;
                layer0.Visibility = Visibility.Collapsed;
            }
        }
    }
}
