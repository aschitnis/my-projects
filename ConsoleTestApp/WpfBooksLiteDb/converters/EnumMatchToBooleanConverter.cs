using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfBooksLiteDb.converters
{
    public class EnumMatchToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            string checkValue = value.ToString();
            string paramValue = parameter.ToString();
            return checkValue.Equals(paramValue, StringComparison.CurrentCultureIgnoreCase);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            bool useValue = (bool)value;
            string targetValue = parameter.ToString();
            if (useValue)
            {
                return targetValue;
            }

            return null;
        }
    }
}
