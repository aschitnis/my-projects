using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfForexRates
{
    public static class AppCommands
    {
        private static ICommand exit = new CExit();
        public static ICommand Exit
        {
            get { return exit; }
        }
    }
}
