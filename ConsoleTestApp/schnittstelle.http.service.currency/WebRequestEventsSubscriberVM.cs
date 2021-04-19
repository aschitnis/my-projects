using schnittstelle.http.service.currency.customeventargs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.http.service.currency.viewmodel
{
    public class WebRequestEventsSubscriberVM : ViewModelBase
    {
        public string ErrorMessage { get; set; }

        public string Message { get; set; }

        public void HandleWebserviceErrorEvent(object sender, WebserviceEventArgs e)
        {
            ErrorMessage = e.Message;
        }

        public void HandleWebserviceMessageEvent(object sender, WebserviceEventArgs e)
        {
            Message = e.Message;
        }

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
