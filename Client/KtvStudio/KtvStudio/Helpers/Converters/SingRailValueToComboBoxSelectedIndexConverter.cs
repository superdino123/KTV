﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KtvStudio.Helpers.Converters
{
    public class SingRailValueToComboBoxSelectedIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return null;
            if (value.ToString().Equals("-1"))
                return 0;
            if (value.ToString().Equals("1"))
                return 1;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return null;
            if (int.Parse(value.ToString()) == 0)
                return "-1";
            if (int.Parse(value.ToString()) == 1)
                return "1";
            return null;
        }
    }
}
