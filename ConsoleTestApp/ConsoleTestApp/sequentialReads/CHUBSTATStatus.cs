using System.Collections.Generic;

namespace ConsoleTestApp.sequentialReads
{
    public class CBaseHUBSTATStatus
    {
        public string Verzeichnis { get; set; }

        private List<CHUBSTATStatus> listHUBSTATStatus;

        public List<CHUBSTATStatus> ListHUBSTATStatus
        {
            get
            {
                if(listHUBSTATStatus == null)
                {
                    listHUBSTATStatus = new List<CHUBSTATStatus>();
                }
                return listHUBSTATStatus;
            }
            set { listHUBSTATStatus = value; }
        }

    }

    public class CHUBSTATStatus
    {
        public CHUBSTATStatus()
        {

        }
        public string Kopfsatz { get; set; }

        private string versanddepot;
        public string Versanddepot
        {
            get { return versanddepot; }
            set { versanddepot = value; }
        }

        private List<CNVESatz> listNvesatz;
        public List<CNVESatz> ListNveSatz
        {
            get
            {
                if (listNvesatz == null)
                    listNvesatz = new List<CNVESatz>();
                return listNvesatz;
            }
            set { listNvesatz = value; }
        }

    }

    public class CNVESatz
    {
        private string schluessel;
        public string Schluessel
        {
            get { return schluessel; }
            set { schluessel = value; }
        }

        private string nve;
        public string NVE
        {
            get { return nve; }
            set { nve = value; }
        }

        private string empfangsdepot;
        public string Empfangsdepot
        {
            get { return empfangsdepot; }
            set { empfangsdepot = value; }
        }

        private string datumuhrzeit;
        public string DatumUhrzeit
        {
            get { return datumuhrzeit; }
            set { datumuhrzeit = value; }
        }
    }
}
