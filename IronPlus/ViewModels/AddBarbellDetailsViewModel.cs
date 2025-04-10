using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using IronPlus.Interfaces;
using IronPlus.Models;
using IronPlus.Services;
using IronPlus.Validation;

using Microsoft.Maui;

namespace IronPlus.ViewModels
{
    public class AddBarbellDetailsViewModel : BaseViewModel
    {
        IDatabaseService databaseService;

        public AddBarbellDetailsViewModel(IDialogService dialogService, ISettingsService settingsService, IDatabaseService databaseService) : base(dialogService, settingsService)
        {
            this.databaseService = databaseService;
            Title = "Add Barbell";

        }

        public override async Task InitializeAsync()
        {
            if (Barbell != null)
            {
                Name.Value = Barbell.Name;
                WeightInPounds.Value = Barbell.WeightInPounds;
                WeightInKilograms.Value = Barbell.WeightInKilograms;
            }

            AddValidationRules();

            SaveBarbellCommand.ChangeCanExecute();

            await base.InitializeAsync();
        }

        Barbell barbell;
        public Barbell Barbell
        {
            get => barbell;
            set => SetProperty(ref barbell, value);
        }

        ValidatableObject<string> name = new ValidatableObject<string>();
        public ValidatableObject<string> Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        ValidatableObject<int> weightInPounds = new ValidatableObject<int>();
        public ValidatableObject<int> WeightInPounds
        {
            get => weightInPounds;
            set => SetProperty(ref weightInPounds, value);
        }

        ValidatableObject<int> weightInKilograms = new ValidatableObject<int>();
        public ValidatableObject<int> WeightInKilograms
        {
            get => weightInKilograms;
            set => SetProperty(ref weightInKilograms, value);
        }


        Command saveBarbellCommand;
        public Command SaveBarbellCommand => saveBarbellCommand ??= new Command(async () =>
       {
           if (Barbell == null)
           {
               Barbell = new Barbell() { Name = Name.Value, WeightInPounds = WeightInPounds.Value, WeightInKilograms = WeightInKilograms.Value };
           }
           else
           {
               Barbell.Name = Name.Value;
               Barbell.WeightInPounds = WeightInPounds.Value;
               Barbell.WeightInKilograms = WeightInKilograms.Value;
           }

           bool errorOccured = false;
           try
           {
               await databaseService.SaveBarbellAsync(Barbell);
           }
           catch (Exception ex)
           {
               errorOccured = true;
               AppCenterService.Track_App_Exception(ex, this);
               await dialogService.AlertAsync("An error occured while saving.", "Error");
           }
           if (!errorOccured)
           {
               dialogService.Toast(new ToastConfig("Barbell was saved successfully!"));
           }
           await Shell.Current.GoToAsync("..");

       }, () => IsInputValid && !IsInputDefault);

        public bool IsInputDefault => Name.Value == null || WeightInPounds.Value == 0 || WeightInKilograms.Value == 0;

        public bool IsInputValid => Name.IsValid && WeightInPounds.IsValid && WeightInKilograms.IsValid;

        Command validateNameCommand;
        public Command ValidateNameCommand => validateNameCommand ??= new Command(() =>
        {
            Name.Validate();
            SaveBarbellCommand.ChangeCanExecute();
        });


        Command validateWeightInPoundsCommand;
        public Command ValidateWeightInPoundsCommand => validateWeightInPoundsCommand ??= new Command(() =>
        {
            WeightInPounds.Validate();
            SaveBarbellCommand.ChangeCanExecute();
        });

        Command validateWeightInKilogramsCommand;
        public Command ValidateWeightInKilogramsCommand => validateWeightInKilogramsCommand ??= new Command(() =>
        {
            WeightInKilograms.Validate();
            SaveBarbellCommand.ChangeCanExecute();
        });

        bool autofillCompleted = false;

        Command<bool> autofillKilogramWeightCommand;
        public Command<bool> AutofillKilogramWeightCommand => autofillKilogramWeightCommand ??= new Command<bool>((hasFocus) =>
        {
            if (!autofillCompleted && WeightInPounds.IsValid && !hasFocus)
            {
                var convertedWeight = UnitConverters.PoundsToKilograms(Convert.ToDouble(WeightInPounds.Value));
                WeightInKilograms.Value = Convert.ToInt32(Helpers.GeneralHelpers.RoundValueToNearest(convertedWeight, 1));

                WeightInKilograms.Validate();
                SaveBarbellCommand.ChangeCanExecute();

                autofillCompleted = true;
            }
        });

        Command<bool> autofillPoundWeightCommand;
        public Command<bool> AutofillPoundWeightCommand => autofillPoundWeightCommand ??= new Command<bool>((hasFocus) =>
        {
            if (!autofillCompleted && WeightInKilograms.IsValid && !hasFocus)
            {
                var convertedWeight = UnitConverters.KilogramsToPounds(Convert.ToDouble(WeightInKilograms.Value));
                WeightInPounds.Value = Convert.ToInt32(Helpers.GeneralHelpers.RoundValueToNearest(convertedWeight, 1));

                WeightInPounds.Validate();
                SaveBarbellCommand.ChangeCanExecute();

                autofillCompleted = true;
            }
        });

        void AddValidationRules()
        {
            Name.Validations.Add(new IsNameForBarbellWithinLengthRule<string>
            {
                ValidationMessage = "Name length must be between 1 and 15."
            });

            WeightInPounds.Validations.Add(new IsWeightForBarbellWithinRangePoundsRule<int>
            {
                ValidationMessage = "Weight must be between 5 and 95."
            });

            WeightInKilograms.Validations.Add(new IsWeightForBarbellWithinRangeKilogramsRule<int>
            {
                ValidationMessage = "Weight must be between 2.5 and 45."
            });
        }

    }
}