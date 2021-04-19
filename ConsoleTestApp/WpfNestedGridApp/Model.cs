using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfNestedGridApp.Model
{
    /** 
     * CHUBSTATStatusModel -> Model pro HUBSTAT Datei
     * **/
    public class CHUBSTATStatusModel : INotifyPropertyChanged       //ModelBase
    {
        public CHUBSTATStatusModel()
        {
            OcNveSatz = new ObservableCollection<CNVESatzModel>();
        }
        /**
         * format Datetime to String : hh.mm.ss.ffffff
         */
        private string _modifiedtime;
        public string ModifiedTime
        {
            get { return _modifiedtime; }
            set { _modifiedtime = value; OnPropertyChanged("ModifiedTime"); }
        }

        private string kopfsatz;
        public string Kopfsatz
        {
            get { return kopfsatz; }
            set { kopfsatz = value; OnPropertyChanged("Kopfsatz"); }
        }

        private string versanddepot;
        public string Versanddepot
        {
            //get => versanddepot;
            //set => SetProperty(ref versanddepot, value);
            get { return versanddepot; }
            set { versanddepot = value; OnPropertyChanged("Versanddepot"); }
        }

        private string erstelldatum;

        public string Erstelldatum
        {
            get { return erstelldatum; }
            set { erstelldatum = value; OnPropertyChanged("Erstelldatum"); }
        }

        private string hubdateiname;

        public string HubDateiName
        {
            get { return hubdateiname; }
            set { hubdateiname = value; OnPropertyChanged("HubDateiName"); }
        }

        private string hubfullfilename;

        public string HubFullFileName
        {
            get { return hubfullfilename; }
            set { hubfullfilename = value; OnPropertyChanged("HubFullFileName"); }
        }

        private ObservableCollection<CNVESatzModel> ocNvesatz;
        public ObservableCollection<CNVESatzModel> OcNveSatz
        {
            get { return ocNvesatz; }
            set { ocNvesatz = value;
                   // OnPropertyChanged("OcNveSatz");
                }
        }

        #region PropertyChanged Notification
        public event PropertyChangedEventHandler PropertyChanged;
        // public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

    /** 
    * CNVESatzModel -> Model pro HUBSTAT Datei
    * **/
    public class CNVESatzModel : INotifyPropertyChanged
    {

        private string schluessel;
        public string Schluessel
        {
            get { return schluessel; }
            set { schluessel = value; OnPropertyChanged("Schluessel"); }
        }

        private string nve;
        public string NVE
        {
            get { return nve; }
            set { nve = value; OnPropertyChanged("NVE"); }
        }

        private string empfangsdepot;
        public string Empfangsdepot
        {
            get { return empfangsdepot; }
            set { empfangsdepot = value; OnPropertyChanged("Empfangsdepot"); }
        }

        private string datumuhrzeit;
        public string DatumUhrzeit
        {
            get { return datumuhrzeit; }
            set { datumuhrzeit = value; OnPropertyChanged("DatumUhrzeit"); }
        }

        private string bemerkung;

        public string Bemerkung
        {
            get { return bemerkung; }
            set { bemerkung = value; OnPropertyChanged("Bemerkung"); }
        }

        private string fremdnummer;

        public string FremdNummer
        {
            get { return fremdnummer; }
            set { fremdnummer = value; OnPropertyChanged("FremdNummer"); }
        }

        #region PropertyChanged Notification
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
