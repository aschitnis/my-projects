using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfOrderBooksProject.PresentationModel
{
    public class BookPresentationModel : INotifyPropertyChanged
    {
        private int bookid;
        private string title;
        private string description;
        private string author;
        private SellerPresentationModel selectedseller;
        private List<SellerPresentationModel> sellerslist;
        private CurrencyPresentationModel selectedcurrency;
        private List<CurrencyPresentationModel> currencieslist;
        private OrderStatusPresentationModel selectedorderstatus;
        private List<OrderStatusPresentationModel> orderstatuslist;

        public int BookId
        {
            get { return bookid; }
            set { bookid = value; OnPropertyChanged("BookId"); }
        }

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("Title"); }
        }
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }

        public string Author
        {
            get { return author; }
            set { author = value; OnPropertyChanged("Author"); }
        }

        public SellerPresentationModel SelectedSeller
        {
            get { return selectedseller; }
            set { selectedseller = value; OnPropertyChanged("SelectedSeller"); }
        }

        public List<SellerPresentationModel> SellersList
        {
            get { return sellerslist; }
            set { sellerslist = value; OnPropertyChanged("SellersList"); }
        }
        public CurrencyPresentationModel SelectedCurrency
        {
            get { return selectedcurrency; }
            set { selectedcurrency = value; OnPropertyChanged("SelectedCurrency"); }
        }
        public List<CurrencyPresentationModel> CurrenciesList
        {
            get { return currencieslist; }
            set { currencieslist = value; OnPropertyChanged("CurrenciesList"); }
        }
        public OrderStatusPresentationModel SelectedOrderStatus
        {
            get { return selectedorderstatus; }
            set { selectedorderstatus = value; OnPropertyChanged("SelectedOrderStatus"); }
        }
        public List<OrderStatusPresentationModel> OrderStatusList
        {
            get { return orderstatuslist; }
            set { orderstatuslist = value; OnPropertyChanged("OrderStatusList"); }
        }

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
