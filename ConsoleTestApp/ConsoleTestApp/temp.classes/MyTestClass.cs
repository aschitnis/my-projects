using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.temp.classes
{
    public class MyClass
    {
        public int Age { get; set; }
        public string Firstname { get; set; }
        public MyClass()
        {
            Age = 41;
            Firstname = "Abhijit";
        }
    }

    public class MyTestClass
    {
        public void ChangeAge(ref MyClass myClass)
        {
            myClass.Age = 50;
        }

        public void ChangeAgeByValue(MyClass myClass)
        {
            myClass.Age = 65;
        }
        public void ChangeFirstnameByValue(MyClass myclass)
        {
            myclass.Firstname = "Sanjay";
        }

        public void ChangeFirstnameByValue(string firstname)
        {
            firstname = "Carpe Diam";
        }
    }
}
