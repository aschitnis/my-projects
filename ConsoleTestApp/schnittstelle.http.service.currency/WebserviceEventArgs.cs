using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.http.service.currency.customeventargs
{
    public class WebserviceEventArgs
    {
        public string Message { get; set; }
        public WebserviceEventArgs(string msg)
        {
            this.Message = msg;
        }
    }
}
