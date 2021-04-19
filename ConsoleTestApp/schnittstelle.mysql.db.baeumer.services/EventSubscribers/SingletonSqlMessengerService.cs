using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.EventSubscribers
{
    public class SingletonSqlMessengerService : INotifyPropertyChanged
    {
        private CSqlMessage sqlmsgdatainstance;

        public CSqlMessage SqlMsgDataInstance
        {
            get { return sqlmsgdatainstance; }
            set { sqlmsgdatainstance = value; RaisePropertyChanged(); }
        }

        private SingletonSqlMessengerService()
        {
            SqlMsgDataInstance = new CSqlMessage();
        }

        private static SingletonSqlMessengerService instance = new SingletonSqlMessengerService();
        // public static SingletonSqlMessengerService Instance => instance;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SingletonSqlMessengerService GetInstance()
        {
            return instance;
        }

        public void SetSqlMessageData(CSqlMessage msg)
        {
            SqlMsgDataInstance.ConnectionSuccess = msg.ConnectionSuccess;
            SqlMsgDataInstance.SqlMessage = msg.SqlMessage;
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
