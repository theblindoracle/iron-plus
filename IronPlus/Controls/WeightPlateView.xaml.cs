using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui;

namespace IronPlus.Controls
{
    public partial class WeightPlateView : ContentView
    {
        public WeightPlateView()
        {
            InitializeComponent();
        }


        public double Weight
        {
            get => (double)GetValue(WeightProperty);
            set => SetValue(WeightProperty, value);
        }

        public static readonly BindableProperty WeightProperty = BindableProperty.Create(
                                                                    propertyName: nameof(Weight),
                                                                    returnType: typeof(double),
                                                                    declaringType: typeof(WeightPlateView),
                                                                    defaultValue: 0.0,
                                                                    propertyChanged: (b, o, n) => ((WeightPlateView)b).LoadBar());


        public int BarbellWeight
        {
            get => (int)GetValue(BarbellWeightProperty);
            set => SetValue(BarbellWeightProperty, value);
        }

        public static readonly BindableProperty BarbellWeightProperty = BindableProperty.Create(
                                                                            propertyName: nameof(BarbellWeight),
                                                                            returnType: typeof(int),
                                                                            declaringType: typeof(WeightPlateView),
                                                                            defaultValue: 0,
                                                                            propertyChanged: (b, o, n) => ((WeightPlateView)b).LoadBar());

        public bool IsKilograms
        {
            get => (bool)GetValue(IsKilogramsProperty);
            set => SetValue(IsKilogramsProperty, value);
        }

        public static readonly BindableProperty IsKilogramsProperty = BindableProperty.Create(
                                                                    propertyName: nameof(IsKilograms),
                                                                    returnType: typeof(bool),
                                                                    declaringType: typeof(WeightPlateView),
                                                                    defaultValue: true,
                                                                    propertyChanged: (b, o, n) => ((WeightPlateView)b).LoadBar());


        public bool IsUsingCompetitionCollar
        {
            get => (bool)GetValue(IsUsingCompetitionCollarProperty);
            set => SetValue(IsUsingCompetitionCollarProperty, value);
        }

        public static readonly BindableProperty IsUsingCompetitionCollarProperty = BindableProperty.Create(
                                                                          propertyName: nameof(IsUsingCompetitionCollar),
                                                                          returnType: typeof(bool),
                                                                          declaringType: typeof(WeightPlateView),
                                                                          defaultValue: true,
                                                                          propertyChanged: (b, o, n) => ((WeightPlateView)b).LoadBar());



        void LoadBar()
        {
            var plates = CalculateNeededPlates();
            CreatePlateView(plates);
        }

