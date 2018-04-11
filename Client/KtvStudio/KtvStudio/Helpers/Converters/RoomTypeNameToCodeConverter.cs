using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KtvStudio.Helpers.Converters
{
    public class RoomTypeNameToCodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var type = value.ToString();
            if (string.IsNullOrEmpty(type))
                return null;
            if (type.Equals("小型包间"))
                return "0";
            if (type.Equals("中型包间"))
                return "1";
            if (type.Equals("大型包间"))
                return "2";
            if (type.Equals("情侣包间"))
                return "3";
            if (type.Equals("豪华包间"))
                return "4";
            if (type.Equals("商务包间"))
                return "5";
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var type = value.ToString();
            if (string.IsNullOrEmpty(type))
                return null;
            if (type.Equals("0"))
                return "小型包间";
            if (type.Equals("1"))
                return "中型包间";
            if (type.Equals("2"))
                return "大型包间";
            if (type.Equals("3"))
                return "情侣包间";
            if (type.Equals("4"))
                return "豪华包间";
            if (type.Equals("5"))
                return "商务包间";
            return null;
        }
    }
}
