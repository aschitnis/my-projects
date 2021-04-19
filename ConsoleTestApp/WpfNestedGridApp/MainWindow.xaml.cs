using System.ComponentModel;
using System.Windows;
using WpfNestedGridApp.VModel;

namespace WpfNestedGridApp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private CHUBSTATStatusViewModel hubViewModel;

        public CHUBSTATStatusViewModel HubViewModel
        {
            get
            {
                if (hubViewModel == null)
                    hubViewModel = new CHUBSTATStatusViewModel();
                return hubViewModel;
            }
            set {
                    hubViewModel = value;
                    OnPropertyChanged(nameof(MainWindow.HubViewModel));
                }
        }

        public MainWindow()
        {
            HubViewModel.Verzeichnis = SingletonProgramConfiguration.Instance.GetConfigurationData()["hubstatfolder"];
            DataContext = HubViewModel;
            InitializeComponent();
        }

        #region PropertyChanged Notification
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        // public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
