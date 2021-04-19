
namespace schnittstelle.mysql.library.VO.Model
{
    public class ValueObjectOrderStatusModel 
    {
        private int orderstatusid;
        private string statusname;

        public int OrderStatusId
        {
            get { return orderstatusid; }
            set { orderstatusid = value;  }
        }
        public string StatusName
        {
            get { return statusname; }
            set { statusname = value;  }
        }
    }
}
