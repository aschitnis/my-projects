
namespace schnittstelle.mysql.library.VO.Model
{
    public class ValueObjectSellerModel 
    {
        private int sellerid;
        public int SellerId
        {
            get { return sellerid; }
            set { sellerid = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
