using System.Collections.Generic;
using System.Threading.Tasks;
using IronPlus.Enums;
using IronPlus.Helpers;
using IronPlus.Interfaces;
using IronPlus.Models;
using IronPlus.Validation;
using Syncfusion.Maui.Buttons;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;

namespace IronPlus.ViewModels
{
    public class UnitConversionViewModel : BaseViewModel
    {
        readonly IDatabaseService databaseService;

        public UnitConversionViewModel(IDialogService userDialog, ISettingsService settingsService, IDatabaseService databaseService) : base(userDialog, settingsService)
        {
            this.databaseService = databaseService;

            Title = "Unit Converter";

            kilogramRoundToNearest = settingsService.KilogramsRoundSetting;
            poundRoundToNearest = settingsService.PoundsRoundSetting;
            IsUsingCompetitionCollar = settingsService.UnitConverterIsUsingCompetitionCollar;
            IsConvertToKilograms = settingsService.UnitConverterIsConvertToKilograms;

            if (IsConvertToKilograms)
            {
                ValueToConvert = new ValidatableObject<double> { Value = 405 };
            }
            else
            {
                ValueToConvert = new ValidatableObject<double> { Value = 182.5 };
            }


            if (IsConvertToKilograms)
            {
                SetUpConvertFromPoundsToKilograms();
            }
            else
            {
                SetUpConvertFromKilogramsToPounds();
            }


            if (IsUsingCompetitionCollar)
            {
                CollarControlIndex = 0;
            }
            else
            {
                CollarControlIndex = 1;
            }



            CalculateConvertedValues();

            MessagingCenter.Subscribe<GeneralSettingsViewModel>(this, MessageKeys.UpdateUnitConversionSettings, (sender) =>
            {
                kilogramRoundToNearest = settingsService.KilogramsRoundSetting;
                poundRoundToNearest = settingsService.PoundsRoundSetting;
                CalculateConvertedValues();
            });
        }

        public override async Task InitializeAsync()
        {
            Barbells = await GetBarbells();

            UpdateSelectedBarbellCommand.Execute(0);
        }

        double poundRoundToNearest;
        double kilogramRoundToNearest;
        double convertedValue;

        public List<SfSegmentItem> ConversionToggleList
        {
            get => new List<SfSegmentItem>
            {
                new SfSegmentItem { Text = "LB to KG"},
                new SfSegmentItem { Text = "KG to LB"}
            };
        }

        public List<SfSegmentItem> CollarToggleList
        {
            get => new List<SfSegmentItem>
            {
                new SfSegmentItem { Text = "Competition Collar"},
                new SfSegmentItem { Text = "No Comp. Collar"}
            };
        }

        List<Barbell> barbells = new List<Barbell>() { new Barbell { Name = "Standard", WeightInPounds = 45, WeightInKilograms = 20 } };
        public List<Barbell> Barbells
        {
            get => barbells;
            set => SetProperty(ref barbells, value);
        }

        private Barbell selectedBarbell =
            new(){Name = "Standard", WeightInPounds = 45,
                WeightInKilograms = 20};
        public Barbell SelectedBarbell
        {
            get => selectedBarbell;
            set
            {
                SetProperty(ref selectedBarbell, value);
                OnPropertyChanged(nameof(SelectedBarbellWeight));
            }
        }

        public int SelectedBarbellWeight
        {
            get => IsConvertToKilograms ? SelectedBarbell.WeightInKilograms : SelectedBarbell.WeightInPounds;
        }

        ValidatableObject<double> valueToConvert;
        public ValidatableObject<double> ValueToConvert
        {
            get => valueToConvert;
            set => SetProperty(ref valueToConvert, value);
        }


        double convertedValueRounded;
        public double ConvertedValueRounded
        {
            get => convertedValueRounded;
            set
            {
                SetProperty(ref convertedValueRounded, value);
                OnPropertyChanged(nameof(ConvertedValueRoundedConvertedBack));
            }
        }

        public double ConvertedValueRoundedConvertedBack
        {
            get => IsConvertToKilograms ? UnitConverters.KilogramsToPounds(ConvertedValueRounded) : UnitConverters.PoundsToKilograms(ConvertedValueRounded);
        }

        string convertFromSuffix;
        public string ConvertFromSuffix
        {
            get => convertFromSuffix;
            set => SetProperty(ref convertFromSuffix, value);
        }

        string convertToSuffix;
        public string ConvertToSuffix
        {
            get => convertToSuffix;
            set => SetProperty(ref convertToSuffix, value);
        }

        bool isConvertToKilograms;
        public bool IsConvertToKilograms
        {
            get => isConvertToKilograms;
            set
            {
                SetProperty(ref isConvertToKilograms, value);
                OnPropertyChanged(nameof(SelectedBarbellWeight));
                settingsService.UnitConverterIsConvertToKilograms = value;
            }
        }

