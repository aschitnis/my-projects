
namespace schnittstelle.mysql.library.VO.Model
{
    public class ValueObjectBookModel 
    {
        private int bookid;
        private string title;
        private string description;
        private string author;

        public int BookId
        {
            get { return bookid; }
            set { bookid = value;  }
        }

        public string Title
        {
            get { return title; }
            set { title = value;  }
        }
        public string Description
        {
            get { return description; }
            set { description = value;  }
        }

        public string Author
        {
            get { return author; }
            set { author = value;  }
        }
    }
}
