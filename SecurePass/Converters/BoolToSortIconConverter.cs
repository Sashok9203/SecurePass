using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SecurePass.Converters
{
    internal class BoolToSortIconConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageConverter imageConverter = new();
            return  (bool)value ? imageConverter.ConvertTo(PlusIconResource.sor_desc, typeof(byte[])) as byte[] :
                imageConverter.ConvertTo(PlusIconResource.sort , typeof(byte[])) as byte[];
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
