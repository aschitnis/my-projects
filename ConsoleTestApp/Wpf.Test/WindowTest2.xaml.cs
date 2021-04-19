using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Wpf.Test.my.books.management.Testing;

namespace Wpf.Test
{
    /// <summary>
    /// Interaktionslogik für WindowTest2.xaml
    /// </summary>
    public partial class WindowTest2 : Window
    {
        public TestVm MyProperty { get; set; }
        public WindowTest2()
        {
            InitializeComponent();
            MyProperty = new TestVm();
            MyProperty.FillCollectionWithIndexData();
            DataContext = MyProperty;
        }
    }
}
