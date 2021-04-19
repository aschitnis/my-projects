using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    public class Currency
    {
        public uint Euro;
        public ushort Cents;

        public Currency(uint euro, ushort cents)
        {
            this.Euro = euro;
            this.Cents = cents;
        }

        public override string ToString()
        {
            return string.Format("${0}.{1,2:00}",Euro,Cents);
        }

        public static explicit operator Currency(float value)
        {
            uint euros = (uint)value;
            ushort cent = (ushort)((value-euros)*100);
            return new Currency(euros, cent);
        }

        public static implicit operator Currency(uint value)
        {
            return new Currency(value, 0);
        }
    }
}
