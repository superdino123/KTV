using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace KtvStudio.Helpers.Converters
{
    public class ConsumeDayVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return null;
            else if (int.Parse(value.ToString()) == 0 || int.Parse(value.ToString()) == 1 || int.Parse(value.ToString()) == 2)
                return Visibility.Collapsed;
            else if (int.Parse(value.ToString()) == 3)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
