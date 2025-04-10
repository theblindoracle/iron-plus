using System;
using System.Globalization;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace IronPlus.Converters
{
    public class SelectionChangedEventArgsToIndex : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var e = value as Syncfusion.Maui.Buttons.SelectionChangedEventArgs;
            // return e.Index;
             return e.NewIndex;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
