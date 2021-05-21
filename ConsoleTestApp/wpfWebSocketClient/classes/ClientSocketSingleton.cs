using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace wpf.websocket.client.classes
{
    // The Singleton Class with a single instance of a WebSocket
    public class ClientSocket
    {
        private static ClientSocket _instance;
        private WebSocket websocket;
        public WebSocket WebSocketClient
        {
            get 
            {
                if (websocket == null)
                    websocket = new WebSocket(ApiServicePath.CurrencySubscriptionServiceEndpoint);
                return websocket;
            }
        }


        // Lock synchronization object
        private static object syncLock = new object();

        protected ClientSocket()
        {
           websocket = new WebSocket(ApiServicePath.CurrencySubscriptionServiceEndpoint);
        }

        public static ClientSocket GetClientSocket()
        {
            // Support multithreaded applications through 'Double checked locking' pattern which (once the instance exists) avoids locking each time the method is invoked
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null) 
                    { 
                        _instance = new ClientSocket();
                    }
                }
            }
            return _instance;
        }
    }
}
