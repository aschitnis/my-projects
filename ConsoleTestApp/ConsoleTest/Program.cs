using ConsoleTest.CustomTask;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ConsoleTest.Extensions;
using EFBikeSalesLib;
using System.Data.Entity;
using ConsoleTest.Temp;
using My.Encryption.Encryptor;
using System.Threading.Tasks;
using System.Threading;
using My.Sales.Repositories;

namespace ConsoleTest
{

    class Program
    {
        public static Task<bool> GetTaskForEncryptor(DataEncrypter enc)
        {
            bool b = false;
            return Task.Run(() => 
            { 
                b = enc.EncryptString(EncryptionType.HashWithSalt, "Abhijit");
                Thread.Sleep(7000);
                return true;
            });
        }
        public static async void RunTaskForEncryptor(DataEncrypter enc)
        {
            Task<bool> t = GetTaskForEncryptor(enc);
            await t;

            if (t.Result)
            { 
                bool valid = enc.ValidatePassword(enc.Password, enc.HashedData);
                if (valid)
                { 
                    Console.WriteLine("Password has been validated");
                }
                else
                {
                    Console.WriteLine("validation failed !!!");
                }
            }
        }

        static void Main(string[] args)
        {
            SalesRepository salesRepository = new SalesRepository();
            salesRepository.GetAllSales();
            MainViewModel vm = new MainViewModel();
            vm.Display();

            BikeStoreDBOperations bikeStoreDBOperations = new BikeStoreDBOperations();
            DbSet<customer> kunden = BikeStoreDBOperations.GetAllCustomers();
            IQueryable<customer> result =  from k in kunden
                                           from ord in k.orders
                                             where ord.shipped_date == null 
                                                   //ord.customer_id != null
                                             orderby k.customer_id ascending
                                             select k;

            foreach( customer c in result)
            {
                Console.WriteLine($"{c.customer_id}--{c.first_name}  -- {c.last_name} -- {c.orders?.FirstOrDefault().order_id}");
            }
            Console.Read();
            AngestellteModel hiredperson = new AngestellteModel() { Name = null };
            LeiharbeiterModel leasedperson = new LeiharbeiterModel() { Name = "Arnold Angerer" };
            Console.WriteLine($"{hiredperson.GetWorkersModelCategory()}");

            string message = "testing exception";
            message = string.IsNullOrEmpty(hiredperson.exception.Message) ? message : hiredperson.exception.Message;
            Console.WriteLine($"{message}");
            Console.ReadLine();
            return;

            string Teststring = "Our teacher tulsi Saheb";
            string sPattern =  "sahe(\\s)?";
            
            if (System.Text.RegularExpressions.Regex.IsMatch(Teststring, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                Console.WriteLine("....Found !!!");
            }

            ApiManager manager = new ApiManager();
            manager.CallWebserviceAsync();

            string sPermissionValue = null;
            CRestriction.CheckPermission(8, out sPermissionValue);
            Console.WriteLine($"Permission level value: {sPermissionValue}");

            Console.WriteLine("{0}", (int)CRestriction.ETemp.Open);
            Console.WriteLine("{0}", CRestriction.FormatToDate("07/27/2012"));

            PersonalManagement oPersManagement = new PersonalManagement();
            oPersManagement.PersonalListe = oPersManagement.Users.Select(x => new PersonalInfo() { VorName = x.FirstName, NachName = x.LastName }).ToList<PersonalInfo>();

            StringComparison[] comparisons = (StringComparison[])Enum.GetValues(typeof(StringComparison));


            PersonalInfo searchedPerson = oPersManagement.Users.Where(x => x.FirstName.StartsWith("A", StringComparison.CurrentCultureIgnoreCase))?
                                                .Select(x => new PersonalInfo() { VorName = x.FirstName, NachName = x.LastName })
                                                .FirstOrDefault( x => x.VorName.StartsWith("Ar",StringComparison.CurrentCultureIgnoreCase) ) ?? new PersonalInfo() { VorName="n.v", NachName="n.v" };
            Console.ReadLine();
        }
    }

    public class CRestriction
    {
        public static DateTime FormatToDate(string sdate)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Parse(sdate,CultureInfo.GetCultureInfo("en"));
            return dt;
        }
        private static HashSet<string> hsPermissionAttributeNames = new HashSet<string>(new string[] { "None:", "ReadLevel0:folder-Read first level",
                                                                                               "ReadLevel1:folder-Read second level","ReadLevelAll:folder-Read all","Write:file-In-folder Write","WriteLevel0:ReadLevel0 | Write | 1<<4",
                                                                                               "WriteLevel1:ReadLevel1 | Write"});
        public enum ETemp
        {
            Open = 1<<0,
            Close = 2<<2
        }

        [Flags]
        public enum PermissionAttributes
        {
            None = 0,
            ReadLevel0 = 1<<0,
            ReadLevel1 = 1<<1,
            ReadLevelAll = 1<<2,
            Write = 1<<3,
            WriteLevel0 = ReadLevel0 | Write | 1<<4,
            WriteLevel1 = ReadLevel1 | Write,
        }


        public static void CheckPermission(int ipermission, out string spermission)
        {
            spermission = "n.v."; 
            if (Enum.IsDefined(typeof(PermissionAttributes),ipermission))
            {
                PermissionAttributes epermissionlevel = (PermissionAttributes)ipermission;
                spermission = hsPermissionAttributeNames.Where(p => p.StartsWith(epermissionlevel.ToString())).FirstOrDefault();
                spermission = spermission.Substring( spermission.IndexOf(":")+1, (spermission.Count() - spermission.IndexOf(":")-1) );
            }
            else
            {
                Console.WriteLine("{0} - is not defined", ipermission);
            }
        }
    }
}
