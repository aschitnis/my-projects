using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using wpf.websocket.client.classes;

namespace wpf.websocket.client
{
    public class SocketViewModel : ViewModelBase
    {
        private List<string> currenciesnames;
        private string selectedcurrencyname;
        private CurrenciesData CurrenciesStructDataModel { get; set; } = new CurrenciesData();

        public WebSocket WebSocketInstance 
        { 
            get 
            { 
                return ClientSocket.GetClientSocket().WebSocketClient;
            }  
        }
 

        public List<string> CurrenciesNames { get => currenciesnames ?? default ; set { currenciesnames = value; OnPropertyChanged(); } }
        public string SelectedCurrencyName { get => selectedcurrencyname; set { selectedcurrencyname = value; OnPropertyChanged(); } }
        private List<string> CurrencyCodes { get; set; } = new List<string>();

        public SocketViewModel()
        {
            WebSocketInstance.OnMessage += WebSocket_OnMessageReceivedFromServer;
            WebSocketInstance.OnError += WebSocketInstance_OnError;
            Init();
        }

        private void WebSocketInstance_OnError(object sender, ErrorEventArgs e)
        {
            string msg = e.Message;
            if (WebSocketInstance.IsAlive)
                WebSocketInstance.Close();
        }

        private void Init()
        {
            CurrenciesStructDataModel = new CurrenciesData();
            CurrenciesNames = new List<string>();
            CurrenciesStructDataModel.Currencies.ForEach((c) => { CurrenciesNames.Add(c.Name);CurrencyCodes.Add(c.Code); }) ;
        }

        public string GetSelectedCurrencyCode()
        {
            return CurrenciesStructDataModel.Currencies.Where(c => c.Name == SelectedCurrencyName).FirstOrDefault().Code;
        }

        public void SendToSocketServer()
        {
            string code = GetSelectedCurrencyCode();
            WebSocketInstance.Connect();
            if (WebSocketInstance.IsAlive == false)
            {
            }
            else
            {
                try
                {
                    WebSocketInstance.Send(code);
                }
                catch(Exception e)
                {
                    string str = e.Message;
                }
            }
        }

        private void WebSocket_OnMessageReceivedFromServer(object sender, MessageEventArgs e)
        {
            string data = e.Data;
            WebSocketInstance.Close();
        }
    }
}
