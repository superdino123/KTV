﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KtvStudio.Helpers.Converters
{
    public class StaffAuthorityCodeToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) return null;
            int authorityCode = int.Parse(value.ToString().Substring(value.ToString().Length - 1, 1));
            if (authorityCode == 0)
                return "操作员";
            if (authorityCode == 1)
                return "管理员";
            if (authorityCode == 2)
                return "开发人员";
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
