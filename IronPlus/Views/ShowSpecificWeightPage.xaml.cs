using System;
using IronPlus.ViewModels;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;

namespace IronPlus.Views
{
    [QueryProperty(nameof(Weight), "weight")]
    [QueryProperty(nameof(IsKilograms), "isKilograms")]
    [QueryProperty(nameof(CompCollar), "isUsingCompCollar")]
    [QueryProperty(nameof(BarbellWeight), "barbellWeight")]


    public partial class ShowSpecificWeightPage : BasePage
    {
        public ShowSpecificWeightPage()
        {
            InitializeComponent();

        }

        public string Weight
        {
            set
            {
                var vm = (ShowSpecificWeightViewModel)BindingContext;
                if (vm != null)
                {
                    vm.Weight = double.Parse(Uri.UnescapeDataString(value));
                }

            }
        }

        public string IsKilograms
        {
            set
            {
                var vm = (ShowSpecificWeightViewModel)BindingContext;
                if (vm != null)
                    vm.IsKilograms = bool.Parse(Uri.UnescapeDataString(value));

                if (vm.IsKilograms)
                {
                    vm.WeightSuffix = "KG";
                    vm.ConvertedSuffix = "LB";
                    vm.ConvertedValue = UnitConverters.KilogramsToPounds(vm.Weight);
                }
                else
                {
                    vm.WeightSuffix = "LB";
                    vm.ConvertedSuffix = "KG";
                    vm.ConvertedValue = UnitConverters.PoundsToKilograms(vm.Weight);
                }
            }
        }

        public string CompCollar
        {
            set
            {
                var vm = (ShowSpecificWeightViewModel)BindingContext;
                if (vm != null)
                {
                    vm.IsUsingCompetitionCollar = bool.Parse(Uri.UnescapeDataString(value));
                }

            }
        }

        public string BarbellWeight
        {
            set
            {
                var vm = (ShowSpecificWeightViewModel)BindingContext;
                if (vm != null)
                {
                    vm.BarbellWeight = int.Parse(Uri.UnescapeDataString(value));
                }

            }
        }
    }
}
