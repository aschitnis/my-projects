using schnittstelle.mysql.db.baeumer.services.DatabaseModels.TablesModel;
using schnittstelle.mysql.db.baeumer.services.DatabaseViewModels.TablesViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.DatabaseViewModels
{
    public class DataManipulationViewModel : INotifyPropertyChanged
    {
        public DataManipulationViewModel()
        {
            NewIncomingItem = new IncomingViewModel();
            NewOutgoingItem = new OutgoingModel();
        }


        private OutgoingModel _newoutgoingmodel;
        public OutgoingModel NewOutgoingItem
        {
            get { return _newoutgoingmodel; }
            set { _newoutgoingmodel = value; RaisePropertyChanged(); }
        }

        private IncomingViewModel _newincomingitem;
        public IncomingViewModel NewIncomingItem
        {
            get
            {
                if (_newincomingitem == null)
                    _newincomingitem = new IncomingViewModel();
                return _newincomingitem;
            }
            set { _newincomingitem = value; RaisePropertyChanged(); }
        }

        public void FillOutgoingItem(int id, int incomingid, DateTime outgoingdate, string narration,
                                      decimal withdrawalamount)
        {
            NewOutgoingItem.OutgoingID = id;
            NewOutgoingItem.IncomingID = incomingid;
            NewOutgoingItem.OutgoingDate = outgoingdate;
            NewOutgoingItem.Narration = narration;
            NewOutgoingItem.WithdrawalAmount = withdrawalamount;
        }

        public void AddOutgoingItemToList()
        {
            NewIncomingItem.OutgoingModels.Add(NewOutgoingItem);
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
