
namespace schnittstelle.mysql.library.VO.Model
{
    public class ValueObjectBookSellerModel 
    {
        private int booksellerid;
        private int bookid;
        private int sellerid;
        private int currencyid;
        private double price;
        private string comments;

        public int BookSellerId
        {
            get { return booksellerid; }
            set { booksellerid = value;  }
        }
        public int BookId
        {
            get { return bookid; }
            set { bookid = value; }
        }
        public int SellerId
        {
            get { return sellerid; }
            set { sellerid = value;  }
        }

        public int CurrencyId
        {
            get { return currencyid; }
            set { currencyid = value;  }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public string Comments
        {
            get { return comments; }
            set { comments = value;  }
        }
    }
}
