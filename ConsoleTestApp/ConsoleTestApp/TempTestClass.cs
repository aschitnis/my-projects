using ConsoleTestApp.sequentialReads;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    public interface ICustomer
    {
       int sekunden { get; set; }
    }

    public class TempTestClass
    {
        public async Task LesenNVEStatusDateien(CBaseHUBSTATStatus oBase)
        {
            Task<CBaseHUBSTATStatus> t = Task.Run<CBaseHUBSTATStatus>(() =>
            {
                foreach (string filename in Directory.EnumerateFiles(oBase.Verzeichnis))
                {
                    CHUBSTATStatus oColli = new CHUBSTATStatus();
                    Debug.WriteLine("*******************  " + filename);
                    LesenNVEStatusDatei(filename, oColli);
                    oBase.ListHUBSTATStatus.Add(oColli);
                }
                return oBase;
            });
            await t;
            //CBaseHUBSTATStatus oBase = new CBaseHUBSTATStatus();

            //foreach ( string filename in Directory.EnumerateFiles(verzeichnis) )
            //{
            //    CHUBSTATStatus oColli = new CHUBSTATStatus();
            //    await LesenNVEStatusDatei(filename, oColli);
            //    oBase.ListHUBSTATStatus.Add(oColli);
            //}
        }

        public void LesenNVEStatusDatei(string fileName, CHUBSTATStatus oColli)
        {
            const Int32 BufferSize = 1024;
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.StartsWith("H"))
                    {
                        oColli.Kopfsatz = line;
                    }
                    else
                    {
                        if ( line.Substring(97, 5).Trim().StartsWith("D") )
                        {
                            CNVESatz oNve = new CNVESatz();
                            oNve.Empfangsdepot = line.Substring(47, 4);
                            oNve.NVE = line.Substring(7, 35).Trim();
                            oNve.Schluessel = line.Substring(0, 4).Trim();
                            oNve.DatumUhrzeit = line.Substring(52, 12).Trim();
                            oColli.ListNveSatz.Add(oNve);
                        }
                    }
                }
            }
        }

        public async Task LesenNVEStatusDateiAsync(string fileName, CHUBSTATStatus oColli)
        {
            Task<CHUBSTATStatus> t = Task.Run<CHUBSTATStatus>(() =>
            {
                const Int32 BufferSize = 1024;
                using (var fileStream = File.OpenRead(fileName))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.StartsWith("H"))
                        {
                            oColli.Kopfsatz = line;
                        }
                        else 
                        {
                            if ( line.Substring(97,5).Trim().StartsWith("D") ||
                                 line.Substring(97, 5).Trim().StartsWith("U") )
                            {
                                CNVESatz oNve = new CNVESatz();
                                oNve.Empfangsdepot = line.Substring(47, 4);
                                oNve.NVE = line.Substring(7, 35).Trim();
                                oNve.Schluessel = line.Substring(0, 4).Trim();
                                oNve.DatumUhrzeit = line.Substring(52, 12).Trim();
                                oColli.ListNveSatz.Add(oNve);
                            }
                        }
                    }
                }
                return oColli;
            }  );
            await t;
        }
        public async Task PrintobjectData<T>(T o)
        {
            
            Task<int> t = Task.Run<int>(() =>
              {
                  int Millisecondspassed = 0;
                  int msBefore = 0;
                  int msAfter = 0;

                  if (o is RootCustomer)
                  {
                      RootCustomer rc = o as RootCustomer;
                      msBefore = DateTime.Now.Millisecond;
                      Thread.Sleep(999);
                      Console.WriteLine("Root Customer: {0} -- {1}", rc.PersonalId, rc.Name);
                      rc.sekunden = DateTime.Now.Second;
                      msAfter = DateTime.Now.Millisecond;
                  }
                  else if (o is Customer)
                  {
                      Customer c = o as Customer;
                      msBefore = DateTime.Now.Millisecond;
                      Thread.Sleep(999);
                      Console.WriteLine("Customer: {0} -- {1}", c.PersonalId, c.Name);
                      c.sekunden = DateTime.Now.Second;
                      msAfter = DateTime.Now.Millisecond;
                      c.sekunden = DateTime.Now.Second;
                  }
                  Millisecondspassed = msAfter - msBefore;
                  return Millisecondspassed;
              });

           await t;
           await t.ContinueWith( (x) =>
            {
                if (o is RootCustomer)
                {
                    RootCustomer rc = o as RootCustomer;
                    Console.WriteLine("RootCustomer / Milliseconds Break = {0} ---  {1}", x.Result, rc.TickCounter.ToString());
                }
                else if (o is Customer)
                {
                    Customer c = o as Customer;
                    Console.WriteLine("Customer / Milliseconds Break = {0} ---  {1}", x.Result, c.TickCounter.ToString());
                }
            } );
        }
        
    }

    public class Customer : ICustomer
    {
        private long _tickCounter;
        public long TickCounter
        {
            get { _tickCounter = DateTime.Now.Ticks; return _tickCounter; }
            set { _tickCounter = value; }
        }
        public string Name { get; set; }
        public int PersonalId { get; set; }

        public int sekunden { get; set; }

        public Customer()
        {
            TickCounter = DateTime.Now.Ticks;
        }
    }

    public class RootCustomer : Customer
    {

    }
}
