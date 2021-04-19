using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wpf.Test
{
    #region Worker class
    public class Worker : ViewModelBase
    {
        private string vorname;
        private string nachname;
        private string city;
        private string telefonvorwahl;
        private int id;

        public int ID 
        {
            get { return id; }
            set { id = value; OnChanged(); }
        }
        public string City
        {
            get { return city; }
            set { city = value; OnChanged(); }
        }
        public string TelefonVorwahl
        {
            get { return telefonvorwahl; }
            set { telefonvorwahl = value; OnChanged(); }
        }
        public string Vorname 
        {
            get { return vorname; }
            set { vorname = value;OnChanged(); }
        }
        public string Nachname 
        {
            get { return nachname; }
            set { nachname = value; OnChanged(); }
        }
    }
    #endregion
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum FilterAuswahl 
    {
        [Description("Keine Auswahl")]
        byNone = 0,
        [Description("Nach Vorname")]
        byFirstname = 11,
        [Description("Nach Nachname")]
        byLastname = 12,
        [Description("Nach Telefon-Nr.")]
        byTelefonVorwahl = 13,
        [Description("Nach Stadt")]
        byCity = 14
    }
    public class MyCollectionViewModel: ViewModelBase
    {
        private List<Worker> workers;
        private string filterText;
        private FilterAuswahl filterauswahl;

        #region DELEGATES
        public delegate Worker FilterWorkerDelegate(Worker worker, string filtertext);
        #endregion

        private ObservableCollection<string> filteredlistofworkers;
        public ObservableCollection<string> FilteredListOfWorkers 
        {
            get 
            {
                if (filteredlistofworkers == null)
                    filteredlistofworkers = new ObservableCollection<string>();
                return filteredlistofworkers;
            }
            set { filteredlistofworkers = value; OnChanged(); }
        }
        public FilterAuswahl CurrentFilterValue 
        {
            get { return filterauswahl; }
            set {
                   filterauswahl = value;
                   SearchWorkersAsPerFilterCriteria();
                   OnChanged();
                }
        }
        public string FilterText
        {
            get { return filterText; }
            set { filterText = value; OnChanged(); }
        }
        public List<Worker> Workers
        {
            get { return workers; }
            set { workers = value; OnChanged(); }
        }

        public MyCollectionViewModel()
        {
            Workers = new List<Worker>();
            Workers.Add(new Worker { ID = 12, City = "Salzburg", TelefonVorwahl = "0650", Vorname = "Abhijit", Nachname = "Chitnis" });
            Workers.Add(new Worker { ID = 14, Vorname = "Arnold", TelefonVorwahl = "0650", City = "Linz", Nachname = "Angerer" });
            Workers.Add(new Worker { ID = 15, Vorname = "Zubin", City = "Pune", TelefonVorwahl = "9824", Nachname = "Motafram" });
            Workers.Add(new Worker { ID = 16, Vorname = "Philipp", City = "Mattighofen", TelefonVorwahl = "0660", Nachname = "Brugger" });
            Workers.Add(new Worker { ID = 20, Vorname = "David", City = "Linz", TelefonVorwahl = "0660", Nachname = "Pucher" });
            Workers.Add(new Worker { ID = 11, Vorname = "Rama", City = "Pune", TelefonVorwahl = "9824", Nachname = "Iyer" });
            Workers.Add(new Worker { ID = 17, Vorname = "Armin", City = "Innsbruck", TelefonVorwahl = "0699", Nachname = "Tiroler" });
            Workers.Add(new Worker { ID = 19, Vorname = "Philipp", City = "Wien", TelefonVorwahl = "0699", Nachname = "Guggenberger" });
            Workers.Add(new Worker { ID = 18, Vorname = "Markus", City = "Puch-Hallein", TelefonVorwahl = "0650", Nachname = "Schwarzmann" });
            Workers.Add(new Worker { ID = 13, City = "Salzburg", TelefonVorwahl = "0660", Vorname = "Markus", Nachname = "Hochradl" });
            
            CurrentFilterValue = FilterAuswahl.byNone;
        }

        private void SearchWorkersAsPerFilterCriteria()
        {
            string str = "Oberschicht XD H1 - 7cm";
            string str2 = "Oberschicht H1 - 7XD";
            var y = Regex.Matches(str, @"\bX\S*D\b");
            
            //string wert = s.Where(t => t == "XBAP").DefaultIfEmpty("ARAGON").First() ;
            switch (CurrentFilterValue)
            {
                case FilterAuswahl.byNone:
                    FilteredListOfWorkers.Clear();
                    break;
                case FilterAuswahl.byTelefonVorwahl:
                    FilterWorkersUsingDelegateFunction(delegate (Worker w, string filtervalue) { return w = w.TelefonVorwahl.StartsWith(filtervalue) ? w : null; });
                    break;
                case FilterAuswahl.byLastname:
                    FilterWorkersUsingDelegateFunction(delegate (Worker w, string filtervalue) { return w = w.Nachname.Equals(filtervalue, StringComparison.OrdinalIgnoreCase) ? w : null; });
                    break;
                case FilterAuswahl.byFirstname:
                    FilterWorkersUsingDelegateFunction(delegate (Worker w,string filtervalue) { return w = w.Vorname.Equals(filtervalue, StringComparison.OrdinalIgnoreCase) ? w : null; });
                    break;
                case FilterAuswahl.byCity:
                    FilterWorkersUsingDelegateFunction( (worker,filtervalue) => { return worker.City.Equals(filtervalue, StringComparison.OrdinalIgnoreCase) ? worker : null; });
                    break;
            }
        }

        private void FilterWorkersUsingDelegateFunction(FilterWorkerDelegate workerfilterdelegate)
        {
            this.FilteredListOfWorkers = new ObservableCollection<string>();
            foreach (Worker worker in Workers)
            {
                Worker workerbyfilter = workerfilterdelegate(worker, FilterText);
                if (workerbyfilter != null)
                {
                    this.FilteredListOfWorkers.Add(workerbyfilter.Vorname + "-" + workerbyfilter.Nachname);
                }
            }
            if (FilteredListOfWorkers.Count == 0)
                CurrentFilterValue = FilterAuswahl.byNone;
        }
    }
}
