using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.books.management
{
    [Serializable]
    public class BookModel : JsonBookModel, INotifyPropertyChanged, ICloneable, IEquatable<BookModel>, IModel
    {
        #region copy constructor & normal constructor
        public BookModel(JsonBookModel book)
        {
            this.Id = book.Id;
            this.Name = book.Name;
            this.Language = book.Language;
            this.Publisher = book.Publisher;
            this.Author = book.Author;
            this.Information = book.Information;
        }

        public BookModel() { }
        public BookModel(int id) { this.Id = id; }
        #endregion

        #region
        //public override bool Equals(object other)
        //{
        //    if (other == null) return false;
        //    return this.Equals(other);
        //}
        public override int GetHashCode()
        {
           return this.Id.GetHashCode();
        }

        public bool Equals(BookModel otherBook)
        {
            if (otherBook == null) return false;

            return this.Id == otherBook.Id || this.Name == otherBook.Name ? true : false;
        }

        // make a deep copy
        public static BookModel DeepCopy(BookModel book)
        {
            return new BookModel(book);
        }
        #endregion

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BookModel Clone()
        {
            var clone = this.MemberwiseClone() as BookModel;
            return clone;
        }
        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
