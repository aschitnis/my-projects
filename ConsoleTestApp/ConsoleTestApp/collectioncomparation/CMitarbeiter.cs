using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.collectioncomparation
{
    public class CMitarbeiter: IComparable<CMitarbeiter>
    {
        public string Firstname { get; set; } = "max";
        public string Lastname { get; set; } = "mustermann";
        public int Salary { get; set; } = 0;
        public int Age { get; set; } = 0;
        public CMitarbeiter()
        {

        }

        public int CompareTo(CMitarbeiter other)
        {
            if (other == null)
                return 1;
            int result = string.Compare(this.Lastname, other.Lastname);
            if (result == 0)
            {
                result = string.Compare(this.Firstname, other.Firstname);
            }
            return result;
            //throw new NotImplementedException();
        }
    }
}
