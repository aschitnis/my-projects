using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Test.my.books.management.Classes;
using Wpf.Test.my.books.management.Extensions;

namespace Wpf.Test.my.books.management
{
    internal sealed class BooksManagementStore
    {
        private static BooksManagementStore instance = new BooksManagementStore();
        public static BooksManagementStore Instance => instance;

        #region Events
        public delegate void DuplicateIdErrorHandler(IModel model);
        public static event DuplicateIdErrorHandler DuplicateIdErrorEvent;
        public static event EventHandler<string> ValidationErrorEvent;
        public event EventHandler<BookModel> OnNewBookSaved;
        #endregion

        #region props
        internal bool IsNotDuplicateBookId { get; set; } = true;
        private IModel model { get; set; } 
        public List<JsonBookModel> JsonBookModels { get; private set; } = new List<JsonBookModel>();
        public List<JsonBookExtendedModel> JsonBookExtendedModels { get; private set; } = new List<JsonBookExtendedModel>();
        public List<BookModel> BookModelsContainer { get; private set; } = new List<BookModel>();
        public List<BookExtendedModel> ExtendedModelsContainer { get; private set; } = new List<BookExtendedModel>();
        private string BooksFilePath { get { return PathManager.FILE_Books_Plaintext; }  }
        private string IndexesFilePath { get { return PathManager.FILE_Index_Plaintext; } }
        #endregion
        private BooksManagementStore()
        {

        }

        private Exception ReadAllBooksFromJsonFile()
        {
            BookModelsContainer.Clear();
            string json = File.ReadAllText(BooksFilePath);
            try 
            {
                var result = JsonConvert.DeserializeObject(json, typeof(List<JsonBookModel>));
                if (!String.IsNullOrEmpty(json))
                    JsonBookModels = (List<JsonBookModel>)result;

                // convert typeof(List<JsonBookModel>) to typeof(List<BookModel>)
                if (JsonBookModels.Count > 0)
                {
                    BookModelsContainer = JsonBookModels.Select(x => new BookModel(x))
                                                        .AsEnumerable<BookModel>()
                                                        .ToList();
                }
                else
                {
                   return new Exception("No books data was found in the [books.json] file");
                }

                return null;
            }
            catch(Exception e)
            {
                return e;
            }
        }

        //The data is read from "extendedInformation.json" & stored in the List<**>. 
        internal Exception ReadAllIndexesFromJsonFile()
        {
            ExtendedModelsContainer.Clear();
            string json = File.ReadAllText(IndexesFilePath);
            try
            {
                var result = JsonConvert.DeserializeObject(json, typeof(List<JsonBookExtendedModel>));
                if (!String.IsNullOrEmpty(json))
                    JsonBookExtendedModels = (List<JsonBookExtendedModel>)result;

                // convert typeof(List<JsonBookExtendedModel>) to typeof(List<BookExtendedModel>)
                // note the good use of a copy-constructor
                if (JsonBookExtendedModels.Count > 0)
                {
                    ExtendedModelsContainer = JsonBookExtendedModels.Select( x => new BookExtendedModel(x) ).ToList<BookExtendedModel>();
                }
                else
                {
                    // exception: no additional data on books was found !
                    return new Exception("No additional data about books was found in the [extendedinformation.json] file");
                }

                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public bool ValidateModel(IModel model)
        {
            IsNotDuplicateBookId = true;
            bool hasError = false;

            if (model is BookModel)
            {
                var book = (BookModel)model;
                
                if (book.Exists()) // checks the Id
                {
                    DuplicateIdErrorEvent?.Invoke(book); // error-message to be displayed in the view.
                }
                else
                {
                    if (!book.Author.HasValue())
                    {
                        hasError = true;
                        ValidationErrorEvent.Invoke(null, $"Error in book with ID {book.Id}. Authorname is mandatory.");
                    }
                    else if (!book.Name.HasValue())
                    {
                        hasError = true;
                        ValidationErrorEvent.Invoke(null, $"Error in book with ID {book.Id}. Bookname is mandatory.");
                    }
                    else if (!book.Language.HasValue())
                    {
                        hasError = true;
                        ValidationErrorEvent.Invoke(null, $"Error in book with ID {book.Id}. Language is mandatory.");
                    }
                }
            }
            else if (model is BookExtendedModel)
            {
                var indexmodel = (BookExtendedModel)model;
                if (indexmodel.BookId == 0)
                {
                    ValidationErrorEvent.Invoke(null, $"Error in Bookindex. BookID for Bookindex cannot be Zero.");
                }
            }
            return hasError;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">Is either of type(BookModel) or type(BookExtendedModel)</param>
        /// <returns></returns>
        internal void SaveModel(IModel model)
        {
            string json;
            if (model is BookModel)
            {
                var book = (BookModel)model;
                if (!book.Exists())
                    BookModelsContainer.Add(book);

                JsonDataManager.SerializeModel(BookModelsContainer, out json);
                File.WriteAllText(BooksFilePath, json, System.Text.Encoding.Unicode);
            }
            else if (model is BookExtendedModel)
            {
                var bookextendedmodel = (BookExtendedModel)model;
                if (!bookextendedmodel.Exists())
                    ExtendedModelsContainer.Add(bookextendedmodel);

                JsonDataManager.SerializeModel(ExtendedModelsContainer, out json);
                File.WriteAllText(IndexesFilePath, json, System.Text.Encoding.Unicode);
            }
        }
        internal void SaveModels(Type t)
        {
            string json;
            if (t == typeof(BookModel))
            {
                JsonDataManager.SerializeModel(BookModelsContainer, out json);
                File.WriteAllText(IndexesFilePath, json, System.Text.Encoding.Unicode);
            }
            else if (t == typeof(BookExtendedModel))
            {
                JsonDataManager.SerializeModel(ExtendedModelsContainer, out json);
                File.WriteAllText(IndexesFilePath, json, System.Text.Encoding.Unicode);
            }
        }
        public Exception InitializeBooksAndAdditionalData()
        {
            Exception ex = null;

            ex = ReadAllBooksFromJsonFile();
            if (ex != null)
                return ex;
            ex = ReadAllIndexesFromJsonFile();
            return ex;
        }

        private bool ContainsModelById<T>(int bookid) where T : IModel
        {
            if (typeof(T) == typeof(BookModel))
                return BookModelsContainer.Where(b => b.Id == bookid)?.FirstOrDefault() == null ? false : true;
            else
                return ExtendedModelsContainer.Where(e => e.BookId == bookid).FirstOrDefault() == null ? false : true;
        }

        public bool Contains(IModel _model)
        {
            bool result = false;
            model = _model;

            if (model.GetType() == typeof(BookModel))
            {
                int id = (model as BookModel).Id;
                result = ContainsModelById<BookModel>(id);
            }
            else
            {
                int id = (model as BookExtendedModel).BookId;
                result = ContainsModelById<BookExtendedModel>(id);
            }
            return result;
        }

        public bool Contains<T>(int bookid) where T : class, new()
        {
            T entry = new T();

            if (entry.GetType() == typeof(BookModel))
                return BookModelsContainer.Where(b => b.Id == bookid)?.FirstOrDefault() == null ? false : true;
            else
                return ExtendedModelsContainer.Where(e => e.BookId == bookid).FirstOrDefault() == null ? false : true;
        }
    }
}
