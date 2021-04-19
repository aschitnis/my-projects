using System;
using System.ComponentModel;

namespace schnittstelle.mysql.library.VO.Model
{
    public class ValueObjectOrderModel : INotifyPropertyChanged
    {
        private int orderid;
            private int booksellerid;
            private int orderstatusid;
            private DateTime orderdate;
            private string comment;
            private DateTime expecteddate; 

        public int OrderId
        {
            get { return orderid; }
            set { orderid = value;OnPropertyChanged("OrderId"); }
        }
        public int BookSellerId
        {
            get { return booksellerid; }
            set { booksellerid = value; OnPropertyChanged("BookSellerId"); }
        }

        public int OrderStatusId
        {
            get { return orderstatusid; }
            set { orderstatusid = value; OnPropertyChanged("OrderStatusId"); }
        }
        public DateTime OrderDate
        {
            get { return orderdate; }
            set { orderdate = value; OnPropertyChanged("OrderDate"); }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; OnPropertyChanged("Comment"); }
        }

        public DateTime ExpectedDate
        {
            get { return expecteddate; }
            set { expecteddate = value; OnPropertyChanged("ExpectedDate"); }
        }

        #region PropertyChanged Notification
        public event PropertyChangedEventHandler PropertyChanged;
        // public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName) );

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
