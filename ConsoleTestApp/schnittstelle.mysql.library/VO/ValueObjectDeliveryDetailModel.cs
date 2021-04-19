using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.library.VO
{
    public class ValueObjectDeliveryDetailModel 
    {
        #region Properties
        private int deliveryid;
        private int orderid;
        private int deliverystatusid;
        private string deliverylocation;
        private DateTime delivereddate;
        private string comments;

        public int DeliveryId
        {
            get { return deliveryid; }
            set { deliveryid = value;  }
        }
        public int OrderId
        {
            get { return orderid; }
            set { orderid = value;  }
        }
        public int DeliveryStatusId
        {
            get { return deliverystatusid; }
            set { deliverystatusid = value; }
        }
        public string DeliveryLocation
        {
            get { return deliverylocation; }
            set { deliverylocation = value;  }
        }
        public DateTime DeliveryDate
        {
            get { return delivereddate; }
            set { delivereddate = value; }
        }
        public string Comments
        {
            get { return comments; }
            set { comments = value;  }
        }
        #endregion
    }
}
