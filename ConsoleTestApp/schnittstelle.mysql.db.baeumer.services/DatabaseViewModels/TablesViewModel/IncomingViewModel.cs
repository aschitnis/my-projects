using schnittstelle.mysql.db.baeumer.services.DAO;
using schnittstelle.mysql.db.baeumer.services.DatabaseModels.TablesModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.DatabaseViewModels.TablesViewModel
{
     public class IncomingViewModel : INotifyPropertyChanged
    {
        #region Constructors
        public IncomingViewModel()
        {
            IncomingModelObject = new IncomingModel();
            OutgoingModels      = new List<OutgoingModel>();
            ChargeModels        = new List<ChargesModel>();
            Customer            = new CustomerModel();
            ClosingModels       = new List<ClosingModel>();
        }
        #endregion

        #region Properties

        private List<ClosingModel> closingModels;

        public List<ClosingModel> ClosingModels
        {
            get { return closingModels; }
            set { closingModels = value; RaisePropertyChanged(); }
        }

        private IncomingModel incomingmodelobject;

        public IncomingModel IncomingModelObject
        {
            get { return incomingmodelobject; }
            set { incomingmodelobject = value; RaisePropertyChanged(); }
        }

        private List<OutgoingModel> outgoingmodels;

        public List<OutgoingModel> OutgoingModels
        {
            get { return outgoingmodels; }
            set { outgoingmodels = value; RaisePropertyChanged(); }
        }

        private List<ChargesModel> chargemodels;

        public List<ChargesModel> ChargeModels
        {
            get { return chargemodels; }
            set { chargemodels = value; RaisePropertyChanged(); }
        }

        private CustomerModel customer;

        public CustomerModel Customer
        {
            get { return customer; }
            set { customer = value; RaisePropertyChanged(); }
        }
        #endregion

        #region Methods
        public void FillIncomingModel(int incomingID, DateTime indate, string narration, double deposit, double bal, int custID)
        {
            IncomingModelObject.IncomingID      = incomingID;
            IncomingModelObject.IncomingDate    = indate;
            IncomingModelObject.Narration       = narration;
            IncomingModelObject.DepositAmount   = deposit;
            IncomingModelObject.Balance         = bal;
            IncomingModelObject.CustomerID      = custID;
        }

        public void FillCustomerModel(int cusID, string name, string account, string description)
        {
            Customer.CustomerID = cusID;
            Customer.CustomerName = name;
            Customer.CustomerAccount = account;
            Customer.Description = description;
        }
        
        public void AddClosingModel(int closingID, int customerID, DateTime closingdate, decimal closingbalance, string description)
        {
            ClosingModels.Add(new ClosingModel
            {
                 ClosingID      = closingID,
                 CustomerID     = customerID,
                 ClosingBalance = closingbalance,
                 ClosingDate    = closingdate,
                 Description    = description
            });
        }

        public void AddOutgoingModel(int outgoingID, int incomingID, DateTime outdate, string narration, decimal withdrawamt)
        {
            OutgoingModels.Add(new OutgoingModel
            {
                OutgoingID = outgoingID,
                IncomingID = incomingID,
                OutgoingDate = outdate,
                Narration = narration,
                WithdrawalAmount = withdrawamt
            });
        }

        public void AddToListChargeModel(int chargeID, int incomingID, DateTime chargedate, string narration, decimal chargeamt, string comments)
        {
            ChargeModels.Add(new ChargesModel
            {
                 ChargeID = chargeID,
                 IncomingID = incomingID,
                 ChargeDate = chargedate,
                 Narration  = narration,
                 Charge     = chargeamt,
                 Comments   = comments
            });
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
