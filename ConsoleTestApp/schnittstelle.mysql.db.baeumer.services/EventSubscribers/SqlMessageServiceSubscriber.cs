using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.EventSubscribers
{
    public class SqlMessageServiceSubscriber
    {
        /* this is the event handler. This Method-signature should confirm to the Delegate in the Publisher class. */
        public void OnConnectionException(object source, SqlMessageEventArgs e)
        {
            SingletonSqlMessengerService Instance = SingletonSqlMessengerService.GetInstance();
            Instance.SetSqlMessageData(e.SqlMessageObject);
            // SingletonSqlMessengerService.Instance.SetSqlMessageData(e.SqlMessageObject);
        }

        public void OnSqlExecutionException(object source, SqlMessageEventArgs e)
        {
            Console.WriteLine("SQL-Error Service: " +e.SqlMessageObject.Filename + " - " + e.SqlMessageObject.Functionname + " - " + e.SqlMessageObject.SqlMessage);
        }

        public void OnDatabaseMessageHandling(object source, SqlMessageEventArgs e)
        {
            SingletonSqlMessengerService Instance = SingletonSqlMessengerService.GetInstance();
            Instance.SetSqlMessageData(e.SqlMessageObject);
            // SingletonSqlMessengerService.Instance.SetSqlMessageData(e.SqlMessageObject);
        }
    }
}
