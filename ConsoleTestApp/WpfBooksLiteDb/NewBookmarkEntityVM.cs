using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfBooksLiteDb.Database.Entities;
using WpfBooksLiteDb.Database.Entities.VM;

namespace WpfBooksLiteDb.Entities.ViewModels
{
    public class NewBookmarkEntityVM : INotifyPropertyChanged, IDisposable
    {
        public string DbPathString { get; private set; }
        private bool isDisposed = false;
        private BookEntity selectedbook;
        private BookmarkEntity bookmark;
        private BookmarkEntity selectedbookmark;
        private string datachangessavedmessage;
        private ObservableCollection<BookEntity> books;

        public event EventHandler<string> OnSaveToDatabaseCompletedEventHandler;
        public ICommand SaveBookmarksToDatabaseCommand { get; set; }

        #region Properties
        public string DataChangesSavedMessage 
        {
            get { return datachangessavedmessage; }
            set { datachangessavedmessage = value;OnPropertyChanged(); }
        }
        public BookmarkEntity Bookmark 
        {
            get 
            {
                if (bookmark == null)
                    bookmark = new BookmarkEntity();
                return bookmark;
            }
            set { bookmark = value;OnPropertyChanged(); }
        }
        public BookmarkEntity SelectedBookmark 
        {
            get { return selectedbookmark;  }
            set 
            {
                if (value != null)
                {
                    Bookmark = value;
                    OnPropertyChanged("Bookmark");
                }
                selectedbookmark = value;
                OnPropertyChanged();
            }
        }
        public BookEntity SelectedBook 
        {
            get { return selectedbook; }
            set { selectedbook = value;OnPropertyChanged(); }
        }

        public ObservableCollection<BookEntity> Books
        {
            get 
            {
                if (books == null)
                    books = new ObservableCollection<BookEntity>();
                return books;
            }
            set { books = value; OnPropertyChanged(); }
        }
        #endregion

        #region Constructor
        public NewBookmarkEntityVM() { }
        public NewBookmarkEntityVM(string _dbpath) 
        {
            OnSaveToDatabaseCompletedEventHandler += BookmarkEntityVM_OnSaveToDatabaseCompleted;
            DbPathString = _dbpath;
            DisplayAllBooksFromDatabase();
            SaveBookmarksToDatabaseCommand = new DbEntityViewModelCommand(SaveBookmarksToDatabase, IsSaveBookmarksAllowed);
        }
        #endregion

        #region Methods
        private bool IsSaveBookmarksAllowed(object parameter)
        {
            if (parameter == null) return false;

            var selectedbookentity = parameter as BookEntity;
            if (selectedbookentity.BookmarkEntities == null)
            {
                return false;
            }
            else
            {
                if (selectedbookentity.BookmarkEntities.Count() > 0)     
                    return true;
                else 
                    return false;
            }
        }
        private void SaveBookmarksToDatabase(object parameter)
        {
            var selectedbookentity = parameter as BookEntity;

            if (Bookmark == null || Bookmark.BmId == 0)
                return;

            string dbResultMsg = "";

            using (var db = new LiteDatabase(DbPathString))
            {
                int iUpdateCount = 0;
                int iInsertCount = 0;

                var allbooks = db.GetCollection<BookEntity>("books");

                BookEntity bookentity = allbooks.FindOne(b => b.BkId == selectedbookentity.BkId);
                
                foreach (BookmarkEntity obookmark in selectedbookentity.BookmarkEntities)
                {
                    BookmarkEntity bookmarkentity = bookentity.BookmarkEntities.Where(m => m.BmId == obookmark.BmId).FirstOrDefault();
                    if (bookmarkentity != null)
                    {
                        iUpdateCount++;
                        bookmarkentity.Id = obookmark.Id;
                        bookmarkentity.BmId = obookmark.BmId;
                        bookmarkentity.FkbkId = bookentity.BkId;
                        bookmarkentity.PageNumber = obookmark.PageNumber;
                        bookmarkentity.DescriptionTag = obookmark.DescriptionTag;
                    }
                    else
                    {
                        iInsertCount++;
                        bookmarkentity = new BookmarkEntity();
                        bookmarkentity.Id = obookmark.Id;
                        bookmarkentity.BmId = obookmark.BmId;
                        bookmarkentity.FkbkId = bookentity.BkId;
                        bookmarkentity.PageNumber = obookmark.PageNumber;
                        bookmarkentity.DescriptionTag = obookmark.DescriptionTag;
                        bookentity.BookmarkEntities.Add(bookmarkentity);
                    }
                    allbooks.Update(bookentity);
                }

                if (iUpdateCount > 0)
                    dbResultMsg = "Updated: " + iUpdateCount;
                if (iInsertCount > 0)
                    dbResultMsg = dbResultMsg + " - New: " + iInsertCount;
                if (iUpdateCount == 0 && iInsertCount == 0)
                    dbResultMsg = "Keine Daten zum Speichern";
            }
            CallOnSavedToDatabaseEvent(dbResultMsg);
        }
        public void DisplayAllBooksFromDatabase()
        {
            using (var db = new LiteDatabase(DbPathString))
            {
                ILiteCollection<BookEntity> booksList = db.GetCollection<BookEntity>("books");
                if (booksList.Count() > 0)
                {
                    Books = new ObservableCollection<BookEntity>(booksList.Query().Select(x => x).ToList());
                    Books.OrderBy(x => x, new BookEntityComparer(BookEntityComparerType.BookName));
                }
                else
                {
                    // disable not-required button controls ToDo
                }
            }
        }

