using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Extensions
{
    public class LeiharbeiterModel : IArbeiterModel
    {
        public string Name { get; set; }

        public void DisplayName()
        {
            Console.WriteLine($"-- Leiharbeiter: {Name}");
        }
    }
}
