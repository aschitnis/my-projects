using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services
{
    public class SqlMessageEventArgs : EventArgs, INotifyPropertyChanged
    {
        private string _data;
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        private CSqlMessage _sqlmessageobject;

        public CSqlMessage SqlMessageObject
        {
            get { return _sqlmessageobject; }
            set { _sqlmessageobject = value; RaisePropertyChanged(); }
        }

        public SqlMessageEventArgs(string _message)
        {
            _data = _message;
        }

        public SqlMessageEventArgs(CSqlMessage sqldataObject)
        {
            SqlMessageObject               = new CSqlMessage();
            SqlMessageObject.SqlMessage   = sqldataObject.SqlMessage;
            SqlMessageObject.Filename     = sqldataObject.Filename;
            SqlMessageObject.Functionname   = sqldataObject.Functionname;
            SqlMessageObject.ConnectionSuccess = sqldataObject.ConnectionSuccess;
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

    public class CSqlMessage : INotifyPropertyChanged
    {
        private bool connectionsuccess;

        public bool ConnectionSuccess
        {
            get { return connectionsuccess; }
            set { connectionsuccess = value; RaisePropertyChanged(); }
        }

        private string _sqlmessage;
        public string SqlMessage
        {
            get { return _sqlmessage; }
            set { _sqlmessage = value; RaisePropertyChanged(); }
        }
        public string Filename { get; set; }
        public string Functionname { get; set; }

        public CSqlMessage()
        {
            
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
