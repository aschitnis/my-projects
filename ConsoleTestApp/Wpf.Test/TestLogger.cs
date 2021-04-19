using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test
{
    public static class TestLogger
    {
        public static void AddLogEntry(Exception ex = null)
        {
            ex = new Exception();
        }
    }
}
