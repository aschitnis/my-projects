using System.ComponentModel;

namespace WpfNestedGridApp.json
{
    public class CPlzModel : INotifyPropertyChanged
    {
        private int _plz;

        public int Plz
        {
            get { return _plz; }
            set { _plz = value; OnPropertyChanged("Plz"); }
        }
        private string _ort;

        public string ort
        {
            get { return _ort; }
            set { _ort = value; OnPropertyChanged("ort"); }
        }
        private string _bundesland;

        public string bundesland
        {
            get { return _bundesland; }
            set { _bundesland = value; OnPropertyChanged("bundesland"); }
        }
        private string _gueltigab;

        public string gueltigab
        {
            get { return _gueltigab; }
            set { _gueltigab = value; OnPropertyChanged("gueltigab"); }
        }
        private string _gueltigbis;

        public string gueltigbis
        {
            get { return _gueltigbis; }
            set { _gueltigbis = value; OnPropertyChanged("gueltigbis"); }
        }
        private string _plztyp;

        public string plztyp
        {
            get { return _plztyp; }
            set { _plztyp = value; OnPropertyChanged("plztyp"); }
        }
        private string _internextern;

        public string internextern
        {
            get { return _internextern; }
            set { _internextern = value; OnPropertyChanged("internextern"); }
        }
        private string _adressierbar;

        public string adressierbar
        {
            get { return _adressierbar; }
            set { _adressierbar = value; OnPropertyChanged("adressierbar"); }
        }
        private string _postfach;

        public string postfach
        {
            get { return _postfach; }
            set { _postfach = value; OnPropertyChanged("postfach"); }
        }

        #region PropertyChanged Notification
        public event PropertyChangedEventHandler PropertyChanged;
        // public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName) );

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
