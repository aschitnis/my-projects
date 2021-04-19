using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;
using System.Runtime.CompilerServices;
using schnittstelle.mysql.db.baeumer.services.EventSubscribers;

namespace schnittstelle.mysql.db.baeumer.services
{
    #region delegates
    /*  
     *  This delegate holds a reference to a method in the Subscriber's class. 
     *  That function will actually be the Event-Handler method in the Subscriber's Class.
    */
    public delegate void SqlEventHandler(object source, SqlMessageEventArgs args);
    #endregion

    public class DbConnector : IDisposable
    {
        /*
         * Define a event based on the delegate.
         */
        public event SqlEventHandler SqlEvent;

        /*
         * raise the SqlEvent type of Event. 
         * This is the Event Publisher method. 
         * It will also notify all the Subscribers when a Sql Event is raised. 
         */
        protected virtual void OnSqlMessageCallback()
        {
            if (SqlEvent != null)
            {
                SqlEvent(this, new SqlMessageEventArgs(sqlMsgObject));
            }
        }

        #region Properties
        private bool isDisposed = false;

        // server=127.0.0.1
        // DGH mysql : server=localhost;user=root;password=elfriede;database=dbbaeumertransactions
        // ThinkPad mysql : server=localhost;user=root;password=Elfriede51;database=dbbaeumertransactions
        private static string DBBAEUMERCONNECTIONSTRING = "server=localhost;user=root;password=Elfriede51;database=dbbaeumertransactions";
        private static int CONNCOUNT = 1;
        private MySqlConnection dbConnection { get; set; }
        public bool IsDatabaseConnected = false;
        
        private System.Timers.Timer connectionTimer = new System.Timers.Timer();

        private bool sqlexception = false;
        private CSqlMessage sqlMsgObject { get; set; }

        public DataTable IncomingTable { get; set; }
        public DataTable ChargesTable { get; set; }
        public DataTable OutgoingTable { get; set; }
        public DataTable ClosingTable { get; set; }
        public DataTable CustomersTable { get; set; }
        #endregion

        #region Constructor
        public DbConnector()
        {
            sqlMsgObject = new CSqlMessage();

            connectionTimer.Interval = 5000;
            connectionTimer.AutoReset = true;
            connectionTimer.Elapsed += OnConnectToDBTimedEvent;

            dbConnection = new MySqlConnection(DBBAEUMERCONNECTIONSTRING);

            dbConnection.StateChange += DbConnection_StateChange;
            dbConnection.Disposed += DbConnection_Disposed;

            InitializeSchemasForTables();
        }

        private void InitializeSchemasForTables()
        {
            IncomingTable = CreateIncomingAndCustomerTableSchema();
            OutgoingTable = CreateOutgoingTableSchema();
            ChargesTable = CreateChargesTableSchema();
            ClosingTable = CreateClosingTableSchema();
            CustomersTable = CreateCustomerTableSchema();
        }
        #endregion

        #region Events
        private void OnConnectToDBTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (IsDatabaseConnected == false)
            {
                CONNCOUNT++;
                OpenConnection();
            }
            else
            {
                CONNCOUNT = 1;
                connectionTimer.Enabled = false;
            }
        }

        private void DbConnection_StateChange(object sender, StateChangeEventArgs e)
        {
            switch (dbConnection.State)
            {
                case ConnectionState.Connecting:

                     IsDatabaseConnected = false;
                    sqlMsgObject.ConnectionSuccess = false;
                    sqlMsgObject.SqlMessage = ".... Verbindung wird hergestellt - " + CONNCOUNT;

                    /* Event is raised. Notify all the subscribers */
                    OnSqlMessageCallback(); 
                    break;
                case ConnectionState.Open:

                    sqlMsgObject.ConnectionSuccess = true;
                    sqlMsgObject.SqlMessage = "Verbunden mit der Datenbank - " + CONNCOUNT;
                    
                    /* Event is raised. Notify all the subscribers */
                    OnSqlMessageCallback();

                    connectionTimer.Enabled = false;
                    IsDatabaseConnected = true;
                    CONNCOUNT = 1;
                    break;
                case ConnectionState.Closed:
                    IsDatabaseConnected = false;

                    if (CONNCOUNT == 1)
                    {
                        connectionTimer.Enabled = true;
                        sqlMsgObject.SqlMessage = "Start connection timer - " + CONNCOUNT;
                        OnSqlMessageCallback();
                    }
                    else
                    {
                        sqlMsgObject.ConnectionSuccess = false;
                        sqlMsgObject.SqlMessage = "Verbindung geschlossen - " + CONNCOUNT;
                        OnSqlMessageCallback();
                    }
                    break;
                case ConnectionState.Broken:
                    IsDatabaseConnected = false;
                    break;
            }
        }

