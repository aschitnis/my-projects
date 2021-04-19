using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.http.service.currency
{
    public class CCurrencyModel : ViewModelBase
    {
        public CCurrencyModel()
        {

        }

        public string DisplayName { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string NamePlural { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void RaisePropertyChanged([CallerMemberName] string caller = "")
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(caller));
        //    }
        //}
    }
}
