using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.DatabaseModels.TablesModel
{
    public class ChargesModel : INotifyPropertyChanged
    {
        private int charge_id;
        public int ChargeID
        {
            get { return charge_id; }
            set { charge_id = value; RaisePropertyChanged(); }
        }

        private int incoming_id;
        public int IncomingID
        {
            get { return incoming_id; }
            set { incoming_id = value; RaisePropertyChanged(); }
        }

        private DateTime chargedate;
        public DateTime ChargeDate
        {
            get { return chargedate; }
            set { chargedate = value; RaisePropertyChanged(); }
        }

        private string narration;

        public string Narration
        {
            get { return narration; }
            set { narration = value; RaisePropertyChanged(); }
        }

        private decimal charge;

        public decimal Charge
        {
            get { return charge; }
            set { charge = value; RaisePropertyChanged(); }
        }

        private string comments;

        public string Comments
        {
            get { return comments; }
            set { comments = value; RaisePropertyChanged(); }
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
