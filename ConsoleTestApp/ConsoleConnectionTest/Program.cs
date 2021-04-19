using System;
using System.IO;
using schnittstelle.mysql.library.extensionmethods;
using System.Threading.Tasks;
using schnittstelle.http.rest.services.certificate.properties;
using ConsoleConnectionTest.text.json;
using ConsoleConnectionTest.root;
using System.Data;
using schnittstelle.mysql.db.baeumer.services.DatabaseViewModels.TablesViewModel;
using schnittstelle.mysql.db.baeumer.services.DatabaseViewModels;
using System.Threading;
using ConsoleConnectionTest.asynchronous;

namespace ConsoleConnectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ContainerViewModel contvm = new ContainerViewModel();
            var x = contvm.BusinessViewModelContainer.GetIncomingData();

            Console.ReadKey();


            //string s = @"C:\Users\Mustermann\source\repos\ConsoleTestApp\ConsoleConnectionTest\bin\Debug\schnittstelle.mysql.db.baeumer.services.dll";
            //s = s.Remove(s.IndexOf(@"\bin"));






            /*****************************************
            HttpSocket.PostAsync(@"http://bwt-digitalservices-coding-challenge.azurewebsites.net/api/challenge/certificate",
                                  callbackState =>
                                                  {
                                                      if (callbackState.Exception != null)
                                                          throw callbackState.Exception;
                                                      Console.WriteLine(HttpSocket.GetResponseText(callbackState.ResponseStream));
                                                  },
                                  new System.Collections.Specialized.NameValueCollection()
                                  );
            *****************************************************/
            //var connectionObject =
            //    new MySqlConnection(@"server=127.0.0.1;user=root;password=Elfriede51;database=library");

            //connectionObject.Open();

           // BwtJsonData oj = new BwtJsonData();

            /****************************
            CBwtHttpWebRequestHandler oBwtWebrequest = 
                new CBwtHttpWebRequestHandler(@"http://bwt-digitalservices-coding-challenge.azurewebsites.net/api/challenge/certificate");

            bool isUrlValid = oBwtWebrequest.CheckUrlConnection(); 
            if (isUrlValid == false)
            {
                Console.WriteLine("ERROR");
            }
            *********************************/
           // oBwtWebrequest.PostDataToValidate(@"http://bwt-digitalservices-coding-challenge.azurewebsites.net/api/challenge/certificate");

            //string sFile = @"C:\temp\bwt_cert_PublicKey_RSA.txt";
            //string sNewFile = @"C:\temp\modified_bwt_cert_PublicKey_RSA.txt";

            //string sModulusFile = @"C:\temp\modulus_bwt_cert.txt";
            //string sModulusNewFile = @"C:\temp\modified_modulus_bwt_cert.txt";

            //ReadAndModifyData_WriteToNewFile(sModulusFile, sModulusNewFile);



            //Console.WriteLine(" File read-modify.write completed");

            //LibraryDbConnector obj = new LibraryDbConnector();
            // obj.TestRunAsyncTask();
            // Console.WriteLine("Connection: {0}", obj.ConnectionString);

            // obj.IstVerbindungErfolgreich();
         }

        /// <summary>
        /// Public-Key oder Modulo (in Hex Format) aus vorhandenen Text-Datei lesen. 
        /// Die Leerstellen im String werden gelöscht. 
        /// Der String wird in eine neue (vorhandene) Datei geschrieben.   
        /// </summary>
        /// <param name="sFile">Datei zum Lesen</param>
        /// <param name="sNewFile">Datei zum Schrieben</param>
        public static void ReadAndModifyData_WriteToNewFile(string sFile, string sNewFile)
        {
            Get_ReadFile(sFile)
                       .ContinueWith((t) =>
                       {
                           Task.Run(async () =>
                           {
                               await Write_ModifiedDataToNewFile(sNewFile, t.Result.RemoveWhiteSpaces
                                   
                                   (t.Result));
                               Console.WriteLine(" File write completed");
                           }).Wait();

                       });
        }

        public static async Task<string> Get_ReadFile(string sFile)
        {
            string sData = null;

            await Task.Run(() =>
            {
                using (StreamReader sr = new StreamReader(sFile))
                {
                    string sLine;
                    while ((sLine = sr.ReadLine()) != null)
                    {
                        sData += sLine;
                    }
                }
            });
            return sData;
        }


        public static async Task Write_ModifiedDataToNewFile(string sFile, string sData)
        {
            Console.WriteLine(sFile);
            if(!File.Exists(sFile))
            {
                File.Create(sFile);
            }

            using (StreamWriter sw = new StreamWriter(sFile, false))
            {
                await Task.Run( () =>
                                {
                                    sw.Write(sData);
                                });
            }
        }
    }
}
