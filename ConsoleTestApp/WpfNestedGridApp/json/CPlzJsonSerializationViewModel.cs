using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace WpfNestedGridApp.json
{

    public class CPlzJsonSerializationViewModel : INotifyPropertyChanged
    {
        public delegate void FilterDataHandler(object sender, EventArgs e);
        public event FilterDataHandler FilterPublisherEvent;
 
        private string sJsonString { get; set; }
        private string sJsonFile { get; }
        //private FilterGridData delegateFilterGrid { get; set; }

        // Für RadioButton
        private string currentOption;

        public string CurrentOption
        {
            get { return currentOption; }

            set {
                  currentOption = value;
                  OnPropertyChanged("CurrentOption");
                 // call a Event here to filter and update the datagrid
                  OnRaiseFilterEvent();
                }
        }

        private CBunddata bundobject;

        public CBunddata BundData
        {
            get
            {
                if (bundobject == null)
                    bundobject = new CBunddata();
                return bundobject;
            }
            set { bundobject = value; OnPropertyChanged("BundData"); }
        }


        #region Constructor
        public CPlzJsonSerializationViewModel()
        {
            sJsonFile = SingletonProgramConfiguration.Instance.GetConfigurationData()["jsonfile"];
            //ConfigurationManager.AppSettings["JsonFile"].ToString();
            Init();
        }
        #endregion

        #region Functions

        /**
         * This is the method that will invoke the Event 'FilterCompleted'
         * **/
        public void OnRaiseFilterEvent()
        {
            if(FilterPublisherEvent != null)
            {
                FilterPublisherEvent.Invoke(this, EventArgs.Empty);
            }
        }

        /**
         * Subscriber Method for FilterCompleted Event
         * **/
        public void FilterSubscriberMethod(object sender, EventArgs e)
        {
            BundData.results.Clear();
            switch(CurrentOption)
            {
                case "NÖ":
                    BundData.results = new ObservableCollection<CPlzModel>(BundData.TmpResults.Where(x => x.bundesland == "Niederösterreich"));
                    break;
                case "OÖ":
                    BundData.results = new ObservableCollection<CPlzModel>(BundData.TmpResults.Where(x => x.bundesland == "Oberösterreich"));
                    break;
                case "Tirol":
                    BundData.results = new ObservableCollection<CPlzModel>(BundData.TmpResults.Where(x => x.bundesland == "Tirol"));
                    break;
                case "Salzburg":
                    BundData.results = new ObservableCollection<CPlzModel>(BundData.TmpResults.Where(x => x.bundesland == "Salzburg"));
                    break;
                case "Wien":
                    BundData.results = new ObservableCollection<CPlzModel>(BundData.TmpResults.Where(x => x.bundesland == "Wien"));
                    break;
                case "Burgenland":
                    BundData.results = new ObservableCollection<CPlzModel>(BundData.TmpResults.Where(x => x.bundesland == "Burgenland"));
                    break;
                case "Kärnten":
                    BundData.results = new ObservableCollection<CPlzModel>(BundData.TmpResults.Where(x => x.bundesland == "Kärnten"));
                    break;
                case "Alle":
                    BundData.results = new ObservableCollection<CPlzModel>(BundData.TmpResults);
                    break;
            }
            
        }

        private void Init()
        {
            FilterPublisherEvent += FilterSubscriberMethod; 

            try
            {
                sJsonString = File.ReadAllText(sJsonFile);
            }
            catch (Exception) { }

            if (!string.IsNullOrEmpty(sJsonString))
            {
                BundData = JsonConvert.DeserializeObject<CBunddata>(sJsonString);
                foreach(CPlzModel oplz in BundData.results)
                {
                    switch(oplz.bundesland)
                    {
                        case "W": oplz.bundesland = "Wien";
                            break;
                        case "N":
                            oplz.bundesland = "Niederösterreich";
                            break;
                        case "Sa":
                            oplz.bundesland = "Salzburg";
                            break;
                        case "O":
                            oplz.bundesland = "Oberösterreich";
                            break;
                        case "T":
                            oplz.bundesland = "Tirol";
                            break;
                        case "K":
                            oplz.bundesland = "Kärnten";
                            break;
                        case "B":
                            oplz.bundesland = "Burgenland";
                            break;
                    }
                }
                BundData.TmpResults = new ObservableCollection<CPlzModel>(BundData.results);
            }
        }

        #endregion

        #region Commands
        public ICommand BundeslandAbkuerzungUmbenennenCommand { get; set; }
        #endregion

        #region Commands Methods
        public void ExecuteBundeslandAbkuerzungUmbenennenCommand(object parameter)
        {

        }
        #endregion

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
