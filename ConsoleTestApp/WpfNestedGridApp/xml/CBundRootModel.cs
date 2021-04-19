using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfNestedGridApp.xml.model
{
    #region Root Model Class
    public class CBundRootModel : INotifyPropertyChanged
    {
        public CBundRootModel()
        {
            ListBundeslaender = new List<CBundModel>();
        }

        private List<CBundModel> _listbundeslaender;
        public List<CBundModel> ListBundeslaender
        {
            get { return _listbundeslaender; }
            set { _listbundeslaender = value; RaisePropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
    #endregion

    #region CGemeindeModel Class
    public class CGemeindeModel : INotifyPropertyChanged
    {
        public CGemeindeModel()
        {
            ListOrte = new List<COrtModel>();
        }
        private string _plz;
        public string Plz { get { return _plz; } set { _plz = value; RaisePropertyChanged(); } }
        private string _name;
        public string Name { get { return _name; } set { _name = value; RaisePropertyChanged(); } }
        private int _einwohner;
        public int Einwohner { get { return _einwohner; } set { _einwohner = value; RaisePropertyChanged(); } }
        private string _lat;
        public string Lat { get { return _lat; } set { _lat = value; RaisePropertyChanged(); } }
        private string _lon;
        public string Lon { get { return _lon; } set { _lon = value; RaisePropertyChanged(); } }
        private double _flaeche;
        public double Flaeche { get { return _flaeche; } set { _flaeche = value; RaisePropertyChanged(); } }
        private string _bezeichnung;
        public string Bezeichnung { get { return _bezeichnung; } set { _bezeichnung = value; RaisePropertyChanged(); } }
        private List<COrtModel> _listOrte;
        public List<COrtModel> ListOrte { get { return _listOrte; } set { _listOrte = value; RaisePropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(
            [CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
    #endregion

    #region CBezirkModel Class 
    public class CBezirkModel : INotifyPropertyChanged
    {
        public CBezirkModel()
        {
            ListGemeinde = new List<CGemeindeModel>();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }

        private string _bezeichnung;
        public string Bezeichnung
        {
            get { return _bezeichnung; }
            set { _bezeichnung = value; RaisePropertyChanged(); }
        } // entspricht typ

        private string _plz;
        public string Plz
        {
            get { return _plz; }
            set { _plz = value; RaisePropertyChanged(); }
        }

        private int _einwohner;
        public int Einwohner
        {
            get { return _einwohner; }
            set { _einwohner = value; RaisePropertyChanged(); }
        }

        private string _lat;
        public string Lat
        {
            get { return _lat; }
            set { _lat = value; RaisePropertyChanged(); }
        }
        private string _lon;
        public string Lon
        {
            get { return _lon; }
            set { _lon = value; RaisePropertyChanged(); }
        }

        private double _flaeche;
        public double Flaeche
        {
            get { return _flaeche; }
            set { _flaeche = value; RaisePropertyChanged(); }
        }

        private List<CGemeindeModel> _listgemeinde;
        public List<CGemeindeModel> ListGemeinde
        {
            get { return _listgemeinde; }
            set { _listgemeinde = value; RaisePropertyChanged(); }
        }

        #region Eventing
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(
            [CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
        #endregion
    }
    #endregion

    #region CBundModel Class
    public class CBundModel : INotifyPropertyChanged
    {
        public CBundModel()
        {
            ListBezirke = new List<CBezirkModel>();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }
        private int _einwohner;
        public int Einwohner
        {
            get { return _einwohner; }
            set { _einwohner = value; RaisePropertyChanged(); }
        }
        private string _lat;
        public string Lat
        {
            get { return _lat; }
            set { _lat = value; RaisePropertyChanged(); }
        }
        private string _lon;
        public string Lon
        {
            get { return _lon; }
            set { _lon = value; RaisePropertyChanged(); }
        }
        private List<CBezirkModel> _listbezirke;
        public List<CBezirkModel> ListBezirke
        {
            get { return _listbezirke; }
            set { _listbezirke = value; RaisePropertyChanged(); }
        }

        #region Eventing
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(
            [CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
        #endregion
    }
    #endregion

    #region COrtModel Class
    public class COrtModel : INotifyPropertyChanged
    {
        public COrtModel()
        {

        }

        private string _bezeichnung;
        public string Bezeichnung
        {
            get { return _bezeichnung; }
            set { _bezeichnung = value; RaisePropertyChanged(); }
        } // entspricht typ

        private string _plz;
        public string Plz
        {
            get { return _plz; }
            set { _plz = value; RaisePropertyChanged(); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }

        private int _einwohner;
        public int Einwohner
        {
            get { return _einwohner; }
            set { _einwohner = value; RaisePropertyChanged(); }
        }

        private double _lat;
        public double Lat
        {
            get { return _lat; }
            set { _lat = value; RaisePropertyChanged(); }
        }
        private string _lon;
        public string Lon
        {
            get { return _lon; }
            set { _lon = value; RaisePropertyChanged(); }
        }

        private double _flaeche;
        public double Flaeche
        {
            get { return _flaeche; }
            set { _flaeche = value; RaisePropertyChanged(); }
        }

        private int _hoehe;
        public int Hoehe
        {
            get { return _hoehe; }
            set { _hoehe = value; RaisePropertyChanged(); }
        }

        private string _lage;
        public string Lage
        {
            get { return _lage; }
            set { _lage = value; RaisePropertyChanged(); }
        }

        private string _verkehr;
        public string Verkehr
        {
            get { return _verkehr; }
            set { _verkehr = value; RaisePropertyChanged(); }
        }

        #region Eventing
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(
            [CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
        #endregion
    }
    #endregion
}
