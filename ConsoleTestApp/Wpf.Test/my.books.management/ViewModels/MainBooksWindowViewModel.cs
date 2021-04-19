using System;
using System.Resources;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Wpf.Test.my.books.management.ViewModels;
using Wpf.Test.my.books.management.MVVM.Dialog;

namespace Wpf.Test.my.books.management
{

    public class MainBooksWindowViewModel : INotifyPropertyChanged, IDialogRequestClose
    {
        public enum enumFilterValue
        {
            None,
            Author,
            Publisher,
            Bookname
        }
        private readonly IDialogService dialogService;

        private BooksManagementStore StorageAccessInstance = BooksManagementStore.Instance;
        private Dictionary<string, List<BookModel>> AuthorsWithBooksDictionary = new Dictionary<string, List<BookModel>>();

        private enumFilterValue selectedsearchbyvalue;
        private string searchbyfiltertext;
        private BookModel currentbook;
        private List<BookModel> bookscontainer;
        private string currentsearchoption;
        private List<string> searchoptions;
        private ObservableCollection<BookModel> searchresultbooks;
        private List<string> authors;
        private string currentauthor;
        private HashSet<BookExtendedModel> additionaldatacontainer;
        private Dictionary<BookModel, BookExtendedModel> booktoadditionaldatamappingdictionary;
        private BookExtendedModel additionaldataforcurrentbook;
        private string currentadditionaldataindexinformation;
        private string executionmessage;

        #region Events
        public event EventHandler<ErrorEventArgs> messageEvent;
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        #endregion

        #region Props
        public enumFilterValue SelectedSearchByValue
        {
            get { return selectedsearchbyvalue; }
            set
            {
                selectedsearchbyvalue = value;
                OnChanged();
            }
        }

        public List<enumFilterValue> EnumSearchByValues
        {
            get { return Enum.GetValues(typeof(enumFilterValue)).OfType<enumFilterValue>().ToList(); }
        }
        public int NextBookId
        {
            get { return StorageAccessInstance.BookModelsContainer.Count == 0 ? 1 : StorageAccessInstance.BookModelsContainer.Max(n => n.Id) + 1; }
        }
        public ICommand DisplayNewBookDialogCommand { get; }
        public ICommand SearchBooksByFilterCommand { get; set; }
        public ICommand SetToDefaultCommand { get; set; }
        public string SearchByFilterText 
        {
            get { return searchbyfiltertext; }
            set { searchbyfiltertext = value; OnChanged(); }
        }

        public string CurrentAdditionalDataDescription 
        {
            get { return currentadditionaldataindexinformation; }
            set { currentadditionaldataindexinformation = value; OnChanged(); }
        }
        public BookModel CurrentBook 
        {
            get { return currentbook; }
            set 
            { 
                currentbook = value;OnChanged();
                SearchDictionaryToFindAdditionalDataBelongingToCurrentBook();
            }
        }

        public ObservableCollection<BookModel> SearchResultBooks 
        {
            get { return searchresultbooks; }
            set { searchresultbooks = value; OnChanged(); }
        }

        public List<string> Authors 
        {
            get { return authors ?? (authors = new List<string>()); }
            set { authors = value;OnChanged(); }
        }

        public string CurrentAuthor 
        {
            get { return currentauthor; }
            set 
            { 
                currentauthor = value;
                OnChanged();
                FindBooksBelongingToCurrentAuthor();
            }
        }
        public HashSet<BookExtendedModel> HashSetExtendedDataContainer 
        {
            get { return additionaldatacontainer; }
            set { additionaldatacontainer = value; OnChanged(); }
        }
        public BookExtendedModel AdditionalDataForCurrentBook 
        {
            get { return additionaldataforcurrentbook; }
            set { additionaldataforcurrentbook = value; OnChanged(); }
        }
        public Dictionary<BookModel,BookExtendedModel> BookModelToExtendedBookModelMappingDictionary 
        {
            get 
            {
               return booktoadditionaldatamappingdictionary ?? (booktoadditionaldatamappingdictionary = new Dictionary<BookModel, BookExtendedModel>());
            }
            set { booktoadditionaldatamappingdictionary = value; OnChanged(); }
        }
        public string ExecutionMessage 
        {
            get { return executionmessage; }
            set { executionmessage = value;OnChanged(); }
        }
        public List<BookModel> BooksContainer 
        {
            get { return bookscontainer ?? (bookscontainer = new List<BookModel>()); }
            set { bookscontainer = value; OnChanged(); }
        }
        #endregion

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructor
        public MainBooksWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            messageEvent -= HandleExecutionMessageEvent;
            messageEvent += HandleExecutionMessageEvent;
            
