using ConsoleTestApp.events.eventconsumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.events
{
    public class CBank
    {
        public event EventHandler<CKontoCheckEventArgs> KontoSaldoCheckEvent;
        public string BankName { get; set; }
        public CKreditInstitutWien InstitutWien { get; set; }
        public CKontoTransaktion Konto { get; set; }
        public CKontoDetails KontoDetails { get; set; }
        public CBank()
        {
            Konto = new CKontoTransaktion();
            KontoDetails = new CKontoDetails();
            InstitutWien = new CKreditInstitutWien();
            KontoSaldoCheckEvent += InstitutWien.KreditLimitUeberschreitungsNachricht;
        }

        public void UeberweisungGutschreiben(int euro)
        {
            Konto.Habenstand += euro;
            Konto.Saldo += Konto.Habenstand;
        }

        public void GeldUeberweisungAbbuchen(int euro)
        {
            Konto.Sollstand += euro;
            Konto.Saldo = Konto.Habenstand - Konto.Sollstand;
            KontoDetails.CurrentSaldo = Konto.Saldo;
            if (Konto.Saldo < 0)
            {
                RaiseKontoSaldoCheckEvent(KontoDetails);
            }
        }

        protected virtual void RaiseKontoSaldoCheckEvent(CKontoDetails details)
        {
            EventHandler<CKontoCheckEventArgs> eventkontoSaldoCheck = KontoSaldoCheckEvent;
            /*
             * If no one has subscribed to this event(delegate), it is NULL.
             */
            if (eventkontoSaldoCheck != null)
            {
                try
                {
                    /* invokes all the handlers that are subscribed to the event. */
                    eventkontoSaldoCheck(this, new CKontoCheckEventArgs(details));
                }
                catch(Exception e)
                {
                    Console.WriteLine($"{e.Message} - {e.InnerException.Message}");
                }
            }
        }
    }
 }
