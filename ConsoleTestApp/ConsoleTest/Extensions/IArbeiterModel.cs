using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Extensions
{
    public interface IArbeiterModel
    {
        public string Name { get; set; }
        public void DisplayName();
    }
}
