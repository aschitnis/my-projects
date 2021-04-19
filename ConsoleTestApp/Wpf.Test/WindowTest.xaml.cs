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
using System.Windows.Shapes;
using Wpf.Test.my.books.management;
using static Wpf.Test.MyCollectionViewModel;

namespace Wpf.Test
{
    /// <summary>
    /// Interaktionslogik für WindowTest.xaml
    /// </summary>
    public partial class WindowTest : Window
    {
        public MyCollectionViewModel WorkersMainViewModel { get; set; }

        public WindowTest()
        {
            InitializeComponent();
            WorkersMainViewModel = new MyCollectionViewModel();
            this.DataContext = WorkersMainViewModel;
        }
    }
}
