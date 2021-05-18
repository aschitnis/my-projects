using Demo.Ws.Server.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Demo.Ws.Server.socket.services
{
    public class WeatherService : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            base.OnMessage(e);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
        }
    }

    public class CurrencyService : WebSocketBehavior
    {
        protected async override void OnMessage(MessageEventArgs e)
        {
            //await Task.Factory.StartNew(() => { });
            //base.OnMessage(e);
            ApiAccess api = new ApiAccess();
            using (var socket = new ClientWebSocket())
            {
                try
                {
                    await socket.ConnectAsync(new Uri("https://api.currencyscoop.com/v1/latest?base=INR&api_key=e765634f4c80f76ea733e1e5da897a39"), CancellationToken.None);

                    await api.Send(socket,)
                    //await Receive(socket);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR - {ex.Message}");
                }
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
        }
    }
}
