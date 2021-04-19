using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleTestApp.Excel.Bund
{
    public class CDataManager
    {
        public List<CBundesland> ListBundeslaender { get; set; }
        private string fileXml
        {
            get { return @"C:\Users\Mustermann\source\repos\ConsoleTestApp\WpfNestedGridApp\BeispielDaten\XAT.xml"; }
        }

        private string fileExcel
        {
            get { return @"C:\Users\Mustermann\source\repos\ConsoleTestApp\WpfNestedGridApp\BeispielDaten\AT.xls"; }
        }

        public CDataManager()
        {
            ListBundeslaender = new List<CBundesland>();
        }

        #region Functions
        public void ReadAndFillData(string file)
        {
               List<CBundesland> ListBundesland = new List<CBundesland>();
               using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read))
               {
                   IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(fs);
                   using (DataSet dataset = reader.AsDataSet())
                   {
                       int tmpLevel = 0;
                       int cntbundeslaender = 0;
                       int cntbezirke = 0;
                       int cntgemeinde = 0;
                       int xslIndex = 0;

                       foreach (DataRow row in dataset.Tables[0].Rows)
                       {
                           if (xslIndex == 0) { xslIndex++; continue; }

                           int ilevel = int.Parse(row[13].ToString());

                           if (ilevel == 3)  // Bundesland
                           {
                               /**
                                   * abfragen ob das bundesland Wien ist, weil Wien keine Bezirke vorweist
                                   * Anders behandeln
                               * **/
                               if (string.Compare(row[3].ToString(), "Wien") == 0)
                               {
                                   ListBundesland.Add(new CBundesland
                                   {
                                       Name = row[3].ToString(),
                                       ListBezirke = new List<CBezirk>()
                                   });
                                   ListBundesland[cntbundeslaender].ListBezirke.Add(new CBezirk { Name = row[3].ToString() });
                               }
                               else
                               {
                                   ListBundesland.Add(new CBundesland
                                   {
                                       Name = row[3].ToString()
                                   });
                                   ListBundesland[cntbundeslaender].ListBezirke.Clear();
                               }

                               cntbundeslaender++;
                               cntbezirke = 0;
                               cntgemeinde = 0;
                           }
                           else if (ilevel == 5)    // Bezirk
                           {
                               if (string.Compare(row[3].ToString(), "Wien (Stadt)") == 0)
                                   continue;

                               ListBundesland[cntbundeslaender - 1].ListBezirke.Add(new CBezirk
                               {
                                   Name = row[3].ToString(),
                                   ListGemeinde = new List<CGemeinde>()
                               });
                               ListBundesland[cntbundeslaender - 1].ListBezirke[cntbezirke].ListGemeinde.Clear();
                               cntbezirke++;
                               cntgemeinde = 0;
                           }
                           else if (ilevel == 6 || ilevel == 7)   // Gemeinde
                           {
                               /** ****
                                   * speziell Fall - Wien
                                   * ***/
                               if (string.Compare(ListBundesland[cntbundeslaender - 1].Name, "Wien") == 0)
                               {
                                   ListBundesland[cntbundeslaender - 1].ListBezirke[0].ListGemeinde.Add(new CGemeinde
                                   {
                                       Name = row[3].ToString()
                                   });

                                   if (string.Compare(row[3].ToString(), "Wien, Liesing") == 0)
                                       break;
                               }
                               else
                               {
                                   ListBundesland[cntbundeslaender - 1].ListBezirke[cntbezirke - 1].ListGemeinde.Add(new CGemeinde
                                   {
                                       Name = row[3].ToString()
                                   });
                               }
                               cntgemeinde++;
                           }
                           tmpLevel = ilevel;
                           xslIndex++;
                       }
                   }
               }
        }

        public List<double> ConvertToCompatibleGeoPoints(object latitude, object longitude)
        {
            List<double> listLocations = new List<double>();
            
            if ((latitude.ToString() == " ") == true || string.IsNullOrEmpty(latitude.ToString()) == true)
            {
                listLocations.Add(0);
            }
            else
            {

                listLocations.Add(Convert.ToDouble(latitude.ToString().Replace('.', ',')) );
            }

            if ((longitude.ToString() == " ") == true || string.IsNullOrEmpty(longitude.ToString()) == true)
            {
                listLocations.Add(0);
            }
            else
            {
               listLocations.Add(Convert.ToDouble(longitude.ToString().Replace('.', ','))); 
            }
            return listLocations;
        }

        public double ConvertToCompatibleArea(object flaeche)
        {
            double dflaeche = 0;
            if ((flaeche.ToString() == " ") == true || string.IsNullOrEmpty(flaeche.ToString()) == true)
            {
                dflaeche = 0;
            }
            else
            {
                dflaeche = Convert.ToDouble(flaeche.ToString().Replace('.', ','));
            }
            return dflaeche;
        }
        #endregion

        #region Async Functions
        public async Task ExcelToXmlAsync(IProgress<int> progress = null)
        {
            var TaskReadExcel = ReadExcelAsync(progress);

            Console.WriteLine("Excel lesen gestartet......");
            
            this.ListBundeslaender = await TaskReadExcel;
            await 5000;
            Console.WriteLine("Excel data erfolgreich gelesen {0}", ListBundeslaender.Count);


            var TaskWriteXml = WriteExcelToXmlAsync();
            
            Console.WriteLine("...XML Datei schreiben gestartet");
            await 5000;
            await TaskWriteXml.ContinueWith((t) => {  Console.WriteLine("XML data erfolgreich geschrieben"); });
        }

        public Task<List<CBundesland>> ReadExcelAsync(IProgress<int> progress = null)
        {
            return Task.Factory.StartNew(() =>
            {
                List<CBundesland> ListBundesland = new List<CBundesland>();
                using (FileStream fs = File.Open(fileExcel, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(fs);
                    using (DataSet dataset = reader.AsDataSet())
                    {
                        int tmpLevel = 0;
                        int cntbundeslaender = 0;
                        int cntbezirke = 0;
                        int cntgemeinde = 0;
                        int cntort = 0;
                        int xslIndex = 0;
                        int i = 1;

                        foreach (DataRow row in dataset.Tables[0].Rows)
                        {
                            if (xslIndex == 0) { xslIndex++; continue; }

                            int ilevel = int.Parse(row[13].ToString());

                            if (progress != null)
                                progress.Report(i * 10);

                            if (ilevel == 3)  // Bundesland
                            {
                                /**
                                    * abfragen ob das bundesland Wien ist, weil Wien keine Bezirke vorweist
                                    * Anders behandeln
                                * **/
                                if (string.Compare(row[3].ToString(), "Wien") == 0)
                                {
                                    List<double> listLoc = ConvertToCompatibleGeoPoints(row[4], row[5]);

                                    ListBundesland.Add(new CBundesland
                                    {
                                        Name = row[3].ToString(),
                                        Bezeichnung = row[12].ToString(),
                                        Einwohner = string.IsNullOrEmpty(row[9].ToString()) == true ? 0 : int.Parse(row[9].ToString()),
                                        Lat = listLoc[0],
                                        Lon = listLoc[1],
                                        Flaeche = ConvertToCompatibleArea(row[10]),
                                        ListBezirke = new List<CBezirk>()
                                    });

                                    ListBundesland[cntbundeslaender].ListBezirke
                                                                    .Add(new CBezirk { Name = row[3].ToString(), Bezeichnung = row[12].ToString() });
                                }
                                else
                                {
                                    List<double> listLoc = ConvertToCompatibleGeoPoints(row[4], row[5]);
                                    ListBundesland.Add(new CBundesland
                                    {
                                        Name = row[3].ToString(),
                                        Einwohner = string.IsNullOrEmpty(row[9].ToString()) == true ? 0 : int.Parse(row[9].ToString()),
                                        Lat = listLoc[0],
                                        Lon = listLoc[1],
                                        Flaeche = ConvertToCompatibleArea(row[10]),
                                        Bezeichnung = row[12].ToString(),
                                    });
                                    ListBundesland[cntbundeslaender].ListBezirke.Clear();
                                }

                                cntbundeslaender++;
                                cntbezirke = 0;
                                cntgemeinde = 0;
                                cntort = 0;
                            }
                            else if (ilevel == 5)    // Bezirk
                            {
                                if (string.Compare(row[3].ToString(), "Wien (Stadt)", comparisonType: StringComparison.OrdinalIgnoreCase) == 0)
                                    continue;

                                List<double> listLoc = ConvertToCompatibleGeoPoints(row[4], row[5]);
                                ListBundesland[cntbundeslaender - 1].ListBezirke.Add(new CBezirk
                                {
                                    Name = row[3].ToString(),
                                    Bezeichnung = row[12].ToString(),
                                    Einwohner = string.IsNullOrEmpty(row[9].ToString()) == true ? 0 : int.Parse(row[9].ToString()),
                                    Lat = listLoc[0],
                                    Lon = listLoc[1],
                                    Plz = row[7].ToString(),
                                    Flaeche = ConvertToCompatibleArea(row[10]),
                                    ListGemeinde = new List<CGemeinde>()
                                });
                                ListBundesland[cntbundeslaender - 1].ListBezirke[cntbezirke].ListGemeinde.Clear();
                                cntbezirke++;
                                cntgemeinde = 0;
                                cntort = 0;
                            }
                            else if ( ilevel == 6 ||    // Gemeinde
                                      (ilevel == 7 && string.Compare(row[12].ToString(),"Gemeindebezirk", comparisonType: StringComparison.OrdinalIgnoreCase) == 0))   // Gemeindebezirk
                            {

                                /** ****
                                    * speziell Fall - Wien
                                    * ***/
                                if (string.Compare(ListBundesland[cntbundeslaender - 1].Name, "Wien") == 0)
                                {
                                    List<double> listLoc = ConvertToCompatibleGeoPoints(row[4], row[5]);
                                    ListBundesland[cntbundeslaender - 1].ListBezirke[0].ListGemeinde.Add(new CGemeinde
                                    {
                                        Name = row[3].ToString(), Bezeichnung = row[12].ToString(),
                                        Plz = row[7].ToString(),
                                        Einwohner = string.IsNullOrEmpty(row[9].ToString()) == true ? 0 : int.Parse(row[9].ToString()),
                                        Lat = listLoc[0],
                                        Lon = listLoc[1],
                                        Flaeche = ConvertToCompatibleArea(row[10])
                                    });
     


                                    if (string.Compare(row[3].ToString(), "Wien, Liesing", comparisonType: StringComparison.OrdinalIgnoreCase) == 0)
                                        break;
                                }
                                else
                                {
                                    List<double> listLoc = ConvertToCompatibleGeoPoints(row[4], row[5]);
                                    ListBundesland[cntbundeslaender - 1].ListBezirke[cntbezirke - 1].ListGemeinde.Add(new CGemeinde
                                    {
                                        Name = row[3].ToString(),
                                        Bezeichnung = row[12].ToString(),
                                        Plz = row[7].ToString(),
                                        Einwohner = string.IsNullOrEmpty(row[9].ToString()) == true ? 0 : int.Parse(row[9].ToString()),
                                        Lat = listLoc[0],
                                        Lon = listLoc[1]
                                    });
                                }
                                cntgemeinde++;
                                cntort = 0;
                            }
                            else if ( (ilevel == 7 || ilevel == 8) && (String.Compare(row[12].ToString(),"Gemeindebezirk") > 0 ) )     // Ort oder Ortsteil
                            {
                                List<double> listLoc = ConvertToCompatibleGeoPoints(row[4], row[5]);
                                ListBundesland[cntbundeslaender - 1].ListBezirke[cntbezirke - 1].ListGemeinde[cntgemeinde - 1].ListOrte.Add(new COrt
                                {
                                    Name = row[3].ToString(),
                                    Bezeichnung = row[12].ToString(),
                                    Plz = row[7].ToString(),
                                    Hoehe = 0,
                                    Einwohner = string.IsNullOrEmpty(row[9].ToString()) == true ? 0 : int.Parse(row[9].ToString()),
                                    Lat = listLoc[0],
                                    Lon = listLoc[1]
                                });
                                cntort++;
                            }
                            tmpLevel = ilevel;
                            xslIndex++;
                            i++;
                        }  // foreach 
                    }
                }
                return ListBundesland;
            });
        }

        public Task WriteExcelToXmlAsync()
        {
            if (File.Exists(fileXml))
            {
                File.Delete(fileXml);
            }

            Task t = Task.Factory.StartNew(() =>
            {
                
                string tmpBezirkname = "";
                string tmpGemeindename = "";
                string tmpOrtname = "";

                XmlWriter xmlWriter = XmlWriter.Create(fileXml);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("data");

                xmlWriter.WriteStartElement("bundeslaender");
                foreach (CBundesland obund in this.ListBundeslaender)
                {
                    xmlWriter.WriteStartElement("bund");
                    xmlWriter.WriteAttributeString("name", obund.Name);
                    xmlWriter.WriteStartElement("einwohner");
                    xmlWriter.WriteString(obund.Einwohner.ToString());
                    xmlWriter.WriteEndElement();                        // einwohner
                    xmlWriter.WriteStartElement("lat");
                    xmlWriter.WriteString(obund.Lat.ToString().Replace(',', '.'));
                    xmlWriter.WriteEndElement();                        // lat
                    xmlWriter.WriteStartElement("lon");
                    xmlWriter.WriteString(obund.Lon.ToString().Replace(',', '.'));
                    xmlWriter.WriteEndElement();                       // lon

                    xmlWriter.WriteStartElement("bezirke");
                    foreach (CBezirk obezirk in obund.ListBezirke)
                    {
                        if (string.Compare(tmpBezirkname, obezirk.Name) == 0)
                        {
                            tmpBezirkname = obezirk.Name;
                            continue;
                        }

                        xmlWriter.WriteStartElement("bezirk");
                        xmlWriter.WriteAttributeString("name", obezirk.Name);

                        xmlWriter.WriteStartElement("einwohner");
                        xmlWriter.WriteString(obezirk.Einwohner.ToString());
                        xmlWriter.WriteEndElement();                        // einwohner
                        xmlWriter.WriteStartElement("lat");
                        xmlWriter.WriteString(obezirk.Lat.ToString().Replace(',', '.'));
                        xmlWriter.WriteEndElement();                        // lat
                        xmlWriter.WriteStartElement("lon");
                        xmlWriter.WriteString(obezirk.Lon.ToString().Replace(',', '.'));
                        xmlWriter.WriteEndElement();                       // lon
                        xmlWriter.WriteStartElement("plz");
                        xmlWriter.WriteString(obezirk.Plz);
                        xmlWriter.WriteEndElement();                       // Plz

                        xmlWriter.WriteStartElement("gemeinden");
                        foreach (CGemeinde ogemeinde in obezirk.ListGemeinde)
                        {
                            if (string.Compare(tmpGemeindename, ogemeinde.Name) == 0)
                            {
                                tmpGemeindename = ogemeinde.Name;
                                continue;
                            }
                            xmlWriter.WriteStartElement("gemeinde");
                            xmlWriter.WriteAttributeString("name", ogemeinde.Name);
                            xmlWriter.WriteStartElement("einwohner");
                            xmlWriter.WriteString(ogemeinde.Einwohner.ToString());
                            xmlWriter.WriteEndElement();                        // einwohner
                            xmlWriter.WriteStartElement("lat");
                            xmlWriter.WriteString(ogemeinde.Lat.ToString().Replace(',', '.'));
                            xmlWriter.WriteEndElement();                        // lat
                            xmlWriter.WriteStartElement("lon");
                            xmlWriter.WriteString(ogemeinde.Lon.ToString().Replace(',', '.'));
                            xmlWriter.WriteEndElement();                       // lon
                            xmlWriter.WriteStartElement("plz");
                            xmlWriter.WriteString(ogemeinde.Plz);
                            xmlWriter.WriteEndElement();                       // Plz
                            xmlWriter.WriteStartElement("flaeche");
                            xmlWriter.WriteString(ogemeinde.Flaeche.ToString().Replace(',', '.'));
                            xmlWriter.WriteEndElement();                     // Fläche
                            xmlWriter.WriteStartElement("bezeichnung");
                            xmlWriter.WriteString(ogemeinde.Bezeichnung);
                            xmlWriter.WriteEndElement();                    // Bezeichnung

                            xmlWriter.WriteStartElement("orte");
                            foreach (COrt ort in ogemeinde.ListOrte)
                            {
                                if (string.Compare(tmpOrtname, ort.Name) == 0)
                                {
                                    tmpOrtname = ort.Name;
                                    continue;
                                }
                                xmlWriter.WriteStartElement("ort");
                                xmlWriter.WriteAttributeString("name", ort.Name);
                                xmlWriter.WriteStartElement("einwohner");
                                xmlWriter.WriteString(ort.Einwohner.ToString());
                                xmlWriter.WriteEndElement();                   // einwohner
                                xmlWriter.WriteStartElement("lat");
                                xmlWriter.WriteString(ort.Lat.ToString().Replace(',', '.'));
                                xmlWriter.WriteEndElement();                        // lat
                                xmlWriter.WriteStartElement("lon");
                                xmlWriter.WriteString(ort.Lon.ToString().Replace(',', '.'));
                                xmlWriter.WriteEndElement();                       // lon
                                xmlWriter.WriteStartElement("hoehe");
                                xmlWriter.WriteString("0");
                                xmlWriter.WriteEndElement();                       // Höhe
                                xmlWriter.WriteStartElement("plz");
                                xmlWriter.WriteString(ort.Plz);
                                xmlWriter.WriteEndElement();                       // Plz
                                xmlWriter.WriteStartElement("lage");
                                xmlWriter.WriteString("n.v.");
                                xmlWriter.WriteEndElement();                       // Lage
                                xmlWriter.WriteStartElement("verkehr");
                                xmlWriter.WriteString("n.v.");
                                xmlWriter.WriteEndElement();                     // Verkehr

                                xmlWriter.WriteEndElement();                  // ort
                            }

                            xmlWriter.WriteEndElement();                    // orte
                            xmlWriter.WriteEndElement();                    // gemeinde
                            tmpGemeindename = ogemeinde.Name;
                        }

                        xmlWriter.WriteEndElement();                       // gemeinden
                        xmlWriter.WriteEndElement();                     // bezirk
                        tmpBezirkname = obezirk.Name;
                    }

                    xmlWriter.WriteEndElement();                       // bezirke
                    xmlWriter.WriteEndElement();  // bund
                }

                xmlWriter.WriteEndElement();                        // bundeslaender
                xmlWriter.WriteEndElement();    // data
                xmlWriter.WriteEndDocument();

                xmlWriter.Close();
            });
            return t;
        }
        #endregion
    }

    #region Extension Classes
    public static class IntegerExtensions
    {
        public static TaskAwaiter GetAwaiter(this int integer)
        {
            return Task.Delay(integer).GetAwaiter();
        }
    }
    #endregion
}
