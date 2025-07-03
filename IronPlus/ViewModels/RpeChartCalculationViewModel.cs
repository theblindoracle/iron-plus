using IronPlus.Enums;
using IronPlus.Helpers;
using IronPlus.Interfaces;
using IronPlus.Validation;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace IronPlus.ViewModels
{
    public class RpeChartCalculationViewModel : BaseViewModel
    {
        readonly IRpeCalculationService rpeCalculationService;

        public RpeChartCalculationViewModel(IDialogService userDialog, ISettingsService settingsService, IRpeCalculationService rpeCalculationService) : base(userDialog, settingsService)
        {
            this.rpeCalculationService = rpeCalculationService;

            Title = "RPE Calculator";

            RoundToNearest = settingsService.RpeChartRoundSetting;

            AddValidationRules();

            CalculateNewWeight();

            MessagingCenter.Subscribe<RPEChartSettingsViewModel>(this, MessageKeys.UpdateRpeChartSettings, (sender) =>
            {
                RoundToNearest = settingsService.RpeChartRoundSetting;
                CalculateNewWeight();
            });
        }


        ValidatableObject<double> haveWeight = new ValidatableObject<double> { Value = 330.0 };
        public ValidatableObject<double> HaveWeight
        {
            get => haveWeight;
            set
            {
                SetProperty(ref haveWeight, value);
            }
        }

        ValidatableObject<int> haveReps = new ValidatableObject<int> { Value = 1 };
        public ValidatableObject<int> HaveReps
        {
            get => haveReps;
            set
            {
                SetProperty(ref haveReps, value);
            }
        }

        ValidatableObject<double> haveRpe = new ValidatableObject<double> { Value = 9.0 };
        public ValidatableObject<double> HaveRpe
        {
            get => haveRpe;
            set
            {
                SetProperty(ref haveRpe, value);
            }
        }

        ValidatableObject<int> wantReps = new ValidatableObject<int> { Value = 5 };
        public ValidatableObject<int> WantReps
        {
            get => wantReps;
            set
            {
                SetProperty(ref wantReps, value);
            }
        }

        ValidatableObject<double> wantRpe = new ValidatableObject<double> { Value = 9.0 };
        public ValidatableObject<double> WantRpe
        {
            get => wantRpe;
            set
            {
                SetProperty(ref wantRpe, value);
            }
        }

        double roundToNearest;
        public double RoundToNearest
        {
            get => roundToNearest;
            set
            {
                SetProperty(ref roundToNearest, value);
            }
        }

        double wantedSetWeight;
        public double WantedSetWeight
        {
            get => wantedSetWeight;
            set => SetProperty(ref wantedSetWeight, value);
        }

        double e1RM;
        public double E1RM
        {
            get => e1RM;
            set => SetProperty(ref e1RM, value);
        }

        Command navigateToHowToUseCommand;
        public Command NavigateToHowToUseCommand => navigateToHowToUseCommand ??= new Command(async () => await Shell.Current.GoToAsync($"howToUseRpeChart"));


        Command calculateNewWeightCommand;
        public Command CalculateNewWeightCommand => calculateNewWeightCommand ??= new Command(CalculateNewWeight);

        bool showCompletedSetHelperText;
        public bool ShowCompletedSetHelperText
        {
            get => showCompletedSetHelperText;
            set => SetProperty(ref showCompletedSetHelperText, value);
        }

        Command showCompletedSetHelperTextCommand;
        public Command ShowCompletedSetHelperTextCommand => showCompletedSetHelperTextCommand ??= new Command(() => ShowCompletedSetHelperText = !ShowCompletedSetHelperText);

        bool showTargetSetHelperText;
        public bool ShowTargetSetHelperText
        {
            get => showTargetSetHelperText;
            set => SetProperty(ref showTargetSetHelperText, value);
        }

        Command showTargetSetHelperTextCommand;
        public Command ShowTargetSetHelperTextCommand => showTargetSetHelperTextCommand ??= new Command(() => ShowTargetSetHelperText = !ShowTargetSetHelperText);


        void CalculateNewWeight()
        {
            if (ValidateInput())
            {
                E1RM = GeneralHelpers.RoundValueToNearest(rpeCalculationService.CalculateOneRepMax(HaveWeight.Value, HaveReps.Value, HaveRpe.Value), RoundToNearest);
                WantedSetWeight = GeneralHelpers.RoundValueToNearest(rpeCalculationService.CalculateWantedSetWeight(WantReps.Value, WantRpe.Value, E1RM), RoundToNearest);
            }
        }


        void AddValidationRules()
        {
            HaveWeight.Validations.Add(new IsWeightWithinRangeRPEChartRule<double>
            {
                ValidationMessage = "Weight must be between 0 and 10000."
            });

            HaveReps.Validations.Add(new IsRepsInRangeRpeChartRule<int>
            {
                ValidationMessage = "Reps must between 1 and 15."
            });

            HaveRpe.Validations.Add(new IsRpeWithinRangeRpeChartRule<double>
            {
                ValidationMessage = "RPE must be between 5 and 10."
            });

            WantReps.Validations.Add(new IsRepsForChartWithinRangeRule<int>
            {
                ValidationMessage = "Selected Reps must be between 1 and 12."
            });

            WantRpe.Validations.Add(new IsRpeWithinRangeRpeChartRule<double>
            {
                ValidationMessage = "RPE must be between 5 and 10."
            });

        }

        bool ValidateInput()
        {
            HaveWeight.Validate();
            HaveReps.Validate();
            HaveRpe.Validate();
            WantReps.Validate();
            WantRpe.Validate();

            return HaveWeight.IsValid && HaveReps.IsValid && HaveRpe.IsValid && WantReps.IsValid;
        }
    }
}