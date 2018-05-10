using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KtvMusic.Helpers.Converters
{
    public class MVUploadUrlToLocationUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) return null;
            string name = value.ToString().Substring(value.ToString().LastIndexOf('/') + 1);
            string locationUrl = ConfigurationManager.AppSettings["MVLocationUrl"];
            return locationUrl + name;
    }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
