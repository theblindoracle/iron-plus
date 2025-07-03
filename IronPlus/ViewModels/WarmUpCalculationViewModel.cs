using IronPlus.Helpers;
using IronPlus.Interfaces;
using IronPlus.Models;
using IronPlus.Services;
using IronPlus.Validation;
using Syncfusion.Maui.Buttons;

namespace IronPlus.ViewModels
{
    public class WarmUpCalculationViewModel : BaseViewModel
    {

        readonly IWarmUpCalculationService warmUpCalculationService;
        readonly IDatabaseService databaseService;


        public WarmUpCalculationViewModel(IDialogService dialogService, ISettingsService settingsService, IWarmUpCalculationService warmUpCalculationService, IDatabaseService databaseService) : base(dialogService, settingsService)
        {
            this.warmUpCalculationService = warmUpCalculationService;
            this.databaseService = databaseService;


            Title = "Warm Up Calculator";

            TopSetWeight = new ValidatableObject<double>
            {
                Value = 327.5
            };

            BarbellWeight = selectedBarbell.WeightInPounds;

            SelectedCompetitionCollarIndex = 1;
            SelectedConverstionTypeIndex = 0;
            SelectedBarbellIndex = 0;

            UpdateTopSetValidations();

            NavigateToConversionPageCommand = new Command<WarmUpWeight>(NavigateToConversionPage);

            CalculateWarmUpWeight();
        }

        public override async Task InitializeAsync()
        {
            Barbells = await GetBarbells();
        }

        ValidatableObject<double> topSetWeight;
        public ValidatableObject<double> TopSetWeight
        {
            get => topSetWeight;
            set
            {
                SetProperty(ref topSetWeight, value);
                OnPropertyChanged(nameof(WarmUpWeights));
            }
        }

        bool isKilograms;
        public bool IsKilograms
        {
            get => isKilograms;
            set
            {
                SetProperty(ref isKilograms, value);
                settingsService.WarmUpCalculatorIsKilograms = value;
            }
        }

        bool isUsingCompetitionCollar;
        public bool IsUsingCompetitionCollar
        {
            get => isUsingCompetitionCollar;
            set => SetProperty(ref isUsingCompetitionCollar, value);
        }

        int barbellWeight;
        public int BarbellWeight
        {
            get => barbellWeight;
            set => SetProperty(ref barbellWeight, value);
        }

        public List<SfSegmentItem> UnitTypeList => new List<SfSegmentItem>
        {
            new SfSegmentItem { Text = "Pounds"},
            new SfSegmentItem { Text = "Kilograms"}
        };


        public List<SfSegmentItem> CompetitionCollarOptions => new List<SfSegmentItem>
        {
            new SfSegmentItem { Text = "Competition Collar"},
            new SfSegmentItem { Text = "No Comp. Collar"}
        };

        int selectedConversionTypeIndex;
        public int SelectedConverstionTypeIndex
        {
            get => selectedConversionTypeIndex;
            set => SetProperty(ref selectedConversionTypeIndex, value);
        }

        int selectedIsCompetitionCollarIndex;
        public int SelectedCompetitionCollarIndex
        {
            get => selectedIsCompetitionCollarIndex;
            set => SetProperty(ref selectedIsCompetitionCollarIndex, value);
        }

        int selectedBarbellIndex;
        public int SelectedBarbellIndex
        {
            get => selectedBarbellIndex;
            set => SetProperty(ref selectedBarbellIndex, value);
        }

        List<WarmUpWeight> warmUpWeights;
        public List<WarmUpWeight> WarmUpWeights
        {
            get => warmUpWeights;
            set => SetProperty(ref warmUpWeights, value);
        }


        Barbell selectedBarbell = new Barbell()
        {
            Name = "Standard",
            WeightInPounds = 45,
            WeightInKilograms = 20
        };

        List<Barbell> barbells = new List<Barbell>();
        public List<Barbell> Barbells
        {
            get => barbells;
            set => SetProperty(ref barbells, value);
        }

        Command calculateWarmUpWeightCommand;
        public Command CalculateWarmUpWeightCommand => calculateWarmUpWeightCommand ??= new Command(CalculateWarmUpWeight);


        Command<int> updateUnitTypeCommand;
        public Command<int> UpdateUnitTypeCommand => updateUnitTypeCommand ??= new Command<int>((index) =>
        {
            SelectedConverstionTypeIndex = index;
            if (index == 0)
            {
                IsKilograms = false;
                BarbellWeight = selectedBarbell.WeightInPounds;
                UpdateTopSetValidations();
                TopSetWeight.Value = GeneralHelpers.RoundValueToNearest(UnitConverters.KilogramsToPounds(TopSetWeight.Value), 5);
            }
            else
            {
                IsKilograms = true;
                BarbellWeight = selectedBarbell.WeightInKilograms;
                UpdateTopSetValidations();
                TopSetWeight.Value = GeneralHelpers.RoundValueToNearest(UnitConverters.PoundsToKilograms(TopSetWeight.Value), 2.5);
            }
        });


        Command<int> updateIsUsingCompetitionCollarCommand;
        public Command<int> UpdateIsUsingCompititionCollarCommand => updateIsUsingCompetitionCollarCommand ??= new Command<int>((index) =>
        {
            SelectedCompetitionCollarIndex = index;
            if (index == 0)
                IsUsingCompetitionCollar = true;
            else
                IsUsingCompetitionCollar = false;

            CalculateWarmUpWeight();
        });



        Command<int> updateSelectedBarbellCommand;
        public Command<int> UpdateSelectedBarbellCommand => updateSelectedBarbellCommand ??= new Command<int>((index) =>
        {
            SelectedBarbellIndex = index;
            selectedBarbell = Barbells[index];
            BarbellWeight = IsKilograms ? selectedBarbell.WeightInKilograms : selectedBarbell.WeightInPounds;

            CalculateWarmUpWeight();
        });

        void CalculateWarmUpWeight()
        {
            TopSetWeight.Validate();
            if (TopSetWeight.IsValid)
            {
                WarmUpWeights = warmUpCalculationService.CalculateWarmUps(TopSetWeight.Value, IsKilograms, IsUsingCompetitionCollar, BarbellWeight);

            }
        }

        Command<WarmUpWeight> navigateToConversionPageCommand;
        public Command<WarmUpWeight> NavigateToConversionPageCommand
        {
            get => navigateToConversionPageCommand;
            set => SetProperty(ref navigateToConversionPageCommand, value);
        }

        Command navigateToHowToUseCommand;
        public Command NavigateToHowToUseCommand => navigateToHowToUseCommand ??= new Command(async () => await Shell.Current.GoToAsync($"howToUseWarmUpCalculation"));


        async void NavigateToConversionPage(WarmUpWeight warmUpWeight)
        {
            await Shell.Current.GoToAsync($"showSpecificWeight?weight={warmUpWeight.Weight}&isKilograms={isKilograms}&isUsingCompCollar={IsUsingCompetitionCollar}&barbellWeight={BarbellWeight}");
        }


        void UpdateTopSetValidations()
        {
            TopSetWeight.Validations.Clear();
            TopSetWeight.IsValid = true;
            if (isKilograms)
            {
                TopSetWeight.Validations.Add(new IsWeightWithinRangeKilogramsRule<double>
                {
                    ValidationMessage = "Weight must be between 20 and 501."
                });
            }
            else
            {
                TopSetWeight.Validations.Add(new IsWeightWithinRangePoundsRule<double>
                {
                    ValidationMessage = "Weight must be between 45 and 1104."
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