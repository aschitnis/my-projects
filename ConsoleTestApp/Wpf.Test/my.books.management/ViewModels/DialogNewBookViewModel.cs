using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wpf.Test.my.books.management.Classes;
using Wpf.Test.my.books.management.Extensions;
using Wpf.Test.my.books.management.MVVM.Dialog;

namespace Wpf.Test.my.books.management.ViewModels
{
    /// <summary>
    /// This class binds to the Selected Index which has a textual description.
    /// </summary>
    public class BookIndexModel : INotifyPropertyChanged
    {
        private string _indexdescription;
        public string IndexDescription 
        {
            get { return _indexdescription; }
            set { _indexdescription = value; OnChanged(); }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
    public class DialogNewBookViewModel : INotifyPropertyChanged, IDialogRequestClose
    {
        private ObservableCollection<BookIndexModel> _bookindexmodelslist;
        private BookIndexModel _selectedindexmodel;
        private BookModel _newbook;
        private string _newbooksavemessage;

        #region Commands
        public ICommand SaveNewBookCommand { get; }
        public ICommand SaveAllIndexesCommand { get; }
        public ICommand AddIndexCommand { get; }
        public ICommand CloseDialogCommand { get; }
        #endregion

        #region props
        public BookModel NewBook 
        {
            get { return _newbook; }
            set { _newbook = value; }
        }
        public ObservableCollection<BookIndexModel> BookIndexModelsList
        {
            get { return _bookindexmodelslist ?? (_bookindexmodelslist = new ObservableCollection<BookIndexModel>()); }
            set { _bookindexmodelslist = value; OnChanged(); }
        }
        public BookIndexModel SelectedIndexModel 
        {
            get { return _selectedindexmodel; }
            set {
                  _selectedindexmodel = value;
                  OnChanged(); 
                }
        }

        public string Message 
        {
            get { return _newbooksavemessage; }
            set { _newbooksavemessage = value;OnChanged(); }
        }
        #endregion

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #region constructors
        public DialogNewBookViewModel()
        {
            NewBook = new BookModel();
            BooksManagementStore.DuplicateIdErrorEvent += OnErrorDuplicateId;

            SaveNewBookCommand = new BookManagementCommand(SaveNewBook, CanSaveBook );
            AddIndexCommand = new BookManagementCommand(AddNewBookIndex, (a) => { return true; });
            CloseDialogCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(new DialogCloseResult(true) )));
        }

        public DialogNewBookViewModel(int nextbookid)
        {
            NewBook = new BookModel(nextbookid);
            BooksManagementStore.DuplicateIdErrorEvent += OnErrorDuplicateId;
            BooksManagementStore.ValidationErrorEvent += OnValidateError;

            SaveNewBookCommand = new BookManagementCommand(SaveNewBook, CanSaveBook);
            SaveAllIndexesCommand = new BookManagementCommand(SaveAllIndexes, CanSaveIndexes);

            AddIndexCommand = new BookManagementCommand(AddNewBookIndex, (a) => { return true; });
            CloseDialogCommand = new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(new DialogCloseResult(true))));
        }
        #endregion

        #region Event methods
        public void OnErrorDuplicateId(IModel model)
        {
            BooksManagementStore.Instance.IsNotDuplicateBookId = false;

            if (model is BookModel)
                Message = $"Book with ID: {((BookModel)model).Id} already exists in catalog";
            else if (model is BookExtendedModel)
                Message = $"Bookindex with Book-ID: {((BookExtendedModel)model).BookId} already exists in catalog";
        }
        public void OnValidateError(object sender, string _errorMsg)
        {
            Message = _errorMsg;
        }
        #endregion

        #region Functions
        private bool ValidateBook(BookModel book)
        {
            return BooksManagementStore.Instance.ValidateModel(book);
        }
        private bool ValidateBookIndex(BookExtendedModel indexModel)
        {
            return BooksManagementStore.Instance.ValidateModel(indexModel);
        }
        private bool ContainsBook(BookModel book)
        {
            return book.Exists(); 
        }
        private bool ContainsBookIndex(int indexId)
        {
            return BooksManagementStore.Instance.Contains<BookExtendedModel>(indexId);
        }
        #endregion

        #region Command Methods
        public void SaveNewBook(object parameter)
        {
            if (ValidateBook(NewBook) == true)
            {
                //save the new book to the json file.
                BooksManagementStore.Instance.SaveModel(NewBook);
            }
        }

        private bool CanSaveBook(object parameter)
        {
            return true;
        }
        
        private void AddNewBookIndex(object parameter)
        {
            BookIndexModelsList.Add(new BookIndexModel());
        }
        private bool CanAddIndex(object parameter)
        {
            return true;
        }

        private void SaveIndexes()
        {

        }

        private BookExtendedModel FindIndexModel(int bookId)
        {
           return BooksManagementStore.Instance.ExtendedModelsContainer.Where(e => e.BookId == bookId).First();
        }

        // Check whether the new index data already exists in the List<> or not.
        private bool NoUnsavedIndexDataFound()
        {
            bool isFound = true;
            // Find the index object with the book-Id.
            var indexModel = BooksManagementStore.Instance.ExtendedModelsContainer.Where(x => x.BookId == NewBook.Id).FirstOrDefault();
            
            foreach(string indexdescription in indexModel.Indexes)
            {
                if (!BookIndexModelsList.Where(m => m.IndexDescription == indexdescription).Any())
                {
                    isFound = false;
                    break;
                }
            }
            return isFound;
        }

        // if/else : a) If the index object for the new-book already exists then overwrite the data & save it.
        //           b) Otherwise create a new index entry for the new book & save it.
        private void SaveAllIndexes(object parameter)
        {
            if (BookIndexModelsList.Count == 0)
                return;

            if (ContainsBookIndex(NewBook.Id))
            {
                if (NoUnsavedIndexDataFound()) // if the new index data has already been saved then there is nothing to be done.
                    return;

                var indexModelObjectForNewBook = FindIndexModel(NewBook.Id);
                indexModelObjectForNewBook.Indexes = BookIndexModelsList.Count > 0 ? BookIndexModelsList.Select(x => x.IndexDescription).ToList<string>() : new List<string>();
                
                BooksManagementStore.Instance.SaveModels(typeof(BookExtendedModel));
            }
            else
            {
                BookExtendedModel bookindexModel = new BookExtendedModel() 
                { 
                    BookId = NewBook.Id,
                    Indexes = new List<string>(BookIndexModelsList.Select(x => x.IndexDescription).ToList<string>() )
                };

                BookExtendedModel clonedIndexModel = bookindexModel.Clone(); 
                BooksManagementStore.Instance.SaveModel(clonedIndexModel);
            }

            // ** rewrite all indexes to json 
            // 1) read/reload all index data
            // 2) add the new index data to container list
            // 3) save the entire list to json.

            BookExtendedModel indexmodel = new BookExtendedModel();
            indexmodel.BookId = NewBook.Id;
            indexmodel.Indexes = BookIndexModelsList.Select(x => x.IndexDescription).ToList();
            
            if (indexmodel.Exists())
                indexmodel.Indexes.Clear();
            //if (BooksManagementStore.Instance.ContainsModelById<BookExtendedModel>(NewBook.Id))



            BooksManagementStore.Instance.SaveModels(typeof(BookExtendedModel));
        }
        private bool CanSaveIndexes(object parameter)
        {
            return true;
        }

        #endregion

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
