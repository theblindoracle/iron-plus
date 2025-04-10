using System;
using System.Collections.Generic;
using System.Globalization;
using IronPlus.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace IronPlus.Converters
{
    public class IsBarbellSegmentControlVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = value as List<Barbell>;

            return list != null && list.Count > 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
