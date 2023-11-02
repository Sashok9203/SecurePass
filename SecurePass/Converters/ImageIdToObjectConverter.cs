using SecurePass.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SecurePass.Converters
{
    internal class ImageIdToObjectConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture) => ImageLoader.GetById((int)value);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
