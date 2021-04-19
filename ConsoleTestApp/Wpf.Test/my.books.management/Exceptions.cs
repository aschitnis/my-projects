using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.books.management
{
    public class BookValidationException : Exception
    {
        public BookValidationException(string message) : base(message)
        {

        }
    }

    public class ViewModelMappingException : Exception
    {
        public ViewModelMappingException(string message): base(message)
        {

        }
    }
}