        private void DbConnection_Disposed(object sender, EventArgs e)
        {
            IsDatabaseConnected = false;
            sqlMsgObject.ConnectionSuccess = false;
            sqlMsgObject.SqlMessage = "Database Connection is deleted from Heap-memory";

            /* Event is raised. Notify all the subscribers */
            OnSqlMessageCallback();
        }
        #endregion

        #region Async-Connection Methods
        /// <summary>
        /// 
        /// </summary>
        private async Task<CSqlMessage> ConnectAsnyc()
        {
            sqlexception = false;

            Task<CSqlMessage> t = Task.Run(() =>
            {
                try
                {
                       dbConnection.Open();
                }
                catch (Exception e)
                {
                    sqlexception = true;
                    sqlMsgObject.ConnectionSuccess = false;
                    sqlMsgObject.SqlMessage = "Verbindungsfehler: " + e.Message;
                    sqlMsgObject.Filename = @"DbConnector.cs";
                    sqlMsgObject.Functionname = "ConnectAsnyc";
                }
                return sqlMsgObject;
            });
            await t;
            
            return sqlMsgObject;
        }

        public void OpenConnection()
        {
            if (isDisposed)
            {
                sqlMsgObject.ConnectionSuccess = false;
                sqlMsgObject.Filename = "DbConnector.cs";
                sqlMsgObject.SqlMessage = "Connector Object does not exist!. Please restart the program.";
            }
            else
            {
                sqlMsgObject = Task.Run(ConnectAsnyc).Result;
                if (sqlexception)
                {
                    OnSqlMessageCallback();
                }
            }
        }
        #endregion


        #region SQL Functions
        /* 
         * Calls the stored-procedure : sp_getIncomingAndCustomerData(incomingID int).
         * Gets data from tblIncoming & tblcustomers table for a incoming_id as parameter. 
         */
        public void GetAsyncIncomingAndCustomerData(string _query)
        {
          Task<DataTable> t =  Task.Run<DataTable>( () => 
                {
                    return executeAsyncGetCustomerAndIncomingData(_query);
                } );

        /*
         * Basically Task.GetAwaiter().GetResult() is equivalent to await task ?? maybe !!.
         */
            TaskAwaiter<DataTable> taskAwaiter = t.GetAwaiter();

          if (!sqlexception)
           {
                IncomingTable = taskAwaiter.GetResult();
           }
          else
           {
                /* raise the event in case of a Sql-execution error */
                OnSqlMessageCallback();
           }
        }

        /* 
         * Calls the stored-procedure : sp_getOutgoingDataForIncomingID(incomingID int).
         * Gets data from tblOutgoing table for a incoming_id as parameter. 
         */
        public void GetAsyncOutgoingData(string _query)
        {
            Task<DataTable> t = Task.Run<DataTable>(() =>
                                {
                                    return ExecuteAsyncGetOutgoingData(_query);
                                });
            // Task.WaitAny(t);
            TaskAwaiter<DataTable> taskAwaiter = t.GetAwaiter();

            if (!sqlexception)
            {
                OutgoingTable = taskAwaiter.GetResult();
            }
            else
            {
                /* raise the event in case of a Sql-execution error */
                OnSqlMessageCallback();
            }
        }

        /* 
         * Calls the stored-procedure : sp_getChargesdataForIncomingID(incomingID int).
         * Gets data from tblCharges table for a incoming_id as parameter. 
         */
        public void GetAsyncChargesData(string _query)
        {
            Task<DataTable> t = Task.Run<DataTable>(() =>
                                {
                                    return executeAsyncGetChargesData(_query);
                                });
            // Task.WaitAny(t);
            TaskAwaiter<DataTable> taskAwaiter = t.GetAwaiter();

            if (!sqlexception)
            {
                ChargesTable = taskAwaiter.GetResult();
            }
            else
            {
                /* raise the event in case of a Sql-execution error */
                OnSqlMessageCallback();
            }
         }

        /* Calls the stored-procedure : sp_getClosingdataForCustomerID(incomingID int).
         */
        public void GetAsyncClosingData(string _query)
        {
            Task<DataTable> t = Task.Run<DataTable>(() =>
            {
                return executeAsyncGetClosingData(_query);
            });

            // Task.WaitAny(t);
            TaskAwaiter<DataTable> taskAwaiter = t.GetAwaiter();

            if (!sqlexception)
            {
                ClosingTable = taskAwaiter.GetResult();
            }
            else
            {
                OnSqlMessageCallback();
            }
        }

