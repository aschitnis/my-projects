using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebSocketSharp;

namespace ConsoleWebSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a instance of websocket
            using (WebSocket ws = new WebSocket("ws://127.0.0.1:7890/EchoAll"))
            {
                ws.OnMessage += Ws_OnMessage;

                ws.Connect();

                ws.Send("Hallo Server");

                Console.ReadKey();
            }
        }

        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Sent from Server: " + e.Data);
        }
    }
}
