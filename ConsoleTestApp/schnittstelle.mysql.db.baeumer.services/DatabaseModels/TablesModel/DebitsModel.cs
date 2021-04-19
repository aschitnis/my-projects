using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.DatabaseModels.TablesModel
{
    public class DebitsModel : INotifyPropertyChanged
    {
        private int debit_id;

        public int DebitID
        {
            get { return debit_id; }
            set { debit_id = value; RaisePropertyChanged(); }
        }

        private int debtor_id;

        public int DebtorID
        {
            get { return debtor_id; }
            set { debtor_id = value; RaisePropertyChanged(); }
        }

        private int creditor_id;

        public int CreditorID
        {
            get { return creditor_id; }
            set { creditor_id = value; RaisePropertyChanged(); }
        }

        private decimal debitamount;

        public decimal DebitAmount
        {
            get { return debitamount; }
            set { debitamount = value; RaisePropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
