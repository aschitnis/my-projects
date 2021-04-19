using schnittstelle.mysql.library.VO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.library.VO
{
    public class ValueObjectCollectionModel
    {
        public List<ValueObjectBookModel> VOBooksList { get; set; }
        public List<ValueObjectOrderModel> VOOrdersList { get; set; }
        public List<ValueObjectSellerModel> VOSellersList { get; set; }
        public List<ValueObjectCurrencyModel> VOCurrenciesList { get; set; }
        public List<ValueObjectOrderStatusModel> VOOrderStatusList { get; set; }
        public List<ValueObjectDeliveryDetailModel> VODeliveryDetailsList { get; set; }

        public ValueObjectCollectionModel()
        {
            VOBooksList             = new List<ValueObjectBookModel>();
            VOOrdersList            = new List<ValueObjectOrderModel>();
            VOSellersList           = new List<ValueObjectSellerModel>();
            VOCurrenciesList        = new List<ValueObjectCurrencyModel>();
            VOOrderStatusList       = new List<ValueObjectOrderStatusModel>();
            VODeliveryDetailsList   = new List<ValueObjectDeliveryDetailModel>();
        }
    }
}
