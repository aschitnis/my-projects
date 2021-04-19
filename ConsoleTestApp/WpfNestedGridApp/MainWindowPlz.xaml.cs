using System.Windows;
using WpfNestedGridApp.json;

namespace WpfNestedGridApp
{
    /// <summary>
    /// Interaktionslogik für MainWindowPlz.xaml
    /// </summary>
    public partial class MainWindowPlz : Window
    {
        private CPlzJsonSerializationViewModel plzJsonSerializationViewModelobject;

        public CPlzJsonSerializationViewModel PlzJsonSerializationViewModelobject
        {
            get {
                if (plzJsonSerializationViewModelobject == null)
                    plzJsonSerializationViewModelobject = new CPlzJsonSerializationViewModel();
                    return plzJsonSerializationViewModelobject;
                }
            set { plzJsonSerializationViewModelobject = value; }
        }

        public MainWindowPlz()
        {
            InitializeComponent();
            DataContext = PlzJsonSerializationViewModelobject;
        }
    }
}
