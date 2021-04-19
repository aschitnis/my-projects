using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfNestedGridApp.json
{
    public class CBunddata : INotifyPropertyChanged
    {
        public CBunddata()
        {
            results = new ObservableCollection<CPlzModel>();
        }

        private ObservableCollection<CPlzModel> _results;

        public ObservableCollection<CPlzModel> results
        {
            get { return _results; }
            set { _results = value; OnPropertyChanged("results"); }
        }

        private ObservableCollection<CPlzModel> _tmpresults;

        public ObservableCollection<CPlzModel> TmpResults
        {
            get { return _tmpresults; }
            set { _tmpresults = value; OnPropertyChanged("TmpResults"); }
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
