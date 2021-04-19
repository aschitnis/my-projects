using schnittstelle.mysql.library.DAO;
using schnittstelle.mysql.library.VO;
using schnittstelle.mysql.library.VO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.library.BUS
{
    public class DataBUS
    {
        private DataModelDAO _dataDAO { get; set; }
        public DataBUS()
        {
            _dataDAO = new DataModelDAO();
        }

        public List<ValueObjectBookModel> GetAllBooks()
        {
            List<ValueObjectBookModel> listBooks = new List<ValueObjectBookModel>();
            DataTable tBooks = new DataTable();

            tBooks = _dataDAO.SelectQuery(DataModelDAO.Tablenames.BOOKS);
            foreach(DataRow booksRow in tBooks.Rows)
            {
                ValueObjectBookModel book = new ValueObjectBookModel();
                book.BookId      =  Int32.Parse(booksRow["book_id"].ToString());
                book.Title       =  booksRow["title"].ToString();

                book.Description = booksRow["description"].ToString();   /* Die NULL-vorhanden Prüfung ist in DbConnection Klasse bereits vollgezogen worden. */
                book.Author      = booksRow["author"].ToString();
                listBooks.Add(book);
            }
            return listBooks;
        }

        public List<ValueObjectOrderModel> getAllOrders()
        {
            List<ValueObjectOrderModel> listOrders = new List<ValueObjectOrderModel>();
            DataTable tOrders = new DataTable();

            tOrders = _dataDAO.SelectQuery(DataModelDAO.Tablenames.ORDERS);
            foreach (DataRow dr in tOrders.Rows)
            {
                ValueObjectOrderModel order = new ValueObjectOrderModel();
                order.OrderId = Int32.Parse(dr["order_id"].ToString());

                listOrders.Add(order);
            }
            return listOrders;
        }

        public List<ValueObjectSellerModel> getAllSellers()
        {
            List<ValueObjectSellerModel> listSellers = new List<ValueObjectSellerModel>();
            DataTable tSellers = new DataTable();

            tSellers = _dataDAO.SelectQuery(DataModelDAO.Tablenames.SELLERS);
            foreach (DataRow dr in tSellers.Rows)
            {
                ValueObjectSellerModel seller = new ValueObjectSellerModel();
                seller.SellerId = Int32.Parse(dr["seller_id"].ToString());

                listSellers.Add(seller);
            }
            return listSellers;
        }

        public List<ValueObjectBookSellerModel> getAllBookSellers()
        {
            List<ValueObjectBookSellerModel> listBookSellers = new List<ValueObjectBookSellerModel>();
            DataTable tBookSellers = new DataTable();

            tBookSellers = _dataDAO.SelectQuery(DataModelDAO.Tablenames.BOOKSELLERS);
            foreach (DataRow dr in tBookSellers.Rows)
            {
                ValueObjectBookSellerModel bookseller = new ValueObjectBookSellerModel();
                bookseller.SellerId = Int32.Parse(dr["bookseller_id"].ToString());

                listBookSellers.Add(bookseller);
            }
            return listBookSellers;
        }

        public List<ValueObjectCurrencyModel> getAllCurrencies()
        {
            List<ValueObjectCurrencyModel> listCurrencies = new List<ValueObjectCurrencyModel>();
            DataTable tCurrencies = new DataTable();

            tCurrencies = _dataDAO.SelectQuery(DataModelDAO.Tablenames.CURRENCIES);
            foreach (DataRow dr in tCurrencies.Rows)
            {
                ValueObjectCurrencyModel currency = new ValueObjectCurrencyModel();
                currency.CurrencyId = Int32.Parse(dr["currency_id"].ToString());

                listCurrencies.Add(currency);
            }
            return listCurrencies;
        }

        public List<ValueObjectDeliveryDetailModel> getAllDeliveries()
        {
            List<ValueObjectDeliveryDetailModel> listDeliveries = new List<ValueObjectDeliveryDetailModel>();
            DataTable tDeliveries = new DataTable();

            tDeliveries = _dataDAO.SelectQuery(DataModelDAO.Tablenames.DELIVERYDETAILS);
            foreach (DataRow dr in tDeliveries.Rows)
            {
                ValueObjectDeliveryDetailModel delivery = new ValueObjectDeliveryDetailModel();
                delivery.DeliveryId = Int32.Parse(dr["delivery_id"].ToString());

                listDeliveries.Add(delivery);
            }
            return listDeliveries;
        }
    }
}
