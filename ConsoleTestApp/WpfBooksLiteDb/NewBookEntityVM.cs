using LiteDB;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfBooksLiteDb.Database.Entities;

namespace WpfBooksLiteDb
{
    public class NewBookEntityVM : INotifyPropertyChanged
    {
        internal enum SearchBookbyNameOrAuthor { SearchbyBookName = 0, SearchByBookAuthor = 1 };
        public event EventHandler<int> OnSearchBooksCompletedEventHandler;
        public event EventHandler<string> OnDatabaseProcessingCompletedEventHandler;

        #region Commands

        public ICommand SaveBookEntityToDatabaseCommand { get; set; }
        public ICommand SearchBookEntitiesCommand { get; set; }
        public ICommand GetAllBooksFromDatabaseCommand { get; set; }
        #endregion

        #region Properties
        public string DbPathString {get;private set; }

        private string searchbookterm;
        private bool isfilterbynameselected;
        private bool isfilterbyauthorselected;
        private bool iserror = false;
        private string dbmessage;
        private string currentsortoption;
        private BookEntity book;
        private BookEntity selectedbook;
        private bool iscreatenewbookenabled = false;
        private bool issavenewbookenabled = false;
        private bool isgridbookmarkfornewbookenabled = false;
        private ObservableCollection<BookEntity> books;
        private ObservableCollection<BookEntity> searchbooks = new ObservableCollection<BookEntity>();
        public bool IsFilterbyNameSelected 
        {
            get { return isfilterbynameselected; }
            set { 
                  isfilterbynameselected = value;
                  OnPropertyChanged();
                }
        }
        public bool IsFilterByAuthorSelected 
        {
            get { return isfilterbyauthorselected; }
            set { 
                  isfilterbyauthorselected = value;
                  OnPropertyChanged();
                }
        }
        public string SearchBookTerm
        {
            get { return searchbookterm; }
            set { searchbookterm = value;OnPropertyChanged(); }
        }
        private string CurrentSortOption
        {
            get { return currentsortoption; }
            set { currentsortoption = value; OnPropertyChanged(); }
        }
        public bool IsError
        {
            get { return iserror; }
            set { iserror = value; OnPropertyChanged(); }
        }
        public string DbMessage
        {
            get { return dbmessage; }
            set { dbmessage = value;OnPropertyChanged(); }
        }
        public bool IsGridBookMarkForNewBookEnabled
        {
            get { return isgridbookmarkfornewbookenabled; }
            set { isgridbookmarkfornewbookenabled = value; OnPropertyChanged(); }
        }
        public bool IsCreateNewBookEnabled
        {
            get { return iscreatenewbookenabled; }
            set { iscreatenewbookenabled = value; OnPropertyChanged(); }
        }

