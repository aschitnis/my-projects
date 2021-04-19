using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Extensions
{
    public class AngestellteModel : IArbeiterModel
    {
        public Exception exception;

        #nullable enable annotations
        public string Name { get; set; }

        public void DisplayName()
        {
            Console.WriteLine($"-- Angestellte: {Name}");
        }
        public AngestellteModel()
        {
            exception = new Exception();
            Name = null;
        }
    }
}