            DisplayNewBookDialogCommand = new BookManagementCommand(DisplayViewForNewBook, (p) => true);
            SearchBooksByFilterCommand = new BookManagementCommand(SearchBooksByFilter, OnCanExecuteSearchBooksByFilterCommand);
            SetToDefaultCommand = new BookManagementCommand(SetDisplayDataToDefault, (x) =>  true);
            Init();
        }
        #endregion

        /// <summary>
        /// Opens a new Window to make data entries for a new book. 
        /// </summary>
        /// <param name="parameter">Presently this parameter which can be entered from the XAML, is NULL</param>
        public void DisplayViewForNewBook(object parameter)
        {
            int maxId = NextBookId;

            DialogNewBookViewModel newBookVM = new DialogNewBookViewModel(maxId);

            bool? result = dialogService.ShowDialog(newBookVM);

            ReloadData();
        }

        private List<string> GetResourceData()
        {
            // read strings from resx file & fill the combobox.
            Assembly assembly = this.GetType().Assembly;
            ResourceManager resman = new ResourceManager("Wpf.Test.Properties.Resources", assembly);
            return new List<string>() { resman.GetString("resNone"), resman.GetString("resAutor"),
                                        resman.GetString("resPublisher"),resman.GetString("resBookName")};
        }

        private void ReloadData()
        {
            Exception ex = StorageAccessInstance.InitializeBooksAndAdditionalData();
            if (ex == null)
            {
                BooksContainer = StorageAccessInstance.BookModelsContainer;
                ExecutionMessage = $"Books found: {BooksContainer.Count} ";

                AuthorsWithBooksDictionary.Clear();
                Authors.Clear();
                BookModelToExtendedBookModelMappingDictionary.Clear();

                GetAllAuthorsByName();
                MapBooksWithAdditionalData();
            }
        }

        private void Init()
        {
            SearchResultBooks = new ObservableCollection<BookModel>();
            
            Exception ex = StorageAccessInstance.InitializeBooksAndAdditionalData();
            if (ex == null)
            {
                BooksContainer = StorageAccessInstance.BookModelsContainer;

                ExecutionMessage = $"Books found: {BooksContainer.Count} ";
                GetAllAuthorsByName();
                MapBooksWithAdditionalData();
            }
            else
            {
                OnExecutionMessage("Error reading Json data (Books)", ex);
            }
        }

        public bool OnCanExecuteSearchBooksByFilterCommand(object parameter)
        {
            return SearchResultBooks != null && SelectedSearchByValue != enumFilterValue.None;
        }
        public void SearchBooksByFilter(object parameter)
        {
            if (SearchResultBooks == null)
                SearchResultBooks = new ObservableCollection<BookModel>();
            else SearchResultBooks.Clear();

            RegexSearchBooksByFilter();
        }
        public void SetDisplayDataToDefault(object parameter)
        {
            CurrentAuthor = "All";
        }

        /* The Dictionary<BookModel,ExtendedBookModel> is filled up. */
        private void MapBooksWithAdditionalData()
        {
            /* get all ExtendedBookModels from the json file. */
            /* convert List<ExtendedBookModel> to HashSet<ExtendedBookModel> */
            HashSetExtendedDataContainer = new HashSet<BookExtendedModel>(StorageAccessInstance.ExtendedModelsContainer);
            
            if (HashSetExtendedDataContainer.Count > 0)
            {
                ExecutionMessage += $"Extended-data found: {HashSetExtendedDataContainer.Count} ";

                // find which books have additional data (ExtendedBookModel) 
                IEnumerable<BookModel> booksWithExtendedData = BooksContainer.
                                                                Where(c => HashSetExtendedDataContainer.
                                                                Where(a => a.BookId == c.Id).Any()).
                                                                AsEnumerable<BookModel>();

                // map the books to their additional data (ExtendedDataModel), 
                //  storing the mapping in a Dictionary<BookModel,ExtendedBookModel> 
                foreach (BookModel tkeybook in booksWithExtendedData)
                {
                    /* find the additional-data (ExtendedBookModel) for the book (BookModel) */
                    BookExtendedModel tvalueadditionaldata = HashSetExtendedDataContainer.
                                                                Where(h => h.BookId == tkeybook.Id).
                                                                FirstOrDefault();
                    
                    /* map the Book to it's additional-data */
                    BookModelToExtendedBookModelMappingDictionary.Add(tkeybook, tvalueadditionaldata);
                }
            }
        }

