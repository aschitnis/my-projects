using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.DatabaseModels.TablesModel
{
    public class OutgoingModel : INotifyPropertyChanged
    {
        private int outgoing_id;

        public int OutgoingID
        {
            get { return outgoing_id; }
            set { outgoing_id = value; RaisePropertyChanged(); }
        }

        private int incoming_id;

        public int IncomingID
        {
            get { return incoming_id; }
            set { incoming_id = value; RaisePropertyChanged(); }
        }

        private DateTime outgoingdate;

        public DateTime OutgoingDate
        {
            get { return outgoingdate; }
            set { outgoingdate = value; RaisePropertyChanged(); }
        }

        private string narration;

        public string Narration
        {
            get { return narration; }
            set { narration = value; RaisePropertyChanged(); }
        }

        private decimal withdrawalamount;

        public decimal WithdrawalAmount
        {
            get { return withdrawalamount; }
            set { withdrawalamount = value; RaisePropertyChanged(); }
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
