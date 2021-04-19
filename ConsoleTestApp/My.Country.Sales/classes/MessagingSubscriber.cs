using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace my.country.sales.classes
{
    public class MessagingSubscriber : INotifyPropertyChanged
    {
        private string currentweathereventmessage;
        private string messagesingletoncontainer;
        public string CurrentWeatherEventMessage
        {
            get { return currentweathereventmessage; }
            set { currentweathereventmessage = value; OnPropertyChanged(); }
        }
        public string MessageSingletonContainer 
        { 
            get { return messagesingletoncontainer; } 
            set 
            { 
                messagesingletoncontainer = value;
                OnPropertyChanged(); 
            } 
        }
        public void RaiseSingletonSalesDataEvent(object sender, string message)
        {
            MessageSingletonContainer = message;
        }
        public void RaiseSingletonSalesDataErrorEvent(object sender, string message)
        {
            MessageSingletonContainer = message;
        }
        public void RaiseCurrentWeatherDataEvent(object sender, string message)
        {
            CurrentWeatherEventMessage = message;
        }

        #region NotifyChanged Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
