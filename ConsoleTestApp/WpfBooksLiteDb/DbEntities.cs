using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfBooksLiteDb.Database.Entities
{
    public enum BookEntityComparerType
    {
        BookName,
        BookAuthor,
        Bookdescription,
        None
    }

    public class BookEntity : INotifyPropertyChanged
    {
        private int bkid = 0;
        private string name;
        private string author;
        private string description;
        private ObservableCollection<BookmarkEntity> bookmarkentities;

        public int Id { get; set; } = 1;

        public int BkId 
        { get { return bkid; } set { bkid = value;OnPropertyChanged(); } }
        public string Name { get { return name; } set { name = value;OnPropertyChanged(); } }
        public string Author { get { return author; } set { author = value;OnPropertyChanged(); } }
        public string Description { get { return description; } set { description = value; } }
        public ObservableCollection<BookmarkEntity> BookmarkEntities { get { return bookmarkentities; } set { bookmarkentities = value; OnPropertyChanged(); } }
        public BookEntity()
        {
            BookmarkEntities = new ObservableCollection<BookmarkEntity>();
            // bookmarkentities.OrderBy(b, )
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class BookEntityComparer : IComparer<BookEntity>
    {
        private BookEntityComparerType bookcompareEnum;
        public BookEntityComparer(BookEntityComparerType _bookcompareenum)
        {
            bookcompareEnum = _bookcompareenum;
        }

        public int Compare(BookEntity x, BookEntity y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return 1;
            if (y == null) return -1;

            switch(bookcompareEnum)
            {
                case BookEntityComparerType.BookAuthor:
                    return string.Compare(x.Author, y.Author);
                case BookEntityComparerType.BookName:
                    return string.Compare(x.Name, y.Name);
                case BookEntityComparerType.Bookdescription:
                    return string.Compare(x.Description, y.Description);
                case BookEntityComparerType.None:
                    if (x.BkId == y.BkId) return 0;
                    else if (x.BkId < y.BkId) return -1;
                    else return 1;
                default:
                    throw new ArgumentException("Undefined Compare Type");
            }
        }
    }
    public class BookmarkEntity : INotifyPropertyChanged
    {
        private int bmid = 0;
        private string pagenumber;
        private string descriptiontag;
        private int fkbkid;

        public int Id { get; set; } = 1;
        public int BmId 
        {
            get { return bmid; }
            set { bmid = value; OnPropertyChanged(); }
        }
        public string PageNumber 
        {
            get { return pagenumber; }
            set { pagenumber = value; OnPropertyChanged(); }
        }
        public int FkbkId 
        {
            get { return fkbkid; }
            set { fkbkid = value; OnPropertyChanged(); }
        }
        public string DescriptionTag 
        {
            get { return descriptiontag; }
            set { descriptiontag = value; OnPropertyChanged(); }
        }
        public BookmarkEntity()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
