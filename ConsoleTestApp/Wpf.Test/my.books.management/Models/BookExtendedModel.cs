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
    public class BookExtendedModel : JsonBookExtendedModel, INotifyPropertyChanged, ICloneable, IModel
    {
        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructors
        public BookExtendedModel() { }

        /*** copy constructor   ***/
        public BookExtendedModel(JsonBookExtendedModel book)
        {
            this.BookId = book.BookId;
            this.Indexes = book.Indexes;
        }
        #endregion
        public static BookExtendedModel DeepCopy(BookExtendedModel model)
        {
            return new BookExtendedModel(model);
        }
        #region
        public override bool Equals(object obj)
        {
            BookExtendedModel bookextendeditem = obj as BookExtendedModel;
            return bookextendeditem == null ? false : bookextendeditem.BookId == this.BookId;
        }
        public override int GetHashCode()
        {
            return this.BookId.GetHashCode();
        }

        public BookExtendedModel Clone()
        {
            var clone = this.MemberwiseClone() as BookExtendedModel;
            return clone;
        }
        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
