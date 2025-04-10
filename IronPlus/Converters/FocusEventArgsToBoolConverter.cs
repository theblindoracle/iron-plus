using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace IronPlus.Converters
{
    public class FocusEventArgsToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var args = value as Syncfusion.Maui.Inputs.SfNumericEntry.FocusRequestArgs;
            return args.Focus;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
