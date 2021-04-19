using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 1) Create a new delegate instance/property & instantiate it.
/// 2) Subscribe/Attach a delegate(method) to a EventHandler instance using anonymous method.  
/// </summary>
namespace ConsoleTestApp.temp.classes
{
    public class MyDelegateExample
    {
        private event EventHandler<List<object>> PrintEvent;
        // declare a delegate
        public delegate void DelSave(int iNum, string str);

        // create a property of type delegate DelSave
        public DelSave SaveDataDelegate { get; set; }
        public MyDelegateExample()
        {
            // instantiate the delegate
            SaveDataDelegate = new DelSave(SaveDataToDatabase);

            PrintEvent -= delegate (object s, List<object> list)
                            {
                                PrintDataToConsole(s, list);
                            };
            // instantiate a EventHandler. A EventHandler is a delegate type.
            PrintEvent += delegate (object s, List<object> list)
                            {
                                PrintDataToConsole(s, list);
                            };
        }

        private void PrintDataToConsole(object sender, List<object> data)
        {
            Console.WriteLine($"Print output to Console: {data[0]} -- {data[1]}");
        }
        private void PrintDataToTerminal(object sender, List<object> data)
        {
            Console.WriteLine($"Print output to TERMINAL: {data[0]} -- {data[1]}");
        }

        private void SaveDataToDatabase(int _age, string _firstname)
        {
            Console.WriteLine($"saving to Database: {_age}  {_firstname}");
        }
        private void SaveDataToFile(int _age, string _firstname)
        {
            Console.WriteLine($"saving to local file: {_age}  {_firstname}");
        }

        public void CallEventPrintData(object o, List<object> datalist)
        {
            if (PrintEvent != null)
                PrintEvent.Invoke(o, datalist);
        }
    }
}
