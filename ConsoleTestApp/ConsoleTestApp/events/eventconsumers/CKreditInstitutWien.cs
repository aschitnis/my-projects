using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.events.eventconsumers
{
    #region Event Listeners / Consumer classes
    public class CKreditInstitutWien
    {
        public string KundeKontoNummer { get; set; } = "000000";
        public CKreditInstitutWien()
        {

        }

        public void KreditLimitUeberschreitungsNachricht(object sender, CKontoCheckEventArgs args)
        {
            try
            {
                Console.WriteLine("----- Kontostand IM MINUS ----");
                Console.WriteLine($"{args.Details.Nummer}-{args.Details.KontoBesitzer}- Saldo:EURO {args.Details.CurrentSaldo}" );
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
    #endregion
}