        bool isUsingCompetitionCollar;
        public bool IsUsingCompetitionCollar
        {
            get => isUsingCompetitionCollar;
            set
            {
                SetProperty(ref isUsingCompetitionCollar, value);
                settingsService.UnitConverterIsUsingCompetitionCollar = value;
            }
        }

        int conversionTypeControlIndex;
        public int ConversionTypeControlIndex
        {
            get => conversionTypeControlIndex;
            set => SetProperty(ref conversionTypeControlIndex, value);
        }

        int collarControlIndex;
        public int CollarControlIndex
        {
            get => collarControlIndex;
            set => SetProperty(ref collarControlIndex, value);
        }

        int barbellControlIndex;
        public int BarbellControlIndex
        {
            get => barbellControlIndex;
            set => SetProperty(ref barbellControlIndex, value);
        }



        Command updateConversionTypeCommand;
        public Command UpdateConversionTypeCommand => updateConversionTypeCommand ??= new Command(UpdateConversionType);

        Command updateCollarCommand;
        public Command UpdateIsUsingCompetitionCollarCommand => updateCollarCommand ??= new Command(UpdateIsUsingCompetitionCollar);


        Command calculateConvertedValuesCommand;
        public Command CalculateConvertedValuesCommand => calculateConvertedValuesCommand ??= new Command(CalculateConvertedValues);


        Command<int> updateSelectedBarbellCommand;
        public Command<int> UpdateSelectedBarbellCommand => updateSelectedBarbellCommand ??= new Command<int>((index) =>
        {
            SelectedBarbell = Barbells[index];
        });

        Command navigateToHowToUseCommand;
        public Command NavigateToHowToUseCommand => navigateToHowToUseCommand ??= new Command(async () => await Shell.Current.GoToAsync($"howToUseUnitConversion"));

        void UpdateConversionType()
        {
            if (ConversionTypeControlIndex == 0)
            {
                SetUpConvertFromPoundsToKilograms();
            }

            if (ConversionTypeControlIndex == 1)
            {
                SetUpConvertFromKilogramsToPounds();
            }
        }

        void UpdateIsUsingCompetitionCollar()
        {
            if (CollarControlIndex == 0)
            {
                IsUsingCompetitionCollar = true;
            }
            if (CollarControlIndex == 1)
            {
                IsUsingCompetitionCollar = false;
            }
        }

        void SetUpConvertFromKilogramsToPounds()
        {
            IsConvertToKilograms = false;
            UpdateValueToConvertValidations();
            ConversionTypeControlIndex = 1;
            ConvertFromSuffix = "KG";
            ConvertToSuffix = "LB";
            if (convertedValue != 0)
            {
                ValueToConvert.Value = ConvertedValueRounded;
            }
        }

        void SetUpConvertFromPoundsToKilograms()
        {
            IsConvertToKilograms = true;
            UpdateValueToConvertValidations();
            ConversionTypeControlIndex = 0;
            ConvertFromSuffix = "LB";
            ConvertToSuffix = "KG";
            if (convertedValue != 0)
            {
                ValueToConvert.Value = ConvertedValueRounded;
            }
        }

        void CalculateConvertedValues()
        {
            ValueToConvert.Validate();
            if (ValueToConvert.IsValid)
            {
                if (IsConvertToKilograms)
                {
                    convertedValue = UnitConverters.PoundsToKilograms(ValueToConvert.Value);
                    ConvertedValueRounded = GeneralHelpers.RoundValueToNearest(convertedValue, kilogramRoundToNearest);
                }
                else
                {
                    convertedValue = UnitConverters.KilogramsToPounds(ValueToConvert.Value);
                    ConvertedValueRounded = GeneralHelpers.RoundValueToNearest(convertedValue, poundRoundToNearest);
                }
            }
        }

        void UpdateValueToConvertValidations()
        {
            ValueToConvert.Validations.Clear();
            ValueToConvert.IsValid = true;
            if (IsConvertToKilograms)
            {
                ValueToConvert.Validations.Add(new IsWeightWithinRangePoundsRule<double>
                {
                    ValidationMessage = "Weight must be between 45 and 1104."
                });
            }
            else
            {
                ValueToConvert.Validations.Add(new IsWeightWithinRangeKilogramsRule<double>
                {
                    ValidationMessage = "Weight must be between 20 and 501."
                });
            }
        }

        async Task<List<Barbell>> GetBarbells()
        {
            var barbells = new List<Barbell>()
            {
                new Barbell()
                {
                    Name = "Standard",
                    WeightInPounds = 45,
                    WeightInKilograms = 20
                }
            };

            barbells.AddRange(await databaseService.GetBarbellsAsync());

            return barbells;
        }
    }
}
