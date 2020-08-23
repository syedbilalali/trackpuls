﻿//**************************************************************************************************
// Author       : Syed Bilal Ali
// Description  : Converts the value of width and height to its negative value
//**************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace trackpuls.Helper
{
    class InvertConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            return -(double)value;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
