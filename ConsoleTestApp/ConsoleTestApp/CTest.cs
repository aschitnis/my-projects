using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    public class CTestMain
    {
        public object ReadJson(Type resultType)
        {
            if (resultType == typeof(CTest))
            {
                return new CTest();
            }
            else
            {
                return new CTestSecond("Max Reinhard", 5023);
            }
        }
    }
    public class CTest
    {
        public string Name { get; set; }
        public int  Plz { get; set; }
        public CTest()
        {
            Name = "abhijit";
            Plz = 5020;
        }
    }
    public class CTestSecond
    {
        public string FullName { get; set; }
        public int Plz { get; set; }
        public CTestSecond()
        {

        }
        public CTestSecond(string _name, int _plz)
        {
            FullName = _name;
            Plz = _plz;
        }
    }
}