        public void GetAsyncAllCustomersData(string _query)
        {
            Task<DataTable> t = Task.Run<DataTable>(() =>
            {
                return executeAsyncGetCustomersData(_query);
            });

            TaskAwaiter<DataTable> taskAwaiter = t.GetAwaiter();

            if (!sqlexception)
            {
                CustomersTable = taskAwaiter.GetResult();
            }
            else
            {
                /* raise the event in case of a Sql-execution error */
                OnSqlMessageCallback();
            }
        }

        /*gets all the incoming_id's from the tblIncoming Table.
         
         */
        public DataTable executeToGetAllPrimaryKeysIncomingData(string _query = null)
        {
            DataTable tblPKIncoming = new DataTable("tblPrimayKeysIncoming");
            tblPKIncoming.Columns.Add(new DataColumn { AllowDBNull = false, ColumnName = "incoming_id", DataType = typeof(Int32) });

            if (IsDatabaseConnected == true)
            {
                using (MySqlCommand cmd = new MySqlCommand(_query, dbConnection))
                {
                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            DataRow rwIncoming = tblPKIncoming.NewRow();
                            rwIncoming["incoming_id"] = dataReader.GetInt32("incoming_id");
                            tblPKIncoming.Rows.Add(rwIncoming);
                        }
                    }
                }
            }
            return tblPKIncoming;
        }

        #endregion

        #region Async Sql-methods
        private async Task<DataTable> executeAsyncGetClosingData(string _query = null)
        {
            sqlexception = false;

            Task<DataTable> t = Task.Run(() =>
            {
                if (IsDatabaseConnected == true)
                {
                    ClosingTable.Rows.Clear();
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(_query, dbConnection))
                        {
                            using (MySqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                while (dataReader.Read())
                                {
                                    DataRow rwClosing = ClosingTable.NewRow();
                                    rwClosing["closing_id"] = dataReader.GetInt32("closing_id");
                                    rwClosing["closingdate"] = dataReader.GetDateTime("closingdate");
                                    rwClosing["closingbalance"] = dataReader.GetDouble("closingbalance");
                                    rwClosing["description"] = dataReader.IsDBNull(3) == false ? dataReader.GetString("description") : null;
                                    rwClosing["customer_id"] = dataReader.GetInt32("customer_id");

                                    ClosingTable.Rows.Add(rwClosing);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        sqlexception = true;
                        sqlMsgObject.SqlMessage = "Sql-Executionfehler: " + e.Message;
                        sqlMsgObject.Filename = "DbConnector.cs";
                        sqlMsgObject.Functionname = "async executeAsyncGetClosingData";
                    }
                    finally
                    {
                    }
                }
                return ClosingTable;
            });
            await t;
            return ClosingTable;
        }

        private async Task<DataTable> executeAsyncGetCustomerAndIncomingData(string _query = null)
        {
            sqlexception = false;
            
            Task<DataTable> t = Task.Run(() =>
            {
                if (IsDatabaseConnected == true)
                {
                    IncomingTable.Rows.Clear();
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(_query, dbConnection))
                        {
                            using (MySqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                while (dataReader.Read())
                                {
                                    DataRow rwIncoming = IncomingTable.NewRow();
                                    rwIncoming["incoming_id"] = dataReader.GetInt32("incoming_id");
                                    rwIncoming["incomingdate"] = dataReader.GetDateTime("incomingdate");
                                    rwIncoming["narration"] = dataReader.GetString("narration");
                                    rwIncoming["depositamount"] = dataReader.GetDouble("depositamount");
                                    rwIncoming["balance"] = dataReader.GetDouble("balance");
                                    rwIncoming["customer_id"] = dataReader.GetInt32("customer_id");
                                    rwIncoming["incoming_customer_id"] = dataReader.GetInt32("customer_id");
                                    rwIncoming["customername"] = dataReader.IsDBNull(7) == false ? dataReader.GetString("customername") : null;
                                    rwIncoming["customeraccount"] = dataReader.IsDBNull(8) == false ? dataReader.GetString("customeraccount") : null;
                                    rwIncoming["description"] = dataReader.IsDBNull(9) == false ? dataReader.GetString("description") : null;

                                    IncomingTable.Rows.Add(rwIncoming);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        sqlexception = true;
                        sqlMsgObject.ConnectionSuccess = false;
                        sqlMsgObject.SqlMessage = "Sql-Executionfehler: " + e.Message;
                        sqlMsgObject.Filename = "DbConnector.cs";
                        sqlMsgObject.Functionname = "async executeAsyncGetCustomerAndIncomingData";
                    }
                    finally
                    {
                    }
                }
                return IncomingTable;
            });
            await t;
            return IncomingTable;
        }

        private async Task<DataTable> executeAsyncGetChargesData(string _query = null)
        {
            sqlexception = false;

            Task<DataTable> t = Task.Run(() =>
            {
                if (IsDatabaseConnected == true)
                {
                    ChargesTable.Rows.Clear();
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(_query, dbConnection))
                        {
                            using (MySqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                while (dataReader.Read())
                                {
                                    DataRow rwCharges = ChargesTable.NewRow();

                                    rwCharges["charge_id"] = dataReader.GetInt32("charge_id");
                                    rwCharges["charge_incoming_id"] = dataReader.GetInt32("charge_incoming_id");
                                    rwCharges["chargedate"] = dataReader.IsDBNull(2) == false ? dataReader.GetDateTime("chargedate") : default(DateTime);
                                    rwCharges["charge_narration"] = dataReader.IsDBNull(3) == false ? dataReader.GetString("charge_narration") : null;
                                    rwCharges["charge"] = dataReader.GetDouble("charge");
                                    rwCharges["comments"] = dataReader.IsDBNull(5) == false ? dataReader.GetString("comments") : null;
                                    ChargesTable.Rows.Add(rwCharges);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        sqlexception = true;
                        sqlMsgObject.ConnectionSuccess = false;
                        sqlMsgObject.SqlMessage = "Sql-Executionfehler: " + e.Message;
                        sqlMsgObject.Filename = "DbConnector.cs";
                        sqlMsgObject.Functionname = "async executeAsyncGetChargesData";
                    }
                    finally
                    {
                    }
                }
                return ChargesTable;
            });
            await t;
            return ChargesTable;
        }

        private async Task<DataTable> ExecuteAsyncGetOutgoingData(string _query = null)
        {
            sqlexception = false;

            Task<DataTable> t = Task.Run(() =>
            {
                if (IsDatabaseConnected == true)
                {
                    OutgoingTable.Rows.Clear();
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(_query, dbConnection))
                        {
                            using (MySqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                while (dataReader.Read())
                                {
                                    DataRow rwOutgoing = OutgoingTable.NewRow();
                                    rwOutgoing["outgoing_id"] = dataReader.GetInt32("outgoing_id");
                                    rwOutgoing["out_incoming_id"] = dataReader.GetInt32("incoming_id");
                                    rwOutgoing["outgoingdate"] = dataReader.IsDBNull(2) == false ? dataReader.GetDateTime("outgoingdate") : default(DateTime);
                                    rwOutgoing["outgoing_narration"] = dataReader.IsDBNull(3) == false ? dataReader.GetString("outgoing_narration") : null;
                                    rwOutgoing["withdrawalamount"] = dataReader.GetDouble("withdrawalamount");

                                    OutgoingTable.Rows.Add(rwOutgoing);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        sqlexception = true;
                        sqlMsgObject.ConnectionSuccess = false;
                        sqlMsgObject.SqlMessage = "Sql-Executionfehler: " + e.Message;
                        sqlMsgObject.Filename = "DbConnector.cs";
                        sqlMsgObject.Functionname = "async executeAsyncGetOutgoingData";
                    }
                    finally
                    {
                    }
                }
                return OutgoingTable;
            });
            await t;
            return OutgoingTable;
        }

        private async Task<DataTable> executeAsyncGetCustomersData(string _query = null)
        {
            sqlexception = false;

            Task<DataTable> t = Task.Run(() =>
            {
                if (IsDatabaseConnected == true)
                {
                    CustomersTable.Rows.Clear();
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(_query, dbConnection))
                        {
                            using (MySqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                while (dataReader.Read())
                                {
                                    DataRow rwCustomer = CustomersTable.NewRow();
                                    rwCustomer["customer_id"]       = dataReader.GetInt32("customer_id");
                                    rwCustomer["customername"]      = dataReader.GetString("customername");
                                    rwCustomer["customeraccount"]   = dataReader.IsDBNull(2) == false ? dataReader.GetString("customeraccount") : null;
                                    rwCustomer["description"]       = dataReader.IsDBNull(3) == false ? dataReader.GetString("description") : null;

                                    CustomersTable.Rows.Add(rwCustomer);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        sqlexception = true;
                        sqlMsgObject.ConnectionSuccess = false;
                        sqlMsgObject.SqlMessage = "Sql-Executionfehler: " + e.Message;
                        sqlMsgObject.Filename = "DbConnector.cs";
                        sqlMsgObject.Functionname = "async executeAsyncGetCustomersData";
                    }
                    finally
                    {
                    }
                }
                return CustomersTable;
            });
            await t;
            return CustomersTable;
        }
        #endregion

        #region Methods for Table-Schemas
        /* Die Spaltennamen entsprechen die umbenannte Spaltennamen in der Stored-Prozedure 'sp_getIncomingAndCustomerData' */
        private DataTable CreateIncomingAndCustomerTableSchema()
        {
            DataTable tblincoming = new DataTable("incomingandcustomerschema");

            tblincoming.Columns.Add(new DataColumn { AllowDBNull = false, ColumnName = "incoming_id", DataType = typeof(Int32) });
            tblincoming.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "incomingdate", DataType = typeof(DateTime) });
            tblincoming.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "narration", DataType = typeof(String) });
            tblincoming.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "depositamount", DataType = typeof(double) });
            tblincoming.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "balance", DataType = typeof(double) });
            tblincoming.Columns.Add(new DataColumn { AllowDBNull = true, Unique = false, ColumnName = "incoming_customer_id", DataType = typeof(Int32) });

            tblincoming.Columns.Add(new DataColumn { AllowDBNull = true, Unique = false, ColumnName = "customer_id", DataType = typeof(Int32) });
            tblincoming.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "customername", DataType = typeof(String) });
            tblincoming.Columns.Add(new DataColumn { AllowDBNull = true, Unique = false, ColumnName = "customeraccount", DataType = typeof(String) });
            tblincoming.Columns.Add(new DataColumn { AllowDBNull = true, Unique = false, ColumnName = "description", DataType = typeof(String) });

            return tblincoming;
        }

        /* Die Spaltennamen entsprechen die umbenannte Spaltennamen in der Stored-Prozedure 'sp_getOutgoingDataForIncomingID' */
        private DataTable CreateOutgoingTableSchema()
        {
            DataTable tbloutgoing = new DataTable("outgoingschema");

            tbloutgoing.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "outgoing_id", DataType = typeof(Int32) });
            tbloutgoing.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "out_incoming_id", DataType = typeof(Int32) });
            tbloutgoing.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "outgoingdate", DataType = typeof(DateTime) });
            tbloutgoing.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "outgoing_narration", DataType = typeof(String) });
            tbloutgoing.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "withdrawalamount", DataType = typeof(double) });
            return tbloutgoing;
        }

        private DataTable CreateChargesTableSchema()
        {
            DataTable tblcharges = new DataTable("chargesschema");

            tblcharges.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "charge_id", DataType = typeof(Int32) });
            tblcharges.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "charge_incoming_id", DataType = typeof(Int32) });
            tblcharges.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "chargedate", DataType = typeof(DateTime) });
            tblcharges.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "charge_narration", DataType = typeof(String) });
            tblcharges.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "charge", DataType = typeof(double) });
            tblcharges.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "comments", DataType = typeof(String) });
            return tblcharges;
        }

        private DataTable CreateClosingTableSchema()
        {
            DataTable tblclosing = new DataTable("closingschema");

            tblclosing.Columns.Add(new DataColumn { AllowDBNull = false, ColumnName = "closing_id", DataType = typeof(Int32) });
            tblclosing.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "closingdate", DataType = typeof(DateTime) });
            tblclosing.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "closingbalance", DataType = typeof(double) });
            tblclosing.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "description", DataType = typeof(String) });
            tblclosing.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "customer_id", DataType = typeof(Int32) });
            return tblclosing;
        }

        private DataTable CreateCustomerTableSchema()
        {
            DataTable tblcustomer = new DataTable("customerschema");

            tblcustomer.Columns.Add(new DataColumn { AllowDBNull = false, ColumnName = "customer_id", DataType = typeof(Int32) });
            tblcustomer.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "customername", DataType = typeof(String) });
            tblcustomer.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "customeraccount", DataType = typeof(String) });
            tblcustomer.Columns.Add(new DataColumn { AllowDBNull = true, ColumnName = "description", DataType = typeof(String) });
            return tblcustomer;
        }
        #endregion

        #region Dispose Methods

        public void Dispose()
        {
            Dispose(true);

            /* GC.SuppressFinalize(this) :
             * Objects that implement the IDisposable interface can call this method 
             * from the IDisposable.Dispose method to prevent the garbage collector 
             * from calling Object.Finalize on an object that does not require it.
             */
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // clean up managed objects by calling 
                    // their Dispose() method.
                    dbConnection.Dispose();
                    IsDatabaseConnected = false;
                }
                // clean up unmanaged objects here
            }
            isDisposed = true;
        }
        #endregion

        ~DbConnector()
        {
            Dispose(false);
        }
    }
}
