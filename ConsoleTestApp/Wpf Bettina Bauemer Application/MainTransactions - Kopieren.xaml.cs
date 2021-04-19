using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Wpf_Bettina_Bauemer_Application.view.root;

namespace Wpf_Bettina_Bauemer_Application
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainTransactionsCopy : Window
    {
        private MainDataView mainviewobject;

        public MainDataView MainViewObject
        {
            get { return mainviewobject; }
            set { mainviewobject = value; }
        }

        public MainTransactionsCopy()
        {
            InitializeComponent();

            Init();
            mainwindow.DataContext = MainViewObject.MaindataContainer;
        }

        private void Init()
        {
            MainViewObject = new MainDataView();
        }
    }
}
