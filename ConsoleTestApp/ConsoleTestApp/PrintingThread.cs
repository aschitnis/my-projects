using System;
using System.Threading;

namespace ConsoleTestApp
{
    public class PrintingThread
    {
        private object _token { get; }
        private string name {get;set;}
        private static int count { get; set; }
        private int choice { get;  set; }
        public PrintingThread()
        {
            _token = new object();
        }
        public void ProcessDamper()
        {
            lock(_token)
            {
                Thread.Sleep(2000);
                count++;
                Random rdm = new Random();
                choice = rdm.Next(1, 5637);
                Console.WriteLine("You chose{0}: {1}", count, choice );
                
            }
        }

    }
}