        List<Plate> CalculateNeededPlates()
        {
            double weight = Weight;
            List<Plate> plates = new List<Plate>();

            if (IsKilograms)
            {
                if (IsUsingCompetitionCollar)
                {
                    weight -= BarbellWeight + 5;
                }
                else
                {
                    weight -= BarbellWeight;
                }

                while (weight > 0)
                {
                    if (weight >= 50)
                    {
                        plates.Add(TwentyFiveKGPlate);
                        weight -= 50;
                    }
                    else if (weight >= 40)
                    {
                        plates.Add(TwentyKGPlate);
                        weight -= 40;
                    }
                    else if (weight >= 30)
                    {
                        plates.Add(FifteenKGPlate);
                        weight -= 30;
                    }
                    else if (weight >= 20)
                    {
                        plates.Add(TenKGPlate);
                        weight -= 20;
                    }
                    else if (weight >= 10)
                    {
                        plates.Add(FiveKGPlate);
                        weight -= 10;
                    }
                    else if (weight >= 5)
                    {
                        plates.Add(TwoAndAHalfKGPlate);
                        weight -= 5;
                    }
                    else if (weight >= 4)
                    {
                        plates.Add(TwoKGPlate);
                        weight -= 4;
                    }
                    else if (weight >= 3)
                    {
                        plates.Add(OneAndAHalfKGPlate);
                        weight -= 3;
                    }
                    else if (weight >= 2.5)
                    {
                        plates.Add(OneAndAQuarterKGPlate);
                        weight -= 2.5;
                    }
                    else if (weight >= 2)
                    {
                        plates.Add(OneKGPlate);
                        weight -= 2;
                    }
                    else if (weight >= 1)
                    {
                        plates.Add(HalfKGPlate);
                        weight -= 1;
                    }
                    else if (weight >= 0.5)
                    {
                        plates.Add(QuarterKGPlate);
                        weight -= 0.5;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                if (IsUsingCompetitionCollar)
                {
                    weight -= BarbellWeight + 10;
                }
                else
                {
                    weight -= BarbellWeight;
                }

                while (weight > 0)
                {
                    if (weight >= 90)
                    {
                        plates.Add(FourtyFiveLBPlate);
                        weight -= 90;
                    }
                    else if (weight >= 50)
                    {
                        plates.Add(TwentyFiveLBPlate);
                        weight -= 50;
                    }
                    else if (weight >= 20)
                    {
                        plates.Add(TenLBPlate);
                        weight -= 20;
                    }
                    else if (weight >= 10)
                    {
                        plates.Add(FiveLBPlate);
                        weight -= 10;
                    }
                    else if (weight >= 5)
                    {
                        plates.Add(TwoAndAHalfLBPlate);
                        weight -= 5;
                    }
                    else if (weight >= 2.5)
                    {
                        plates.Add(OneAndAQuarterLBPlate);
                        weight -= 2.5;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (IsUsingCompetitionCollar && IsKilograms && Weight != 20)
            {
                plates.Add(CompetitionCollar);
            }
            else if (IsUsingCompetitionCollar && !IsKilograms && Weight != 45)
            {
                plates.Add(CompetitionCollar);
            }

            return plates;
        }

        readonly int plateWidth = 40;

        void CreatePlateView(List<Plate> plates)
        {
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();

            List<View> plateViews = new List<View>();

            for (int index = 0; index < plates.Count; index++)
            {
                var plate = plates[index];

                if (plate.Weight.Length > 3 && plate.PlateType != PlateType.Collar)
                {
                    grid.ColumnDefinitions.Insert(index, new ColumnDefinition() { Width = GridLength.Auto });
                }
                else
                {
                    grid.ColumnDefinitions.Insert(index, new ColumnDefinition() { Width = GridLength.Star });
                }

                int plateNumber = 0;
                if ((plate.Weight == "25" && plate.PlateType == PlateType.Kilogram) ||
                    (plate.Weight == "45" && plate.PlateType == PlateType.Pound))
                    plateNumber = index + 1;

                if (plate.PlateType != PlateType.Collar)
                {
                    plateViews.Add(CreatePlateView(plate, plateNumber));

                }
                else
                {
                    var collarView = new CollarView(plate);
                    plateViews.Add(collarView);
                }

            }

            var spacers = 8 - plates.Count;


            for (int plateCount = spacers; plateCount > 0; plateCount--)
            {
                plateViews.Add(Spacer);
            }

            for (int column = 0; column < plateViews.Count; column++)
            {
                var view = plateViews[column];
                if (view is CollarView collar)
                {
                    grid.Children.Add(collar, column, column + 2, 0, 1);
                    
                }
                else
                {
                    grid.Children.Add(plateViews[column], column, 0);

                }

            }
        }

        Frame CreatePlateView(Plate plate, int plateNumber)
        {

            var label = new Label()
            {
                TextColor = Colors.Black,
                VerticalOptions = LayoutOptions.Center,
                Text = plate.Weight,
                FontSize = 18,
                HorizontalTextAlignment = TextAlignment.Center,
                Style = (Style)Application.Current.Resources["BaseLabelStyle"],
                LineBreakMode = LineBreakMode.WordWrap,
                MaxLines = 1,
                WidthRequest = plateWidth,


            };


            var grid = new Grid()
            {
                Children = { label },
                Padding = new Thickness(0, 4),
            };

            if (plateNumber != 0)
            {
                var plateNumberLabel = new Label()
                {
                    TextColor = Colors.Black,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.End,
                    Text = plateNumber.ToString(),
                    FontSize = 18,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Style = (Style)Application.Current.Resources["BaseLabelStyle"],
                    LineBreakMode = LineBreakMode.NoWrap
                };

                grid.Children.Add(plateNumberLabel);
            }

            var frame = new Frame()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HasShadow = false,
                BorderColor = Colors.Black,
                BackgroundColor = plate.BackgroundColorLight,
                CornerRadius = 5,
                Padding = 0,
                HeightRequest = plate.Height,
                WidthRequest = plateWidth,
                Content = grid,
            };



            if (plate.BackgroundColorDark != Color.Default)
                frame.SetAppThemeColor(Frame.BackgroundColorProperty, plate.BackgroundColorLight, plate.BackgroundColorDark);


            return frame;
        }



        class CollarView : Grid
        {
            int collarTextWidth = 70;
            int plateWidth = 40;

            Frame frame;
            Label textLabel;

            public CollarView(Plate collar)
            {
                textLabel = new Label()
                {
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Text = "Collar",
                    FontSize = 18,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Style = (Style)Application.Current.Resources["BaseLabelStyle"],
                    LineBreakMode = LineBreakMode.NoWrap,
                    WidthRequest = collarTextWidth,
                    Rotation = -90
                };



                frame = new Frame()
                {
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center,
                    HasShadow = false,
                    BorderColor = Colors.Black,
                    BackgroundColor = collar.BackgroundColorLight,
                    CornerRadius = 5,
                    Padding = 0,
                    HeightRequest = collar.Height,
                    WidthRequest = plateWidth
                };


                if (collar.BackgroundColorDark != Color.Default)
                    frame.SetAppThemeColor(Frame.BackgroundColorProperty, collar.BackgroundColorLight, collar.BackgroundColorDark);


                Children.Add(frame);
                Children.Add(textLabel);

                PropertyChanged += Grid_PropertyChanged;
            }

            private void Grid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (e.PropertyName == WidthProperty.PropertyName)
                {
                    textLabel.TranslationX = (plateWidth - Width) / 2;
                }
            }
        }
        class Plate
        {
            public string Weight { get; set; }
            public int Height { get; set; }
            public Color BackgroundColorLight { get; set; }
            public Color BackgroundColorDark { get; set; }
            public PlateType PlateType { get; set; }
        }

        Plate TwentyFiveKGPlate => new Plate() { Weight = "25", Height = 240, BackgroundColorLight = Color.FromHex("#ef5350"), BackgroundColorDark = Color.FromHex("#e57373"), PlateType = PlateType.Kilogram };
        Plate TwentyKGPlate => new Plate() { Weight = "20", Height = 240, BackgroundColorLight = Color.FromHex("#42a5f5"), BackgroundColorDark = Color.FromHex("#64b5f6"), PlateType = PlateType.Kilogram };
        Plate FifteenKGPlate => new Plate() { Weight = "15", Height = 200, BackgroundColorLight = Color.FromHex("#ffeb3b"), BackgroundColorDark = Color.FromHex("#ffee58"), PlateType = PlateType.Kilogram };
        Plate TenKGPlate => new Plate() { Weight = "10", Height = 160, BackgroundColorLight = Color.FromHex("#4caf50"), BackgroundColorDark = Color.FromHex("#66bb6a"), PlateType = PlateType.Kilogram };
        Plate FiveKGPlate => new Plate() { Weight = "5", Height = 120, BackgroundColorLight = Colors.White, BackgroundColorDark = Color.FromHex("#fafafa"), PlateType = PlateType.Kilogram };
        Plate TwoAndAHalfKGPlate => new Plate() { Weight = "2.5", Height = 100, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Kilogram };
        Plate TwoKGPlate => new Plate() { Weight = "2", Height = 100, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Kilogram };
        Plate OneAndAHalfKGPlate => new Plate() { Weight = "1.5", Height = 85, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Kilogram };
        Plate OneAndAQuarterKGPlate => new Plate() { Weight = "1.25", Height = 85, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Kilogram };
        Plate OneKGPlate => new Plate() { Weight = "1", Height = 85, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Kilogram };
        Plate HalfKGPlate => new Plate() { Weight = "0.5", Height = 85, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Kilogram };
        Plate QuarterKGPlate => new Plate() { Weight = "0.25", Height = 85, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Kilogram };

        Plate FourtyFiveLBPlate => new Plate() { Weight = "45", Height = 240, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Pound };
        Plate TwentyFiveLBPlate => new Plate() { Weight = "25", Height = 160, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Pound };
        Plate TenLBPlate => new Plate() { Weight = "10", Height = 140, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Pound };
        Plate FiveLBPlate => new Plate() { Weight = "5", Height = 120, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Pound };
        Plate TwoAndAHalfLBPlate => new Plate() { Weight = "2.5", Height = 100, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Pound };
        Plate OneAndAQuarterLBPlate => new Plate() { Weight = "1.25", Height = 80, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Pound };

        Plate CompetitionCollar => new Plate() { Weight = "Collar", Height = 100, BackgroundColorLight = Color.FromHex("#bdbdbd"), BackgroundColorDark = Color.FromHex("#cfcfcf"), PlateType = PlateType.Collar };

        Frame Spacer => new Frame()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            HasShadow = false,
            BorderColor = Colors.Transparent,
            Padding = 0,
            BackgroundColor = Colors.Transparent,
            HeightRequest = 240,
            WidthRequest = plateWidth,

        };

        enum PlateType
        {
            Pound,
            Kilogram,
            Collar,
            Spacer
        }
    }
}
