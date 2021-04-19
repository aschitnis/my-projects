using System.ComponentModel;

namespace WpfNestedGridApp
{
    public class Temp : INotifyPropertyChanged
    {
        private string vorname;

        public string Vorname
        {
            get { return vorname; }
            set { vorname = value; OnPropertyChanged("Vorname"); }
        }
        private string nachname;

        public string Nachname
        {
            get { return nachname; }
            set { nachname = value; OnPropertyChanged("Nachname"); }
        }


        #region PropertyChanged Notification
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
