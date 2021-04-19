using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperWebSocket;

namespace ConsoleSocketServer
{
    public class WSCompanyServer
    {
        private static WebSocketServer wsServer;
        public void InitServer()
        {
            wsServer = new WebSocketServer();
            int port = 8088;
            wsServer.Setup(port);

            wsServer.NewSessionConnected += WsServer_NewSessionConnected;
            wsServer.NewMessageReceived += WsServer_NewMessageReceived;
            wsServer.NewDataReceived += WsServer_NewDataReceived;
            wsServer.SessionClosed += WsServer_SessionClosed;
        }
        public Exception StartServer()
        {
            return wsServer.Start() == true ? null : new Exception("Socket Server could not start....");
        }

        private void WsServer_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            Console.WriteLine($"Session with Client has been closed.");
        }

        private void WsServer_NewDataReceived(WebSocketSession session, byte[] value)
        {
            
        }

        private void WsServer_NewMessageReceived(WebSocketSession session, string value)
        {
            Console.WriteLine($"{value} received from Client");
            session.Send("Ack. sent from Server");
        }

        private void WsServer_NewSessionConnected(WebSocketSession session)
        {
            Console.WriteLine($"new Session with Client");
        }
    }
}
