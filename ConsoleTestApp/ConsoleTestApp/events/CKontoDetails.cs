using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.events
{
    public class CKontoDetails 
    {
        public int CurrentSaldo { get; set; }
        public string KontoBesitzer { get; set; }
        public string Typ { get; set; }
        public string Iban { get; set; } = "000";
        public string Bic { get; set; } = "000";
        public string Nummer { get; set; }
        public CKontoDetails(string besitzer = "Max Mustermann", string kontotyp = "Giro", string kontonummer = "00000", string iban = "9999999", string bic = "99999")
            : base()
        {
            Iban = iban;
            Bic = bic;
            Typ = kontotyp;
            KontoBesitzer = besitzer;
            Nummer = kontonummer;
        }
        public CKontoDetails() : base()
        {

        }
    }
}
