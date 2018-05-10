using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KtvStudio.Helpers.Converters
{
    public class SingerShowPageSizeToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return null;
            if (int.Parse(value.ToString()) == 20)
                return 0;
            if (int.Parse(value.ToString()) == 50)
                return 1;
            if (int.Parse(value.ToString()) == 200)
                return 2;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return null;
            if (int.Parse(value.ToString()) == 0)
                return 20;
            if (int.Parse(value.ToString()) == 1)
                return 50;
            if (int.Parse(value.ToString()) == 2)
                return 200;
            return 20;
        }
    }
}
