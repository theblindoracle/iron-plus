using System;
using System.Collections.Generic;
using System.Globalization;
using IronPlus.Models;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.Buttons;

namespace IronPlus.Converters
{
    public class BarbellsToSfSegmentItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var barbells = value as List<Barbell>;

            if (barbells == null)
            {
                return null;
            }

            var segments = new List<SfSegmentItem>();
            foreach (Barbell barbell in barbells)
            {
                segments.Add(new SfSegmentItem() { Text = barbell.Name });
            }

            return segments;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
