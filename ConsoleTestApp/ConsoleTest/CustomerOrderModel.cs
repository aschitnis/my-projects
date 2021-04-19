using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class CustomerTestOrderModel
    {
        public List<CustomerOrderLine> Orderlines { get; set; }
    }

    public class CustomerOrderLine
    {
        public int LineNumber { get; set; } = -1;
        public string Name { get; set; }
    }

    public class CustomerManagement
    {
        public CustomerTestOrderModel CustomerOrder { get; set; }

        public void AddOrdersToCustomer()
        {
            CustomerTestOrderModel tmp = CustomerOrder = CustomerOrder ?? new CustomerTestOrderModel();
            CustomerOrder.Orderlines = CustomerOrder.Orderlines ?? new List<CustomerOrderLine>();
            CustomerOrder.Orderlines.Add(new CustomerOrderLine { LineNumber = 12, Name = "abhijit chitnis" });
            CustomerOrder.Orderlines.Add(new CustomerOrderLine { LineNumber = 19, Name = "elfriede krbecek" });
            CustomerOrder.Orderlines.Add(new CustomerOrderLine { LineNumber = 11, Name = "schwarzmann" });
        }

        public void UpdateOrder(out CustomerTestOrderModel order)
        {
            order = null;
            if (order == null)
            {
                order = new CustomerTestOrderModel();
                order.Orderlines = new List<CustomerOrderLine> { new CustomerOrderLine { LineNumber = 54, Name = "Mr. OUT Name" } };
            }
        }
        public void UpdateOrderByRef(ref CustomerTestOrderModel order)
        {
           CustomerTestOrderModel tmp = order = order ?? new CustomerTestOrderModel();
           if (order.Orderlines.Count > 0)
            {
                order.Orderlines[0].Name = "New Name";
            }
        }
    }
}
