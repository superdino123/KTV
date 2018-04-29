using KtvStudio.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KtvStudio.Helpers.Converters
{
    public class LanguageTypeCodeToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string code = value.ToString();
            if (string.IsNullOrEmpty(code))
                return string.Empty;
            return (LanguageTypeEnum)Enum.Parse(typeof(LanguageTypeEnum), code, true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
