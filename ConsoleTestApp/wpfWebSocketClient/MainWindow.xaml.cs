using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using wpf.websocket.client.classes;

// using WebSocketSharp;

namespace wpf.websocket.client
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow myinstance;
        #region properties
        public static MainWindow Instance
        {
            get { return myinstance; }
            private set { }
        }
        #endregion

        public SocketViewModel SocketVM { get; set; } = new SocketViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = SocketVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SocketVM.ConnectAsyncToSocketServer();
 
           //string code = SocketVM.GetSelectedCurrencyCode();
        }

        public void RegisterAndDisplayInfoMessage(string message)
        {
            Instance?.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                Instance.infoPopup.ShowInfoMessage(message);
            });
        }
    }
}
