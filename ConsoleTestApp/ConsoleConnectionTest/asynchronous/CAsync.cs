using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleConnectionTest.asynchronous
{
    public class CAsyncWorker
    {
        public bool IsComplete { get; private set; }

        public async void DoWork()
        {
            IsComplete = false;
            Console.WriteLine("Bearbeitung gestartet");
            await LongOperation();
            Console.WriteLine("Bearbeitung abgeschlossen");
            IsComplete = true;
        }

        public Task LongOperation()
        {
          return  Task.Factory.StartNew(() =>
                                   {
                                       Console.WriteLine("In Bearbeitung");
                                       Thread.Sleep(8000);
                                   }
                                 );

        }
    }
}
