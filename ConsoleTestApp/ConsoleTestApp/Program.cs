using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ConsoleTestApp.collectioncomparation;
using ConsoleTestApp.events;
using ConsoleTestApp.Excel.Bund;
using ConsoleTestApp.Generic.Template;
using ConsoleTestApp.temp.classes;
using ConsoleTestApp.Test.Delegates;
using ConsoleTestApp.webservice;
using schnittstelle.http.service.currency;

namespace ConsoleTestApp
{
    class Program
    {
        public static void Main(string[] args)
        {
 
            TimeSpan timeSpent = new TimeSpan(12);
            Duration duration = new Duration(TimeSpan.FromSeconds(30.0));

            //CBank mybank = new CBank();
            //mybank.BankName = "Bankhaus Spängler";
            //mybank.KontoDetails.KontoBesitzer = "Abhijit Chitnis";
            //mybank.KontoDetails.Iban = "AT24 1953 0100 2482";
            //mybank.KontoDetails.Typ = "GIRO";
            //mybank.KontoDetails.Bic = "SPAEAT2S";
            //mybank.KontoDetails.Nummer = "6702533712";
            //mybank.InstitutWien.KundeKontoNummer = mybank.KontoDetails.Nummer;

            MyClass classObject = new MyClass();
            MyTestClass testClassObject = new MyTestClass();
            testClassObject.ChangeAge(ref classObject);
            testClassObject.ChangeAgeByValue(classObject);

            testClassObject.ChangeFirstnameByValue(classObject);
            string strFirstname = "Conrad";
            testClassObject.ChangeFirstnameByValue(strFirstname);

            Console.WriteLine($"{classObject.Age} {classObject.Firstname} {strFirstname}");

            Console.ReadLine();
            return;
            /* every 15 seconds */
            ConsoleTestApp.temp.classes.TaskScheduler.Instance.ScheduleTask(12, 17, 0.166,
                                            () =>
                                            {
                                                
                                                Console.WriteLine("task1: " + DateTime.Now);
                                                //here write the code that you want to schedule
                                            });

            /* every 5 seconds */
            //ConsoleTestApp.temp.classes.TaskScheduler.Instance.ScheduleTask(15, 37, 0.00139,
            //                        () =>
            //                        {
            //                            Console.WriteLine("task2: " + DateTime.Now);
            //                            //here write the code that you want to schedule
            //                        });
            Console.ReadLine();
            return;
            List<CMitarbeiter> employees = new List<CMitarbeiter>();
            employees.Add(new CMitarbeiter() { Firstname = "abhijit", Lastname = "chitnis", Age = 48, Salary = 1900 } );
            employees.Add(new CMitarbeiter() { Firstname = "andreas", Lastname = "krbecek", Age = 47, Salary = 2000 });
            employees.Add(new CMitarbeiter() { Firstname = "arnold", Lastname = "angerer", Age = 48, Salary = 2350 });
            employees.Add(new CMitarbeiter() { Firstname = "york", Lastname = "kaiser", Age = 44, Salary = 2100 });

            foreach(var a in employees)
            {
                Console.WriteLine($"{a.Age} -- {a.Lastname} -- {a.Salary} -- {a.Age}");
            }

            employees.Sort(new CMitarbeiterComparer(MitarbeiterEnum.SalaryAscending));
  
            Console.WriteLine("After sort by Salary");
            foreach (var a in employees)
            {
                Console.WriteLine($"{a.Age} -- {a.Lastname} -- {a.Salary} -- {a.Age}");
            }
            Func<CMitarbeiter, CMitarbeiter, MitarbeiterEnum, int> f1 = (first, second, comparetype) =>
            {
                if (first == null && second == null) return 0;
                if (first == null) return 1;
                if (second == null) return -1;

                int iresult = -1;
                switch (comparetype)
                {
                    case MitarbeiterEnum.SalaryAscending:
                        if (first.Salary == second.Salary)
                            iresult = 0;
                        if (first.Salary > second.Salary)
                            iresult = 1;
                        break;
                    case MitarbeiterEnum.Age:
                        if (first.Age == second.Age)
                            iresult = 0;
                        if (first.Age > second.Age)
                            iresult = 1;
                        break;
                    case MitarbeiterEnum.SalaryDescending:
                        if (second.Salary == first.Salary)
                            iresult = 0;
                        if (second.Salary > first.Salary)
                            iresult = 1;
                        break;
                    default:
                        throw new ArgumentException("unexpected Comparetype");
                }
                return iresult;
            };

            // Console.ReadLine();
            CurrencyConversionVM vmCurrencyObject = new CurrencyConversionVM();
            //vmCurrencyObject.SourceCurrencyValue = 25;
            //vmCurrencyObject.CalculateExchangeRate();
            Console.WriteLine(vmCurrencyObject.WebRequestEventSubscriberVM.Message);

            Console.WriteLine($"{vmCurrencyObject.TargetCurrencyCalculatedValue}");
 
            //if (obj.HasHttpException)
            //{
            //    Console.WriteLine(obj.HttpErrorMessage);
            //}
            //else
            //{
            //    if (obj.JsonParseError)
            //        Console.WriteLine(obj.JsonErrorMessage);
            //    else
            //        Console.WriteLine(obj.ExchangeRate);
            //}

            // obj.TestJsonFileRead();
            //CTmpXmlDeserialization obj = new CTmpXmlDeserialization();
            //obj.MakeDeserialization();
            DocumentManager<IDocument> oDocumentManager = new DocumentManager<IDocument>();
            WeakReference weakReference = new WeakReference(oDocumentManager);

           // oDocumentManager = null;
            GC.Collect();
            if (weakReference.IsAlive)
            {
                DocumentManager<IDocument> doc = weakReference.Target as DocumentManager<IDocument>;
                doc.DisplayAllDocuments();
            }

            // oDocumentManager.DisplayAllDocuments();
            /**
            CDataManager odata = new CDataManager();
            Task.Run( async () =>  await odata.ExcelToXmlAsync( new Progress<int>( (i) => { })) );
            **/
            /****************************************
            TempTestClass o = new TempTestClass();

            CBaseHUBSTATStatus oBase = new CBaseHUBSTATStatus();
            oBase.Verzeichnis = @"C:\Users\abch\Downloads\statusAnDialog";
            Task t = o.LesenNVEStatusDateien(oBase);
            await t;

            foreach(CHUBSTATStatus obj in oBase.ListHUBSTATStatus)
            {
                Console.WriteLine("HEADER : {0}", obj.Kopfsatz);
                foreach(CNVESatz objSscc in obj.ListNveSatz)
                {
                    Console.WriteLine("{0}  -- {1} --{2}", objSscc.NVE, objSscc.Empfangsdepot, objSscc.DatumUhrzeit);
                }
                Console.WriteLine(" -------------------------------------------------------------------------------------- ");
            }
            ***************************************/

            /******
            CHUBSTATStatus oColli = new CHUBSTATStatus();
            Task t = o.LesenNVEStatusDateiAsync(@"C:\Users\abch\Downloads\HUBSTAT09077433", oColli);
            await t;
            
            foreach (CNVESatz objSscc in oColli.ListNveSatz)
            {
                Console.WriteLine("{0}  -- {1} --{2}", objSscc.NVE, objSscc.Empfangsdepot, objSscc.DatumUhrzeit);
            }
            *********/

            // await o.PrintobjectData<RootCustomer>(new RootCustomer { PersonalId = 429, Name = "abhijit" });

            //Console.WriteLine("Vorgang beendet");
            Console.Read();
            //oTemp.PrintobjectData<RootCustomer>(new RootCustomer { Id = 429, Name = "abhijit" });
            //oTemp.PrintobjectData<Customer>(new Customer { Id = 404, Name = "prakash" });

            //PrintingThread pt = new PrintingThread();

            //Thread[] ts = new Thread[10];

            //for(int i=0; i<10; i++)
            //{
            //    ts[i] = new Thread(new ThreadStart(pt.ProcessDamper));
            //}

            //foreach (Thread t in ts) t.Start();
        }
    }
}
