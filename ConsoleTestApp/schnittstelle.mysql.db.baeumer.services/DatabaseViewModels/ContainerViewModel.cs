using schnittstelle.mysql.db.baeumer.services.DatabaseViewModels.TablesViewModel;
using schnittstelle.mysql.db.baeumer.services.EventSubscribers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.DatabaseViewModels
{
    public class ContainerViewModel : INotifyPropertyChanged
    {
        public ContainerViewModel()
        {
            Init();
        }

        private MyCommand _connectiondisposecommand;

        public MyCommand ConnectionDisposeCommand
        {
            get { return _connectiondisposecommand; }
            set { _connectiondisposecommand = value; }
        }

        #region Methods
        public void Init()
        {
            IncomingContainer           = new ObservableCollection<IncomingViewModel>();
            BusinessViewModelContainer  = new DatabaseBusinessViewModel();
            SqlMessenger                = SingletonSqlMessengerService.GetInstance();
            // SingletonSqlMessengerService.GetInstance();
            // changed to Method 12072020 SingletonSqlMessengerService.Instance;
            CustomerViewModelContainer = new CustomerViewModel();
            DataManipulationObject      = new DataManipulationViewModel();

            ConnectionDisposeCommand    = new MyCommand(DisposeDatabaseConnectionObject, 
                                                        CanDisposeDatabaseConnection); 
        }

        private void DisposeDatabaseConnectionObject()
        {
            BusinessViewModelContainer.DataAccessObject.DatabaseConnector.Dispose();
        }

        private bool CanDisposeDatabaseConnection()
        {
            if (BusinessViewModelContainer.DataAccessObject.DatabaseConnector.IsDatabaseConnected)
                return true;
            else return false;
        }
        #endregion

        private SingletonSqlMessengerService sqlmessenger;
        public SingletonSqlMessengerService SqlMessenger
        {
            get
            {
                if (sqlmessenger.SqlMsgDataInstance.ConnectionSuccess == true)
                {
                    OnFetchDataFromDatabaseAtApplicationStartUp();
                }
                return sqlmessenger;
            }
            set { sqlmessenger = value;  }
        }

        private CustomerViewModel customerobjectcontainer;
        public CustomerViewModel CustomerViewModelContainer
        {
            get { return customerobjectcontainer; }
            set { customerobjectcontainer = value; RaisePropertyChanged(); }
        }

        private DataManipulationViewModel datamanipulationobject;

        public DataManipulationViewModel DataManipulationObject
        {
            get { return datamanipulationobject; }
            set { datamanipulationobject = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<IncomingViewModel> _incomingcontainer;

        public ObservableCollection<IncomingViewModel> IncomingContainer
        {
            get { return _incomingcontainer; }
            set { _incomingcontainer = value; RaisePropertyChanged(); }
        }


        private IncomingViewModel _selectedincomingitem;

        public IncomingViewModel SelectedIncomingItem
        {
            get { return _selectedincomingitem; }
            set { _selectedincomingitem = value; RaisePropertyChanged(); }
        }

        private DatabaseBusinessViewModel _businessviewmodelcontainer;
        public DatabaseBusinessViewModel BusinessViewModelContainer
        {
            get { return _businessviewmodelcontainer; }
            set { _businessviewmodelcontainer = value; RaisePropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        #region Container methods
        private void OnFetchDataFromDatabaseAtApplicationStartUp()
        {
            if (IncomingContainer.Count == 0)
            {
                IncomingContainer = BusinessViewModelContainer.GetIncomingData();
            }
        }

        public void FillContainerCustomerData()
        {
            CustomerViewModelContainer = BusinessViewModelContainer.GetCustomers();
        }

        public void FillContainerIncomingData()
        {
            IncomingContainer = BusinessViewModelContainer.GetIncomingData();
        }
        #endregion
    }
}
