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
using Wpf.Test.my.books.management.MVVM.Dialog;

namespace Wpf.Test
{
    /// <summary>
    /// Interaktionslogik für DialogNewBook.xaml
    /// </summary>
    public partial class DialogNewBookView : Window, IDialog
    {
        public DialogNewBookView()
        {
            InitializeComponent();
        }
    }
}