        private int GetTheGreatestIdNumberFromAllTheBookmarksInTheDataGrid()
        {
            return SelectedBook.BookmarkEntities.Count() == 0 ? 1 : SelectedBook.BookmarkEntities.Max(m => m.Id);
        }

        /** Lesezeichen Hinzufügen **/
        public void AddOrUpdateBookmarkToBookmarkList()
        {
            if (Bookmark.BmId > 0)
            {
                using (var db = new LiteDatabase(DbPathString))
                {
                    var allbooks = db.GetCollection<BookEntity>("books");

                    BookEntity bookentity = allbooks.FindOne(b => b.BkId == SelectedBook.BkId);

                    if (SelectedBook.BookmarkEntities.Where(e => e.BmId == Bookmark.BmId).FirstOrDefault() == null)
                    {
                        int bookmarkMaxId = bookentity.BookmarkEntities.Count() == 0 ? 1 : bookentity.BookmarkEntities.Max(m => m.Id);
                        Bookmark.Id = bookmarkMaxId + 1;
                        SelectedBook.BookmarkEntities.Add(Bookmark);
                    }
                    else
                    {
                        SelectedBookmark.BmId = Bookmark.BmId;
                        SelectedBookmark.FkbkId = Bookmark.FkbkId;
                        SelectedBookmark.DescriptionTag = Bookmark.DescriptionTag;
                        SelectedBookmark.PageNumber = Bookmark.PageNumber;
                    }
                }
            }
        }
        public void CreateNewBookmark()
        {
            Bookmark = new BookmarkEntity();
            SelectedBookmark = null;
        }
        public void BookmarkEntityVM_OnSaveToDatabaseCompleted(object sender, string e)
        {
            DataChangesSavedMessage = e;
        }
        #endregion

        #region  Event Methods
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void CallOnSavedToDatabaseEvent(string message)
        {
            OnSaveToDatabaseCompletedEventHandler?.Invoke(this, message);
        }
        #endregion

        #region Dispose Methods
        public void Dispose()
        {
            Dispose(true);

            /* GC.SuppressFinalize(this) :
             * Objects that implement the IDisposable interface can call this method 
             * from the IDisposable.Dispose method to prevent the garbage collector 
             * from calling Object.Finalize on an object that does not require it.
             */
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // clean up managed objects by calling their Dispose() method.

                    // IsDatabaseConnected = false;
                }
                // clean up unmanaged objects here
            }
            isDisposed = true;
        }
        #endregion

        #region Destructor
        ~NewBookmarkEntityVM()
        {
            Dispose(false);
        }
        #endregion
    }
}
