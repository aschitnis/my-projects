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
using Wpf.Test.my.books.management;
using System.Drawing;


namespace Wpf.Test
{
    /// <summary>
    /// Interaktionslogik für MainBooksWindow.xaml
    /// </summary>
    public partial class MainBooksWindow : Window
    {
        public MainBooksWindow()
        {
            InitializeComponent();
            SolidColorBrush brush = Application.Current.FindResource("SchlafTeqColor_Theme_main") as SolidColorBrush;
            if (brush is SolidColorBrush)
            {
                SolidColorBrush brushnew = new SolidColorBrush(AdjustBrightness(0.999, brush));
                var hexValue = brushnew.Color.ToString();
                bTest.Background = brushnew;
            }
        }

        #region Events
        //public delegate void BookIdDuplicateCheckHandler(int id);
        //public static event EventHandler<int> OnCheckDuplicateBookId;
        #endregion

        private System.Windows.Media.Color AdjustBrightness(double brightnessFactor, SolidColorBrush originalBrush)
        {
            System.Drawing.Color DGH_adjustedColour = System.Drawing.Color.FromArgb(Convert.ToInt32(originalBrush.Color.A * brightnessFactor),
                                                                                    Convert.ToInt32(originalBrush.Color.R * brightnessFactor),
                                                                                    Convert.ToInt32(originalBrush.Color.G * brightnessFactor),
                                                                                    Convert.ToInt32(originalBrush.Color.B));

            return System.Windows.Media.Color.FromArgb(DGH_adjustedColour.A, DGH_adjustedColour.R, DGH_adjustedColour.G, DGH_adjustedColour.B);
        }
    }
}
