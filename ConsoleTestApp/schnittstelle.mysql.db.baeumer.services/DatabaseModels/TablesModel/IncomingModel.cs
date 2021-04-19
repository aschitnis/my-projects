using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.DatabaseModels.TablesModel
{
    public class IncomingModel : INotifyPropertyChanged
    {
        private int incoming_id;

        public int IncomingID
        {
            get { return incoming_id; }
            set { incoming_id = value; RaisePropertyChanged(); }
        }

        private DateTime incomingdate;

        public DateTime IncomingDate
        {
            get { return incomingdate; }
            set { incomingdate = value; RaisePropertyChanged(); }
        }

        private string narration;

        public string Narration
        {
            get { return narration; }
            set { narration = value; RaisePropertyChanged(); }
        }

        private double depositamount;

        public double DepositAmount
        {
            get { return depositamount; }
            set { depositamount = value; RaisePropertyChanged(); }
        }

        private double balance;

        public double Balance
        {
            get { return balance; }
            set { balance = value; RaisePropertyChanged(); }
        }

        private int customer_id;

        public int CustomerID
        {
            get { return customer_id; }
            set { customer_id = value; RaisePropertyChanged(); }
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
