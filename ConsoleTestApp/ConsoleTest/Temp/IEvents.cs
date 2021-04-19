using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Temp
{
    public interface IEvents
    {
        event EventHandler<string> SaveDataEvent;
    }
}
