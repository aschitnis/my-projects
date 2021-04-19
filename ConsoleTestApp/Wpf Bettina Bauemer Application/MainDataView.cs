using schnittstelle.mysql.db.baeumer.services.DatabaseViewModels;
using schnittstelle.mysql.db.baeumer.services.EventSubscribers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Bettina_Bauemer_Application.view.root
{
    public class MainDataView : INotifyPropertyChanged
    {
        #region properties

        private ContainerViewModel maindatacontainer;
        public ContainerViewModel MaindataContainer
        {
            get { return maindatacontainer; }
            set { maindatacontainer = value; RaisePropertyChanged(); }
        }
        #endregion

        #region constructors
        public MainDataView()
        {
            Initialize();
        }
        #endregion

        #region methods
        private void Initialize()
        {
            MaindataContainer = new ContainerViewModel();
            
            MaindataContainer.FillContainerIncomingData();
            MaindataContainer.FillContainerCustomerData();
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
