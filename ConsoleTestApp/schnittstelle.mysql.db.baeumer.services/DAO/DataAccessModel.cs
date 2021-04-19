using schnittstelle.mysql.db.baeumer.services.EventSubscribers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.DAO
{
    public class DataAccessModel
    {
        public DbConnector DatabaseConnector { get; set; }
        public SqlMessageServiceSubscriber SqlEventSubscriber { get; set;}

        public DataAccessModel()
        {
            DatabaseConnector = new DbConnector();
            SqlEventSubscriber = new SqlMessageServiceSubscriber();

            // register a handler for the event.
            // Th handler is the OnConnectionException() method in the ExceptionSubscriber class.

            //DatabaseConnector.SqlEvent += SqlEventSubscriber.OnConnectionException;
            //DatabaseConnector.SqlEvent += SqlEventSubscriber.OnSqlExecutionException;

            DatabaseConnector.SqlEvent += SqlEventSubscriber.OnDatabaseMessageHandling;

            ConnectToDb();
        }

        private void ConnectToDb()
        {
           DatabaseConnector.OpenConnection();
        }

        public DataTable GetDataPrimaryKeysIncomingModel()
        {
            if (DatabaseConnector.IsDatabaseConnected)
            {
                string _query = "SELECT incoming_id FROM tblincoming";
                return DatabaseConnector.executeToGetAllPrimaryKeysIncomingData(_query);
            }
            else
            {
                return new DataTable();
            }
        }

        public DataTable GetCustomerAndIncomingDataTable(int incoming_id)
        {
           if(DatabaseConnector.IsDatabaseConnected)
            {
                string _query = "CALL sp_getIncomingAndCustomerData(" + incoming_id +")";

                DatabaseConnector.GetAsyncIncomingAndCustomerData(_query);
            }
            return DatabaseConnector.IncomingTable;
        }

        public DataTable GetClosingDataTable(int customer_id)
        {
            if (DatabaseConnector.IsDatabaseConnected)
            {
                string _query = "CALL sp_getClosingdataForCustomerID(" + customer_id + ")";

                DatabaseConnector.GetAsyncClosingData(_query);
            }
            return DatabaseConnector.ClosingTable;
        }

        public DataTable GetForIncomingModelOutgoingDataTable(int incoming_id)
        {
            if (DatabaseConnector.IsDatabaseConnected)
            {
                string _query = "CALL sp_getOutgoingDataForIncomingID(" + incoming_id + ")";

                DatabaseConnector.GetAsyncOutgoingData(_query);
            }
            return DatabaseConnector.OutgoingTable;
        }

        public DataTable GetForIncomingModelChargesDataTable(int incoming_id)
        {
            if (DatabaseConnector.IsDatabaseConnected)
            {
                string _query = "CALL sp_getChargesdataForIncomingID(" + incoming_id + ")";

                DatabaseConnector.GetAsyncChargesData(_query);
            }
            return DatabaseConnector.ChargesTable;
        }

        public DataTable GetCustomerData()
        {
            if (DatabaseConnector.IsDatabaseConnected)
            {
                string query = "CALL sp_getAllCustomers()";
                DatabaseConnector.GetAsyncAllCustomersData(query);
            }
            return DatabaseConnector.CustomersTable;
        }

        public DataTable GetDebits()
        {
            throw new NotImplementedException();

        }
    }
}
