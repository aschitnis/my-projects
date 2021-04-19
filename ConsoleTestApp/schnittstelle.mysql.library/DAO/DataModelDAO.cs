using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using schnittstelle.mysql.library.database.connection;

namespace schnittstelle.mysql.library.DAO
{
    /// <summary>
    /// Database Access Layer (DAO) builds the query based on received parameters 
    /// from the Business Logic Layer and passes it the dbConnection class for execution. 
    /// And simple return results from the dbConnection class to Business Logic Layer.
    /// </summary>
    /// 
    
    public class DataModelDAO
    {
        public enum Tablenames { ORDERS, CURRENCIES, BOOKS, ORDERSTATUS, DELIVERYDETAILS, SELLERS, BOOKSELLERS };

        #region properties
        public DbConnection dbConnection { get; set; }
        #endregion

        #region constructor
        public DataModelDAO()
        {
            dbConnection = new DbConnection();
        }
        #endregion

        #region Methods
        public DataTable SelectQuery(Tablenames enumtable, string[] parameters = null)
        {
            // ORDERS, CURRENCIES, BOOKS, ORDERSTATUS, DELIVERYDETAILS, SELLERS
            DataTable dt = null;
            string sql = null;

            if (dbConnection.IsConnectionOpen)
            {
                int i = (int)enumtable;
                switch(i)
                {
                    case 0:
                        sql = null;
                        break;
                    case 1:
                        sql = null;
                        break;
                    case 2:
                        sql = "SELECT book_id,title,description,author FROM Books";

                        break;
                    case 3:
                        sql = null;
                        break;
                    case 4:
                        sql = null;
                        break;
                    case 5:
                        sql = null;
                        break;
                }
                dt = dbConnection.executeSelectQuery(sql);
            }
            return dt;
        }
        #endregion
    }
}
