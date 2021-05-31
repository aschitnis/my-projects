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
        protected override void OnMessage(MessageEventArgs e)
        {
            ApiAccess api = new ApiAccess();
            string responseJson = null;
            //Console.WriteLine($"{e.Data}");
            Exception ex = api.GetAllCurrencyExchangeRatesAsJsonString(e.Data, out responseJson);
            Console.WriteLine(responseJson);
            if (ex == null)
                Sessions.Broadcast(responseJson);
            else
                Sessions.Broadcast($"{ex.Message}");
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
