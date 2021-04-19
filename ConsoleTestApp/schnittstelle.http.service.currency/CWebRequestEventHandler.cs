using schnittstelle.http.service.currency.customeventargs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.http.service.currency.custom.errorevent
{
    public class CWebRequestEventHandler
    {
        public event EventHandler<WebserviceEventArgs> webserviceError;
        public event EventHandler<WebserviceEventArgs> webserviceMessage;
        public CWebRequestEventHandler()
        {

        }
        #region
        public void OnWebserviceMessage(string message)
        {
            if (webserviceMessage != null)
            {
                webserviceMessage(this, new WebserviceEventArgs(message));
            }
        }

        public void OnWebserviceError(string errormessage)
        {
            if (webserviceError != null)
            {
                webserviceError(this, new WebserviceEventArgs(errormessage));
            }
        }
        #endregion
    }
}
