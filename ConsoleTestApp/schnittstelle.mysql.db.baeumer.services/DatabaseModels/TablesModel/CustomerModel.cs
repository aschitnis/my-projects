using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.DatabaseModels.TablesModel
{
    public class CustomerModel : INotifyPropertyChanged
    {
        private int customer_id;

        public int CustomerID
        {
            get { return customer_id; }
            set { customer_id = value; RaisePropertyChanged(); }
        }

        private string customername;

        public string CustomerName
        {
            get { return customername; }
            set { customername = value; RaisePropertyChanged(); }
        }

        private string customeraccount;

        public string CustomerAccount
        {
            get { return customeraccount; }
            set { customeraccount = value; RaisePropertyChanged(); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; RaisePropertyChanged(); }
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
