using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.events
{
    public class CKontoCheckEventArgs : EventArgs
    {
        public CKontoDetails Details { get; private set; }
        public string Name { get; private set; }

        public CKontoCheckEventArgs(string name)
        {
            this.Name = name;
        }
        public CKontoCheckEventArgs(CKontoDetails details)
        {
            Details = details;
        }
    }
}