        private void RegexSearchBooksByFilter()
        {
            string sPattern = SearchByFilterText + "(\\s)?";
            var rx = new Regex(sPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            switch (SelectedSearchByValue)
            {
                case enumFilterValue.Author:
                    List<BookModel> tmpbooksfilteredbyauthor = AuthorsWithBooksDictionary.Where(x => rx.IsMatch(x.Key)).SelectMany(x => x.Value).ToList<BookModel>();
                    SearchResultBooks = new ObservableCollection<BookModel>(tmpbooksfilteredbyauthor);
                    break;
                case enumFilterValue.Publisher:
                    List<BookModel> tmpbooksfilteredbypublisher = BooksContainer.Where(b => rx.IsMatch(b.Publisher)).ToList<BookModel>();
                    SearchResultBooks = new ObservableCollection<BookModel>(tmpbooksfilteredbypublisher);
                    break;
                case enumFilterValue.Bookname:
                    List<BookModel> tmpbooksfilteredbyname = BooksContainer.Where(b => rx.IsMatch(b.Name)).ToList<BookModel>();
                    SearchResultBooks = new ObservableCollection<BookModel>(tmpbooksfilteredbyname);
                    break;
                case enumFilterValue.None:
                    SearchResultBooks = new ObservableCollection<BookModel>(BooksContainer);
                    break;
                default:
                    break;
            }
        }

        private void FindBooksBelongingToCurrentAuthor()
        {
            List<BookModel> tmpfilteredbooks = AuthorsWithBooksDictionary.Where(x => x.Key == CurrentAuthor).FirstOrDefault().Value?.ToList<BookModel>();

            SearchResultBooks = tmpfilteredbooks != null ? new ObservableCollection<BookModel>(tmpfilteredbooks) : new ObservableCollection<BookModel>();
        }

        private void SearchDictionaryToFindAdditionalDataBelongingToCurrentBook()
        {
            AdditionalDataForCurrentBook = CurrentBook != null ? BookModelToExtendedBookModelMappingDictionary.ContainsKey(CurrentBook) ? BookModelToExtendedBookModelMappingDictionary[CurrentBook] : new BookExtendedModel() : new BookExtendedModel();
        }

        private void GetAllAuthorsByName()
        {
            // create a grouping of all authors & the books written by each.
            IEnumerable<IGrouping<string, BookModel>> authorsandbooks = BooksContainer.GroupBy(b => b.Author)
                                                                             .AsEnumerable<IGrouping<string, BookModel>>();
            List<BookModel> tmpbookmodels = new List<BookModel>(); ;
            Authors.Add("All");

            // Loop the grouping variable which contains Names of all Authors as the key & the Book/s (BookModel) 
            // written by each as the Value. 
            foreach (IGrouping<string, BookModel> group in authorsandbooks)
            {
                Authors.Add(group.Key); // add name's of all authors to the List.

                /** Each author is a key & the books as the Value **/
                AuthorsWithBooksDictionary.Add(group.Key, group.Select(b => b).ToList<BookModel>());
            }
            // get all Books (values) from the dictionary. The Key "All" should have all the books as the Value.
            List<BookModel> tmpmodels = AuthorsWithBooksDictionary.SelectMany(x => x.Value).ToList<BookModel>();
            AuthorsWithBooksDictionary.Add("All", tmpmodels);
        }

        public void HandleExecutionMessageEvent(object sender, ErrorEventArgs args)
        {
            ExecutionMessage = args.Message;
        }
        public void OnExecutionMessage(string msg, Exception ex = null)
        {
            if (messageEvent != null)
                messageEvent(this, new ErrorEventArgs(msg,ex));
        }
    }

    public class ErrorEventArgs
    {
        public string Message { get; set; }
        public Exception Exception { get; set; } 
        public ErrorEventArgs(string msg, Exception exception = null)
        {
            this.Message = msg;
            this.Exception = exception;
        }
    }
}
