using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfBooksLiteDb.Database.Entities;
using WpfBooksLiteDb.Database.Entities.VM;
using WpfBooksLiteDb.Entities.ViewModels;

namespace WpfBooksLiteDb
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DbEntitiesSingletonVM EntitiesViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            EntitiesViewModel = DbEntitiesSingletonVM.GetInstance();
            this.DataContext = EntitiesViewModel;

            //using (var db = new LiteDatabase(@"C:\Projekte\ConsoleTestApp\WpfBooksLiteDb\BookTest.db"))
            //{
            //    /*****
            //    var books = db.GetCollection<BookEntity>("books");
            //    BookEntity result = books.Find(x => x.bkId == 1).FirstOrDefault();
            //    if (result != null)
            //    {
            //        result.BookmarkEntities.Add(new BookmarkEntity { bmId = 13, fkbkId = 1, beschreibung = "On breath", page = 10 });
            //    }
            //    ****/
            //    //var book = new BookEntity
            //    //{
            //    //    bkId = 1,
            //    //    name = "Voice of the Self",
            //    //    BookmarkEntities = new List<BookmarkEntity>
            //    //                                { new BookmarkEntity { bmId = 12, fkbkId = 1, beschreibung = "On madness", page = 8 } }
            //    //};
            //    //books.Insert(book);
            //}
        }

        private void SaveToDbClick(object sender, RoutedEventArgs e)
        {
            //if ((sender as Button).Name.Equals("btnSave"))
            //{
            //    EntitiesViewModel.NewBookViewModel.SaveNewBookEntityToDatabase();
            //}
                
            //else if ((sender as Button).Name.Equals("btnSaveBookmarkToDb"))
            //    EntitiesViewModel.NewBookmarkEntityVM.SaveBookmarksToDatabase();

        }

        private void btnCreateNewBook_Click(object sender, RoutedEventArgs e)
        {
            EntitiesViewModel.NewBookViewModel.CreateNewBookEntity();
        }

         private void btnAddTagToBook_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnNewBook_Click(object sender, RoutedEventArgs e)
        {
            tbBooks.IsSelected = true;
            grdNewBuch.IsEnabled = true;

            tbNewBookmark.IsSelected = false;
            tbNewBookmark.IsEnabled = false;
            tbBooks.IsEnabled = true;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            if ( b.Name == "btnBookmark_Click")
            {
                tbNewBookmark.IsSelected = true;
                EntitiesViewModel.NewBookmarkEntityVM.DisplayAllBooksFromDatabase();

                this.DataContext = EntitiesViewModel.NewBookmarkEntityVM;

                grdNewBuch.IsEnabled = false;
                tbBooks.IsSelected = false;
                tbNewBookmark.IsEnabled = true;
                tbBooks.IsEnabled = false;
            }
            else if (b.Name == "btn_ShowAllBooks")
            {
                tbBooks.IsEnabled = true;
                tbBooks.IsSelected = true;
                grdNewBuch.IsEnabled = true;
            }
        }

        private void btnSaveTag_Click(object sender, RoutedEventArgs e)
        {
            /***
             * a) check if book has a ID > 0
             * b) check if this Book(titel) already exists in the database.
             *      If YES, then Update(overwrite) book data.
             *      Before updating check the following:
             *       i)   
             * ***/
        }

        private void btnNewTag_Click(object sender, RoutedEventArgs e)
        {
            EntitiesViewModel.NewBookmarkEntityVM.CreateNewBookmark();

            //tbBookmarkId.IsEnabled = true;
            //tbBookmarkPageNr.IsEnabled = true;
            //tbBookmarkDescription.IsEnabled = true;
        }

        private void btnAddOrUpdateToDataGrid_Click(object sender, RoutedEventArgs e)
        {
            EntitiesViewModel.NewBookmarkEntityVM.AddOrUpdateBookmarkToBookmarkList();
        }
        private void cbSearchbyName_Checked(object sender, RoutedEventArgs e)
        {
            cbSearchbyAuthor.IsChecked = false;
        }
        private void cbSearchbyAuthor_Checked(object sender, RoutedEventArgs e)
        {
            cbSearchbyName.IsChecked = false;
        }
    }
}
