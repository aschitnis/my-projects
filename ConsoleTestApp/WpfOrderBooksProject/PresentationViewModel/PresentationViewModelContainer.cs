using schnittstelle.mysql.library.BUS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfOrderBooksProject.PresentationModel;

namespace WpfOrderBooksProject.PresentationViewModel 
{
    public class PresentationViewModelContainer : INotifyPropertyChanged
    {
        private DataBUS databusobject; 
        private ObservableCollection<BookPresentationModel> booksviewmodellist;

        public DataBUS DataBusObject
        {
            get { return databusobject; }
            set { databusobject = value; }
        }

        public ObservableCollection<BookPresentationModel> BooksViewModelList
        {
            get {
                    if (booksviewmodellist == null)
                        { booksviewmodellist = new ObservableCollection<BookPresentationModel>(); }
                    return booksviewmodellist; }
            set { booksviewmodellist = value; OnPropertyChanged("BooksViewModelList"); }
        }

        #region constructors
        public PresentationViewModelContainer()
        {
            DataBusObject = new DataBUS();
            BooksViewModelList = new ObservableCollection<BookPresentationModel>();

           var bks = DataBusObject.GetAllBooks();
        }
        #endregion
        #region PropertyChanged Notification
        public event PropertyChangedEventHandler PropertyChanged;
        // public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName) );

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

    }
}
