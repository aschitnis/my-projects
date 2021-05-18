using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Ws.Server.classes
{
    // api-key:  e765634f4c80f76ea733e1e5da897a39
    // user : abhijit , aschitnis@hotmail.com
    // password : elfriede51
    // link : https://currencyscoop.com/login
    // z.b. https://api.currencyscoop.com/v1/latest?base=INR&api_key=e765634f4c80f76ea733e1e5da897a39
    public class ApiAccess
    {
        internal async Task Send(ClientWebSocket socket, string data)
        {
           await socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(data)), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
