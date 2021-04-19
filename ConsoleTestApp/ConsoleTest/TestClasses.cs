using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class JsonTest
    {
        public string Name { get; set; }
    }
    public class TestModel : JsonTest
    {
        public TestModel() {  }
        public TestModel(JsonTest testmodel)
        {
            Name = testmodel.Name;
        }
    }
    public class TestManagement
    {
        
    }
}
