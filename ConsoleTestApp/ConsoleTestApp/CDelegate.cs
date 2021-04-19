using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.Test.Delegates
{
    public class CDelegate
    {
        public Func<int,int,int> FuncAdd;
        public Func<int, int, int> FuncSub;
        public Func<int, int, int> FuncMul;
        

        public CDelegate()
        {
            FuncAdd = delegate (int x, int y) { return x + y; }; 
            FuncSub = SubtractMethod;
            FuncMul = ((x, y) => { return x * y; });
            //
        }

        public int SubtractMethod(int x, int y) { return x - y; }

    }

    public class CDelegateCall
    {
        public static void CallDelegate(Func<int,int,int> f, int x, int y)
        {
            int result = f(x, y);
            Console.WriteLine(result);    
        }
    }
}
