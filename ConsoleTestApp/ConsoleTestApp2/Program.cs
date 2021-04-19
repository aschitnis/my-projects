//using EFBikeSalesLib;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTestApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            CThreadEventing obj = new CThreadEventing();
            //var context = new BikeStoresEntities();
            //customers kunde = new customers() { first_name = "Elfriede", last_name = "Krbecek", city = "Salzburg Stadt", phone = "0043 650 4065003", state = "Salzburg", street = "Erentrudisstrasse 23", zip_code = "5020", email = "khg_elfi@hotmail.com" };
            //context.customers.Add(kunde);
            //context.SaveChanges();

            Task task = Task.Factory.StartNew(() =>
            {
                obj.GetDataFromServer(1);
            });

            Task.Factory.StartNew(() =>
            {
                obj.GetDataFromServer(2);
            });


            //Send first signal to get first set of data from server 1 and server 2
            CThreadEventing.manualResetEvent.Set();
            CThreadEventing.manualResetEvent.Reset();

            Thread.Sleep(TimeSpan.FromSeconds(5));
            //Send second signal to get second set of data from server 1 and server 2
            CThreadEventing.manualResetEvent.Set();

            Console.ReadLine();
        }
    }

    public class CThreadEventing
    {
        public static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        public void GetDataFromServer(int serverNumber)
        {
            //Calling any webservice to get data
            Console.WriteLine("I get first data from server" + serverNumber);
            manualResetEvent.WaitOne();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("I get second data from server" + serverNumber);
            manualResetEvent.WaitOne();
            Console.WriteLine("All the data collected from server" + serverNumber);
        }
    }
}
