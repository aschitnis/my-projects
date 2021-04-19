using MySql.Data.MySqlClient;
using schnittstelle.mysql.library.singleton.configurations;
using System;
using System.Data;

namespace schnittstelle.mysql.library.database.connection
{
    public class DbConnection
    {
        #region Properties
        private static readonly NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();

        private MySqlConnection dbConnection { get; set; }
        private string connectionString { get; set; }
        public bool IsConnectionOpen { get; set; }
        public string ConnectionErrorMessage { get; set; }
        #endregion

        #region Constructor
        public DbConnection()
        {
            Init();
            
            // OpenConnection();
        }
        #endregion

        #region Events
        private void DbConnection_StateChange(object sender, StateChangeEventArgs e)
        {
            switch (e.CurrentState)
            {
                case ConnectionState.Open:
                    IsConnectionOpen = true;
                    _log.Info(string.Format("{0} - Verbindung geöffnet",DateTime.Now.ToString()));
                    break;
                case ConnectionState.Broken:
                    IsConnectionOpen = false;
                    ConnectionErrorMessage = "Verbindung abgebrochen!!!";
                    break;
                case ConnectionState.Closed:
                    IsConnectionOpen = false;
                    _log.Info(string.Format("{0} - Verbindung ist geschlossen", DateTime.Now.ToString()));
                    break;
                default:
                    IsConnectionOpen = false;
                    break;
            }
        }
        #endregion

        #region Methods
        private void Init()
        {
            connectionString = ConfigurationData.Instance.GetMySqlConnectionString();
            dbConnection = new MySqlConnection(connectionString);
            dbConnection.StateChange += DbConnection_StateChange;
        }

        /// <summary>
        ///  MySql ConnectionObjekt versucht die Verbindung 
        ///  zur Datenbank zu aufzubauen. 
        ///  Die Verbindung bleibt offen.
        /// </summary>
        private void OpenConnection()
        {
            IsConnectionOpen = false;
            dbConnection.Open();
        }
        #endregion

        #region Functions
        public DataTable executeSelectQuery(string _query, string[] sqlParamList = null)
        {
           return executeAllBooksSelectQuery(_query);
        }

        public DataTable executeAllOrdersSelectQuery(string _query, string[] sqlParamList = null)
        {
            throw new NotImplementedException();
        }

        public DataTable executeAllSellersSelectQuery(string _query, MySqlParameterCollection sqlParameter = null)
        {
            throw new NotImplementedException();
        }

        public DataTable executeAllOrderStatusSelectQuery(string _query, MySqlParameterCollection sqlParameter = null)
        {
            throw new NotImplementedException();
        }

        private DataTable CreateBooksTableSchema()
        {
            DataTable bookTable = new DataTable("Books");
            DataColumn bookCol = bookTable.Columns.Add(ConfigurationData.Instance.TablesSchemaDictionary["books"][0],typeof(Int32));
            bookCol.AllowDBNull = false;
            bookCol.Unique = true;

            bookTable.Columns.Add(ConfigurationData.Instance.TablesSchemaDictionary["books"][1], typeof(String));
            bookTable.Columns.Add(ConfigurationData.Instance.TablesSchemaDictionary["books"][2], typeof(String));
            bookTable.Columns.Add(ConfigurationData.Instance.TablesSchemaDictionary["books"][3], typeof(String));
            return bookTable;
        }

        private DataTable executeAllBooksSelectQuery(string _query, string[] sqlParamList = null)
        {
            DataTable bookTable = CreateBooksTableSchema();

            if (IsConnectionOpen == false)
                return bookTable;

            using (MySqlCommand cmd = new MySqlCommand(_query, dbConnection))
            {
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        DataRow bookRow = bookTable.NewRow();
                        bookRow["book_id"] = dataReader.GetInt32("book_id");
                        bookRow["title"]   = dataReader.GetString("title");

                        if (dataReader.IsDBNull(2) == false)
                        {
                            bookRow["description"] = dataReader.GetString("description");
                        }
                        else
                        {
                            bookRow["description"] = string.Empty;
                        }

                        bookRow["author"] = dataReader.GetString("author");
                        bookTable.Rows.Add(bookRow);
                    }
                    dataReader.Close();
                }
            }
            _log.Info(string.Format("- Daten aus Tabelle Books erfolgreich geladen. - {0}", DateTime.Now.ToString()));
            return bookTable;
        }

        public DataTable executeAllCurrenciesSelectQuery(string _query, MySqlParameterCollection sqlParameter = null)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
