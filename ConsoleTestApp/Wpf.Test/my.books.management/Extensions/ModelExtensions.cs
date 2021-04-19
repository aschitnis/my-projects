using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.books.management.Extensions
{
    public static class ModelExtensions
    {
        public static bool Exists(this IModel model)
        {
            return BooksManagementStore.Instance.Contains(model);
        }
    }
}
