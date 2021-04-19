using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Encryption.Extensions
{
    public static class StringExtension
    {
        public static bool HasValue(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return false;
            return true;
        }
    }
}
