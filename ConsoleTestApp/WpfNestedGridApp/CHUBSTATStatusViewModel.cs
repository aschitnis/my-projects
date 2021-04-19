using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfNestedGridApp.Commands;
using WpfNestedGridApp.datensatz;
using WpfNestedGridApp.Model;
using WpfNestedGridApp.singleton.datacontainer;

namespace WpfNestedGridApp.VModel
{
    public class CHUBSTATStatusViewModel :  INotifyPropertyChanged
    {
        public CHUBSTATStatusViewModel()
        {
            Init();
        }

        private string versandpartnerchangeviewmodel;

        public string VersandPartnerChangeViewModel
        {
            get { return versandpartnerchangeviewmodel; }
            set { versandpartnerchangeviewmodel = value; }
        }

        private string dateiengelesenMeldung;

        public string DateienGelesenMeldung
        {
            get { return dateiengelesenMeldung; }
            set { dateiengelesenMeldung = value; OnPropertyChanged("DateienGelesenMeldung"); }
        }

        private string verzeichnis;
        public string Verzeichnis
        {
            get { return verzeichnis; }
            set { verzeichnis = value; OnPropertyChanged("Verzeichnis"); } 
        }
        
        private List<CHUBSTATStatusModel> ocHUBSTATStatus;
        public List<CHUBSTATStatusModel> OcHUBSTATStatus
        {
            get { return ocHUBSTATStatus; }
            set { ocHUBSTATStatus = value; OnPropertyChanged("OcHUBSTATStatus"); }//OnPropertyChanged(nameof(CHUBSTATStatusViewModel.OcHUBSTATStatus)); } 
        }

        private CHUBSTATStatusModel selectedHubstatDatei;

        public CHUBSTATStatusModel SelectedHubstatDatei
        {
            get { return selectedHubstatDatei; }
            set {
                    selectedHubstatDatei = value;
                    OnPropertyChanged("SelectedHubstatDatei");
                }
        }

        private CNVESatzModel selectedNVE;

        public CNVESatzModel SelectedNVE
        {
            get { return selectedNVE; }
            set {
                  selectedNVE = value;
                  OnPropertyChanged("SelectedNVE");
            }
        }

        #region Commands
        public ICommand SpeichernColliDataCommand { get; set; }
        public ICommand LesenVersandPartnerCollisCommand { get; set; }
        #endregion

        #region Commands Methods
        public async void ExecuteAsyncNVEReadCommand(object parameter)
        {
            CHUBSTATStatusViewModel oViewModel = parameter as CHUBSTATStatusViewModel;
            oViewModel.OcHUBSTATStatus.Clear();

            Task t = LesenAsyncAlleNVEDateien(oViewModel);
            await t;
            /****************************************************
            await t.ContinueWith((y) =>
                                   {
                                       Stream stream = null;
                                       try
                                       {
                                           File.Delete(@"D:\VisionFlow\BeispielDaten\readme.txt");

                                           stream = new FileStream(@"D:\VisionFlow\BeispielDaten\readme.txt", FileMode.CreateNew);
                                           using (StreamWriter sw = new StreamWriter(stream))
                                           {
                                               foreach (CHUBSTATStatusModel hubmodel in oViewModel.OcHUBSTATStatus)
                                               {
                                                   sw.WriteLine("----- " + hubmodel.Kopfsatz + " ------------------------");
                                                   sw.WriteLine("----- " + hubmodel.HubDateiName + " ------------------------");
                                                   
                                                   foreach (CNVESatzModel nveModel in hubmodel.OcNveSatz)
                                                   {
                                                       sw.WriteLine(nveModel.NVE);
                                                   }
                                               }
                                               stream = null;
                                           }
                                       }
                                       finally
                                       {
                                           stream?.Dispose();
                                       }

                                   });
         ***********************************************************/
        }

        public bool NVELesenCanExecute(object parameter)
        {
            return true;
            //CHUBSTATStatusViewModel oViewModel = parameter as CHUBSTATStatusViewModel;
            //if (!Directory.Exists(oViewModel.Verzeichnis) || oViewModel.OcHUBSTATStatus.Count <= 0)
            //{
            //    return false;
            //}
            //else { return true; }
        }

        private void ExecuteSpeichernChangedNVEDatei(object Parameter)
        {
            File.Move(SelectedHubstatDatei.HubFullFileName, SelectedHubstatDatei.HubFullFileName + ".bak");
            using (FileStream filestream = File.OpenWrite(SelectedHubstatDatei.HubFullFileName))
            {
                using (TextWriter textWriter = new StreamWriter(filestream, Encoding.UTF8))
                {
                    string skopsatz = "H1.01010HUBSTAT   HUBSTAT   3036      " + SelectedHubstatDatei.Versanddepot + "      N1" + SelectedHubstatDatei.Erstelldatum 
                                                                               + "                                                                  ";
                    textWriter.WriteLine(skopsatz);
                    if (SelectedHubstatDatei.OcNveSatz.Count > 0)
                    {
                        string s = null;
                        foreach (CNVESatzModel oNve in SelectedHubstatDatei.OcNveSatz)
                        {
                            if (!string.IsNullOrEmpty(oNve.Bemerkung))
                            {
                                s = oNve.Schluessel + "KDN" + oNve.NVE + "               " + SelectedHubstatDatei.Versanddepot + " "
                                                    + oNve.Empfangsdepot + " "
                                                    + oNve.DatumUhrzeit + oNve.FremdNummer + "                      "
                                                    + "3036 " + oNve.Bemerkung + "                            ";
                            }
                            else
                            {
                                s = oNve.Schluessel + "KDN" + oNve.NVE + "               " + SelectedHubstatDatei.Versanddepot + " "
                                                    + oNve.Empfangsdepot + " "
                                                    + oNve.DatumUhrzeit + "                            "
                                                    + "3036" + "                                ";
                            }
                            textWriter.WriteLine(s);
                        }
                    }
                }
            }
        }
        #endregion