        public bool IsSaveNewBookEnabled
        {
            get { return issavenewbookenabled; }
            set { issavenewbookenabled = value; OnPropertyChanged(); }
        }
        public BookEntity Book
        {
            get { if (book == null) book = new BookEntity(); return book; }
            set { book = value; OnPropertyChanged(); }
        }
        public BookEntity SelectedBook
        {
            get { return selectedbook; }
            set 
            {
                if (value != null)
                {
                    Book = value;
                    OnPropertyChanged("Book");
                }

                selectedbook = value; 
                OnPropertyChanged();
            }
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

        #region Constructors
        public NewBookEntityVM() { }
        public NewBookEntityVM(string _dbpath) 
        { 
            DbPathString = _dbpath;
            IsFilterbyNameSelected = true;
            SaveBookEntityToDatabaseCommand = new DbEntityViewModelCommand(SaveNewBookEntityToDatabase, IsSaveNewBookAllowed);
            SearchBookEntitiesCommand       = new DbEntityViewModelCommand(SearchBooks, (p) => !String.IsNullOrEmpty(SearchBookTerm));
            GetAllBooksFromDatabaseCommand = new DbEntityViewModelCommand(GetAllBooksAsyncCommandMethod, (d) => true); ;
            OnSearchBooksCompletedEventHandler += BookEntityVM_OnSearchBooksComplete;
            OnDatabaseProcessingCompletedEventHandler += BookEntityVM_OnDatabaseProcessingCompleted;
        }
        #endregion

        #region TEST METHODS
        public Task<long> GetTaskPrimeNumberCalculation(long num)
        {
            long primeNumberResult = 0;

            Task<long> t = Task.Run(() =>
            {
                // bool isPrime = true;
                for (long i = 0; i <= num; i++)
                {
                    bool isPrime = true; // Move initialization to here
                    for (long j = 2; j < i; j++) // you actually only need to check up to sqrt(i)
                    {
                        if (i % j == 0) // you don't need the first condition
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime)
                    {
                        primeNumberResult = i;
                        // Console.WriteLine("Prime:" + i);
                    }
                    // isPrime = true;
                }
                return primeNumberResult;
            });
            //DbMessage = "waiting....";
            //await t;
            return t;
        }
        public async Task CalculatePrimeNumberAsync()
        {
            long eingehendeWertZahl = 189999;
            Task<long> t = Task.Run(() => GetTaskPrimeNumberCalculation(eingehendeWertZahl));
            DbMessage = $"Primnummer für {eingehendeWertZahl} wird berechnet";
            await t;
            //TaskAwaiter<long> aw = t.GetAwaiter();
            //long lResult = aw.GetResult();
            DbMessage = $"Primnummer für {eingehendeWertZahl}: {t.Result}";
        }
        #endregion

        #region Methods
        /**********************
        public void GetAllBookEntities()
        {
            IsSaveNewBookEnabled = true;
            IsCreateNewBookEnabled = true;

            using (var db = new LiteDatabase(DbPathString))
            {
                var booksInDb = db.GetCollection<BookEntity>("books").FindAll();
                
                Books = new ObservableCollection<BookEntity>(booksInDb);
            }
            if (Books.Count() > 0)
            {
                SelectedBook = Books.First();
            }
        }
        *******************/

        private bool IsSaveNewBookAllowed(object parameter)
        {
            if (IsSaveNewBookEnabled)
                return true;
            else return false;
        }

        //public void GetAsyncAllBooks()
        //{
        //    Task<ObservableCollection<BookEntity>> t = Task.Run<ObservableCollection<BookEntity>>(() => { return GetAllBooksFromDatabaseAsync(); });
        //    TaskAwaiter<ObservableCollection<BookEntity>> taskawaiter = t.GetAwaiter();
        //    Books = taskawaiter.GetResult();

        //    IsSaveNewBookEnabled = true;
        //    IsCreateNewBookEnabled = true;

        //    if (Books.Count() > 0)
        //    {
        //        SelectedBook = Books.First();
        //    }
        //}

        public void GetAllBooksAsyncCommandMethod(object parameter)
        {
            Task.Run(() => GetAllBooksAsync());
        }
        private async Task GetAllBooksAsync()
        {
            Task<ObservableCollection<BookEntity>> t = Task.Run(() => { return GetTaskAllBooksFromDatabase(); });
            await t;
            Books = t.Result;
            CallDatabaseProcessCompletedEvent($"{Books.Count()} Bücher im DB gefunden.");

            if (Books.Count() > 0)
            {
                SelectedBook = Books.First();
            }

            IsSaveNewBookEnabled = true;
            IsCreateNewBookEnabled = true;
        }

        private Task<ObservableCollection<BookEntity>> GetTaskAllBooksFromDatabase()
        {
            ObservableCollection<BookEntity> bookEntities = new ObservableCollection<BookEntity>();
            Task<ObservableCollection<BookEntity>> t = Task.Run(() =>
            {
                using (var db = new LiteDatabase(DbPathString))
                {
                    var booksInDb = db.GetCollection<BookEntity>("books").FindAll();

                    bookEntities = new ObservableCollection<BookEntity>(booksInDb);
                }
                return bookEntities;
            });
            return t;
        }

        private void SearchBooks(object parameter)
        {
            if (!string.IsNullOrEmpty(SearchBookTerm) && (IsFilterByAuthorSelected || IsFilterbyNameSelected) )
            {
                Books = IsFilterByAuthorSelected == true ? new ObservableCollection<BookEntity>(Books.Where((x) => x.Author.ToLower().Contains(SearchBookTerm.ToLower()))) : IsFilterbyNameSelected == true ? new ObservableCollection<BookEntity>(Books.Where((x) => x.Name.ToLower().Contains(SearchBookTerm.ToLower()))) : Books;
                if (IsFilterByAuthorSelected) CallSearchBooksEvent(Convert.ToInt32(SearchBookbyNameOrAuthor.SearchByBookAuthor));
                else if (IsFilterbyNameSelected) CallSearchBooksEvent(Convert.ToInt32(SearchBookbyNameOrAuthor.SearchbyBookName));
            }
        }
        private void SaveNewBookEntityToDatabase(object parameter)
        {
            //using (var db = new LiteDatabase(DbPathString))
            //{
            //    ILiteCollection<BookEntity> booksList = db.GetCollection<BookEntity>("books");
            //    BookEntity book = booksList.FindOne(x => x.BkId == Book.BkId);
            //    if (book == null)
            //    {
            //        int bookIdmaxprimarykeyvalue = booksList.Count() == 0 ? 1 : booksList.Max(m => m.BkId);
            //        int idValue = booksList.Count() == 0 ? 1 : booksList.Max(m => m.Id);
            //        Book.BkId = bookIdmaxprimarykeyvalue + 1;
            //        Book.Id = idValue + 1;

            //        foreach (BookmarkEntity bookmark in Book.BookmarkEntities)
            //        {
            //            bookmark.FkbkId = Book.BkId;
            //        }

            //        booksList.Insert(Book);
            //        booksList.EnsureIndex(b => b.Id);

            //        CallOnNewOrEditBookSavedToDatabaseCompletedEvent($"Neues Buch - {Book.Name} - hinzugefügt.");
            //    }
            //    else
            //    {
            //        book.Author = SelectedBook.Author;
            //        book.Name = SelectedBook.Name;
            //        book.Description = SelectedBook.Description;

            //        booksList.Update(book);
            //    }
            //}

            LiteDatabase databaseObject = new LiteDatabase(DbPathString);
            ILiteCollection<BookEntity> booksList = databaseObject.GetCollection<BookEntity>("books");
            BookEntity book = booksList.FindOne(x => x.BkId == Book.BkId);
            
            if (book == null)   // new book 
            {
                if (booksList.FindOne(x => x.Name == Book.Name)==null)
                {
                    // go ahead with the code to add a new book to the database table
                    int bookIdmaxprimarykeyvalue = booksList.Count() == 0 ? 1 : booksList.Max(m => m.BkId);
                    int idValue = booksList.Count() == 0 ? 1 : booksList.Max(m => m.Id);
                    Book.BkId = bookIdmaxprimarykeyvalue + 1;
                    Book.Id = idValue + 1;

                    foreach (BookmarkEntity bookmark in Book.BookmarkEntities)
                    {
                        bookmark.FkbkId = Book.BkId;
                    }

                    booksList.Insert(Book);
                    booksList.EnsureIndex(b => b.Id);
                    IsError = false;
                    CallDatabaseProcessCompletedEvent($"Neues Buch - {Book.Name} - hinzugefügt.");
                    databaseObject.Dispose();
                }
                else
                {
                    IsError = true;
                    // message....this book title is already existing in the databases
                    databaseObject.Dispose();
                    CallDatabaseProcessCompletedEvent($"{Book.Name} - ist bereits vorhanden");
                }
            }
            else  // UPDATE EXISTING BOOK details.  
            {
                book.Author = SelectedBook.Author;
                book.Name = SelectedBook.Name;
                book.Description = SelectedBook.Description;
                booksList.Update(book);
                databaseObject.Dispose();
                CallDatabaseProcessCompletedEvent($"Änderungen zum Buch - {Book.Name} - gespeichert.");
            }
            GetAllBooksAsync();
            IsSaveNewBookEnabled = true;
            IsGridBookMarkForNewBookEnabled = true;
            IsCreateNewBookEnabled = true;
        }

        public void CreateNewBookEntity()
        {
            Book = new BookEntity();

            IsGridBookMarkForNewBookEnabled = true;
            IsSaveNewBookEnabled = true;
               // List<BookEntity> selectedbooks = booksList.Find(f => f.Author.StartsWith("Irin")).ToList<BookEntity>();
        }

        #endregion

        #region Event Subscriber methods
        public void BookEntityVM_OnDatabaseProcessingCompleted(object sender, string e)
        {
            DbMessage = e;
        }

        public void BookEntityVM_OnSearchBooksComplete(object sender, int e)
        {
            if (e == 0)
                DbMessage = $"Books matching by Name: {Books.Count()}" ;
            else if (e == 1)
                DbMessage = $"Books matching by Author: {Books.Count()}";
        }
        #endregion

        #region Event Invoke -Methods
        public void CallSearchBooksEvent(int enumSearchBy)
        {
            OnSearchBooksCompletedEventHandler.Invoke(this, enumSearchBy);
        }
        protected void CallDatabaseProcessCompletedEvent(string message)
        {
            OnDatabaseProcessingCompletedEventHandler?.Invoke(this, message);
        }
        #endregion

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
