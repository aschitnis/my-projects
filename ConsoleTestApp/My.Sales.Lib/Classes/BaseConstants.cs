using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// All hard-coded values to be used in the entire project.
/// </summary>
namespace My.Sales.Classes
{
    internal class BaseConstants
    {
        private string SALESFILENAME = @"\data\500000_Sales_Records.csv";
        internal string SalesFileAbsolutePath { get; private set; }
        public BaseConstants()  
        {
            SalesFileAbsolutePath = this.GetType().Assembly.Location.Substring(0, this.GetType().Assembly.Location.LastIndexOf(@"\")) + SALESFILENAME;
        }
    }
}
