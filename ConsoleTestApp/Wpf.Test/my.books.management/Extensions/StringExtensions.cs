using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.books.management.Extensions
{
    public static class StringExtensions
    {
        public static bool HasValue(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }
    }
}
