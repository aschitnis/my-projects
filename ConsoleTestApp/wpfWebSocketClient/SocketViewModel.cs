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
        private string socketstatusmessage;
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
 
        public string SocketStatusMessage { get => socketstatusmessage; set { socketstatusmessage = value;OnPropertyChanged(); } }
        public List<string> CurrenciesNames { get => currenciesnames ?? default ; set { currenciesnames = value; OnPropertyChanged(); } }
        public string SelectedCurrencyName { get => selectedcurrencyname; set { selectedcurrencyname = value; OnPropertyChanged(); } }
        private List<string> CurrencyCodes { get; set; } = new List<string>();

        public SocketViewModel()
        {
            WebSocketInstance.OnMessage += WebSocket_OnMessageReceivedFromServer;
            WebSocketInstance.OnError += WebSocketInstance_OnError;
            WebSocketInstance.OnClose += WebSocketInstance_OnClose;
            Init();
        }

        private void WebSocketInstance_OnClose(object sender, CloseEventArgs e)
        {
            var zubringer = sender;
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

        private Task<bool> GetTaskConnectToSocketServer()
        {
            Task<bool> t = Task.Factory.StartNew<bool>(() =>
                {
                    WebSocketInstance.Connect();
                    if (WebSocketInstance.IsAlive == false)
                    {
                        SocketStatusMessage = "Connection to the Web-Socket is lost on the Main-Thread";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                });
            t.ContinueWith((a) =>
               {
                    if (WebSocketInstance.IsAlive)
                    {
                        SocketStatusMessage = "Connection to the Web-Socket is persistant";
                       var taskCompletionSource = new TaskCompletionSource<bool>();
                       string currencyCode = GetSelectedCurrencyCode();

                       try
                       {
                           WebSocketInstance.SendAsync(currencyCode, success =>
                           {
                               taskCompletionSource.SetResult(success);
                           });
                       }
                       catch (Exception ex)
                       {
                           taskCompletionSource.SetException(ex);
                       }
                       if (taskCompletionSource.Task.Result == true)
                           SocketStatusMessage = "Data was sent successfully to the Socket on the Server";
                   }
                    else
                    {
                       SocketStatusMessage = "Connection to the Web-Socket is lost on the Main-Thread [002]";
                    }
               });
            return t;
        }
        public async void ConnectAsyncToSocketServer()
        {
           bool hasConnectionSuccedded = await GetTaskConnectToSocketServer();
            //if (hasConnectionSuccedded)
            //{
            //    SocketStatusMessage = "Connection to Web-Socket established";
            //}
            //else
            //{
            //    SocketStatusMessage = "Connection to the Web-Socket could not be established";
            //}

        }

        public Task<bool> SendData()
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            string currencyCode = GetSelectedCurrencyCode();
 
            try
            {
                WebSocketInstance.SendAsync(currencyCode, success =>
                {
                    taskCompletionSource.SetResult(success);
                });
            }
            catch(Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }
            return taskCompletionSource.Task;
        }

        public void SendToSocketServer()
        {
            string code = GetSelectedCurrencyCode();
           // WebSocketInstance.WaitTime = new TimeSpan(0, 5, 00);
            WebSocketInstance.ConnectAsync();

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
