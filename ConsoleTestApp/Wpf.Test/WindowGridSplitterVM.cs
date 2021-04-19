using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test
{
    public class WindowGridSplitterVM : INotifyPropertyChanged
    {
        private List<Customer> kunden;
        public List<Customer> Kunden
        {
            get { return kunden; }
            set { kunden = value; OnPropertyChanged(); }
        }

        public WindowGridSplitterVM()
        {
            Kunden = new List<Customer>();
            Kunden.Add(new Customer { Ids = "ASC-Abhijit", Firstname="Abhijit", Lastname="Chitnis", City="Salzburg-Stadt" });
            Kunden.Add(new Customer { Ids = "PSC-Prasanna", Firstname = "Prasanna", Lastname = "Chitnis", City="Mumbai" });
            Kunden.Add(new Customer { Ids = "MHK-Michael", Firstname = "Michael", Lastname = "Krbecek", City="Krems" });
            Kunden.Add(new Customer { Ids = "EHK-Elfriede", Firstname = "Elfriede", Lastname = "Krbecek", City="Istanbul" });
            Kunden.Add(new Customer { Ids = "MHD-Markus", Firstname = "Markus", Lastname = "Hochradl", City="Wien" });
        }

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class Customer : INotifyPropertyChanged
    {
        private string ids;
        private string firstname;
        private string lastname;
        private string city;

        public string Ids 
        { get { return ids; } set { ids = value;OnPropertyChanged(); } }
        public string Firstname
        { get { return firstname; } set { firstname = value; OnPropertyChanged(); } }
        public string Lastname
        { get { return lastname; } set { lastname = value; OnPropertyChanged(); } }
        public string City
        { 
            get { return city; } 
            set { city = value; OnPropertyChanged(); } }
        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
