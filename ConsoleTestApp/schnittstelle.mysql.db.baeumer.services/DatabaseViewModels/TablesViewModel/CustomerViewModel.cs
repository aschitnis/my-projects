using schnittstelle.mysql.db.baeumer.services.DatabaseModels.TablesModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.DatabaseViewModels.TablesViewModel
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        public CustomerViewModel()
        {
            CustomerModels = new List<CustomerModel>();
        }

        private List<CustomerModel> customermodels;

        public List<CustomerModel> CustomerModels
        {
            get { return customermodels; }
            set { customermodels = value; RaisePropertyChanged(); }
        }

        private CustomerModel selectedcustomer;

        public CustomerModel SelectedCustomer
        {
            get { return selectedcustomer; }
            set { selectedcustomer = value; RaisePropertyChanged(); }
        }

        public void ResetCustomerModelList()
        {
            CustomerModels.Clear();
        }

        public void AddCustomerToList(int cusID, string name, string account, string description)
        {
            CustomerModels.Add(new CustomerModel
            {
              CustomerID = cusID,
              CustomerName = name,
              CustomerAccount = account,
              Description = description
            } );
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
