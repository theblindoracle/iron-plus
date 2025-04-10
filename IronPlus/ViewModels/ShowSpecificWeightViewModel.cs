using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IronPlus.Interfaces;
using IronPlus.Models;
using Syncfusion.Maui.Buttons;
using Microsoft.Maui;

namespace IronPlus.ViewModels
{
    public class ShowSpecificWeightViewModel : BaseViewModel
    {
        public ShowSpecificWeightViewModel(IDialogService dialogService, ISettingsService settingsService) : base(dialogService, settingsService)
        {

            Title = "Weight";
        }

        int barbellWeight;
        public int BarbellWeight
        {
            get => barbellWeight;
            set => SetProperty(ref barbellWeight, value);
        }

        bool isUsingCompetitionCollar;
        public bool IsUsingCompetitionCollar
        {
            get => isUsingCompetitionCollar;
            set => SetProperty(ref isUsingCompetitionCollar, value);
        }

        double weight;
        public double Weight
        {
            get => weight;
            set => SetProperty(ref weight, value);
        }

        bool isKilograms;
        public bool IsKilograms
        {
            get => isKilograms;
            set => SetProperty(ref isKilograms, value);
        }

        string weightSuffix;
        public string WeightSuffix
        {
            get => weightSuffix;
            set => SetProperty(ref weightSuffix, value);
        }

        string convertedSuffix;
        public string ConvertedSuffix
        {
            get => convertedSuffix;
            set => SetProperty(ref convertedSuffix, value);
        }

        double convertedValue;
        public double ConvertedValue
        {
            get => convertedValue;
            set => SetProperty(ref convertedValue, value);
        }
    }
}
