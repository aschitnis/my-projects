using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.DatabaseModels.TablesModel
{
    public class ClosingModel : INotifyPropertyChanged
    {
        #region Properties
        private int closing_id;

        public int ClosingID
        {
            get { return closing_id; }
            set { closing_id = value; RaisePropertyChanged(); }
        }

        private DateTime closingdate;

        public DateTime ClosingDate
        {
            get { return closingdate; }
            set { closingdate = value; RaisePropertyChanged(); }
        }

        private decimal closingbalance;

        public decimal ClosingBalance
        {
            get { return closingbalance; }
            set { closingbalance = value; RaisePropertyChanged(); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; RaisePropertyChanged(); }
        }

        private int customer_id;

        public int CustomerID
        {
            get { return customer_id; }
            set { customer_id = value; RaisePropertyChanged(); }
        }
        #endregion

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
