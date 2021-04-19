

namespace schnittstelle.mysql.library.VO.Model
{
    public class ValueObjectCurrencyModel 
    {
        private int currencyid;
        private string name;
        private string tag;

        public int CurrencyId
        {
            get { return currencyid; }
            set { currencyid = value;  }
        }
        public string Name
        {
            get { return name; }
            set { name = value;  }
        }
        public string Tag
        {
            get { return tag; }
            set { tag = value;  }
        }
    }
}
