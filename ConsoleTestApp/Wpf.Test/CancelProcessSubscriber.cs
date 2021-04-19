using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test
{
    public class CancelProcessSubscriber
    {
        public string SubscriberMessage { get; set; }
        public void CancelPrimeNumberCalculation(object sender, CancelPrimeNumberCalculationEventArgs args)
        {
            SubscriberMessage = "cancelled prime calculation-Result: " + args.Message;
        }
        public void CancelProcessMethod(object sender, CancelEventArgs args)
        {
            SubscriberMessage = "cancelled Task......." + args.Zahl;
        }
    }
}