        #region Methods
        private void Init()
        {
            ocHUBSTATStatus = new List<CHUBSTATStatusModel>();

             LesenVersandPartnerCollisCommand = new HubStatViewModelCommand(ExecuteAsyncNVEReadCommand, NVELesenCanExecute);

            SpeichernColliDataCommand = new HubStatViewModelCommand(ExecuteSpeichernChangedNVEDatei,
                                                                     (x) => { return true; });
        }
        #endregion

        private async Task<CHUBSTATStatusViewModel> LesenAsyncAlleNVEDateien(CHUBSTATStatusViewModel oViewModel)
        {
            Task<CHUBSTATStatusViewModel> t = Task.Run<CHUBSTATStatusViewModel>(() =>
            {
                foreach (string filename in Directory.EnumerateFiles(oViewModel.Verzeichnis))
                {
                    CHUBSTATStatusModel oColli = new CHUBSTATStatusModel();
                    LesenSingleNVEStatusDatei(filename, oColli);
                    oViewModel.OcHUBSTATStatus.Add(oColli);
                }
                oViewModel.DateienGelesenMeldung = string.Format("Anzahl der Statusdateien: {0}", oViewModel.OcHUBSTATStatus.Count()) ;
                return oViewModel;
            });
            
            await t;

            return t.Result;
        }

        private void LesenSingleNVEStatusDatei(string fileName, CHUBSTATStatusModel oColli)
        {
            CHubstatDatensatzBeschreibung oHubXmlMapping = CHubstatXmlSerialization.Instance.GetDeserializedHubstat();
            int vpStart = Convert.ToInt32(oHubXmlMapping.Hsatz.Vp.start);
            int vpLength = Convert.ToInt32(oHubXmlMapping.Hsatz.Vp.length);
            int erstelldatumStart = Convert.ToInt32(oHubXmlMapping.Hsatz.Erstelldatum.start);
            int erstelldatumLength = Convert.ToInt32(oHubXmlMapping.Hsatz.Erstelldatum.length);
            int nveStart = Convert.ToInt32(oHubXmlMapping.Asatz.Nve.start);
            int nveLength = Convert.ToInt32(oHubXmlMapping.Asatz.Nve.length);
            int epStart = Convert.ToInt32(oHubXmlMapping.Asatz.Ep.start);
            int epLength = Convert.ToInt32(oHubXmlMapping.Asatz.Ep.length);
            int datumStart = Convert.ToInt32(oHubXmlMapping.Asatz.Datum.start);
            int datumLength = Convert.ToInt32(oHubXmlMapping.Asatz.Datum.length);
            int bemerStart = Convert.ToInt32(oHubXmlMapping.Asatz.Bemerkung.start);
            int bemerLength = Convert.ToInt32(oHubXmlMapping.Asatz.Bemerkung.length);
            int keyStart = Convert.ToInt32(oHubXmlMapping.Asatz.Key.start);
            int keyLength = Convert.ToInt32(oHubXmlMapping.Asatz.Key.length);

            const Int32 BufferSize = 1024;
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (line.StartsWith("H"))
                    {
                        oColli.Versanddepot = line.Substring(vpStart, vpLength);

                        oColli.Kopfsatz = line;
                        oColli.HubDateiName = new FileInfo(fileName).Name;
                        oColli.HubFullFileName = fileName;
                        oColli.ModifiedTime = DateTime.Now.ToString("hh.mm.ss.ffffff");
                        // ddmmyyhhmmss
                        oColli.Erstelldatum = line.Substring(erstelldatumStart, erstelldatumLength); 
                    }
                    else
                    {
                        if (line.StartsWith("A"))
                        {
                            CNVESatzModel oNve = new CNVESatzModel();
                            oNve.Empfangsdepot = line.Substring(epStart, epLength);
                            oNve.NVE = line.Substring(nveStart, nveLength).Trim();
                            oNve.DatumUhrzeit = line.Substring(datumStart, datumLength).Trim();     // ddmmyyhhmm
                            oNve.Schluessel = line.Substring(keyStart, keyLength).Trim();
                            oNve.FremdNummer = line.Substring(64, 6).Trim();
                            oNve.Bemerkung = line.Substring(bemerStart, bemerLength).Trim();

                            oColli.OcNveSatz.Add(oNve);
                        }
                    }
                }
            }
        }

        

        #region PropertyChanged Notification
        public event PropertyChangedEventHandler PropertyChanged;
        // public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName) );

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public void VersandDepot_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VersandPartnerChange")
                OnPropertyChanged("VersandPartnerChangeViewModel");
        }
        #endregion
    }
}
