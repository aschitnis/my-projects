using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;

using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading;
using System.IO;
using System.Runtime.CompilerServices;
using Demo.Ws.Server.socket.services;

namespace Demo.Ws.Server
{
    //public class Echo : WebSocketBehavior
    //{
    //    protected override void OnMessage(MessageEventArgs e)
    //    {
    //        Console.WriteLine($"Nachricht vom ECHO Client: {e.Data}");
    //        Send("Nachricht an ECHO Client-- " + e.Data);

    //    }
    //}

    public class EchoAll : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine($"Nachricht vom ECHO-ALL Client: {e.Data}");
            Sessions.Broadcast("Nachricht an ECHO-ALL Client: " + e.Data);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:7890");

            wssv.AddWebSocketService<CurrencyService>("/CurrencyService");
            //wssv.AddWebSocketService<EchoAll>("/EchoAll");

            wssv.Start();
            
            Console.WriteLine("Server-Dienst "+ "ws://127.0.0.1:7890/CurrencyService gestartet");
            //Console.WriteLine("Server-Dienst " + "ws://127.0.0.1:7890/EchoAll gestartet");
            // wss://ws.twelvedata.com/v1/quotes/price?apikey=a54164d144f245f6a1078d437e87abb6
              

            Console.ReadKey();
            wssv.Stop();
        }
    }
}
