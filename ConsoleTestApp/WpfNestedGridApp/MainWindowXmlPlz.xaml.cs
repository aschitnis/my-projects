using System.Windows;
using WpfNestedGridApp.viewmodels.xml;
using WpfNestedGridApp.xml;

namespace WpfNestedGridApp
{
    /// <summary>
    /// Interaktionslogik für MainWindowXmlPlz.xaml
    /// </summary>
    public partial class MainWindowXmlPlz : Window
    {
        private CBundRootViewModel _bundplzviewmodel;

        public CBundRootViewModel Bundplzviewmodel
        {
            get
            {
                if (_bundplzviewmodel == null)
                    _bundplzviewmodel = new CBundRootViewModel(new CBundSerializationService());
                return _bundplzviewmodel;
            }
            set { _bundplzviewmodel = value; }
        }
        public MainWindowXmlPlz()
        {
            InitializeComponent();
            DataContext = Bundplzviewmodel;
        }
    }
}
