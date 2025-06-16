using System;
using System.Collections.Generic;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace IronPlus.Controls
{
    public partial class Divider : ContentView
    {
        public Divider()
        {
            InitializeComponent();
            Inset = 0d;
        }

        public static readonly BindableProperty InsetProperty = BindableProperty.Create(nameof(Inset),
                                                                                        typeof(double),
                                                                                        typeof(Divider),
                                                                                        24d,
                                                                                        propertyChanged: InsetPropertyChanged);

        private static void InsetPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as Divider;
            control.Margin = new Thickness((double)newValue, 0);
        }

        public double Inset
        {
            get => (double)GetValue(InsetProperty);
            set => SetValue(InsetProperty, value);
        }


        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color),
                                                                                               typeof(Color),
                                                                                               typeof(Divider),
                                                                                               Colors.Silver,
                                                                                               propertyChanged: ColorPropertyChanged);

        private static void ColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as Divider).boxView.Color = (Color)newValue;
        }

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
    }
}
