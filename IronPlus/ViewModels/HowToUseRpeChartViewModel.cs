using IronPlus.Interfaces;
using System.Collections.Generic;

namespace IronPlus.ViewModels
{
    public class HowToUseRpeChartViewModel : BaseViewModel
    {
        public HowToUseRpeChartViewModel(IDialogService dialogService, ISettingsService settingsService) : base(dialogService, settingsService)
        {
            Title = "How To Use";

            GenerateNewChartLabel = "How to generate a new Projected Weight:";
            GenerateNewChartSteps = new List<string>() { "Input the weight hit.", "Input the number of reps performed (between 1-15).", "Input the RPE (between 5-10)." };

            ChangeChartRepsLabel = "How to change rep range for\nRPE Chart:";
            ChangeChartRepsList = new List<string> { "Input the prescriped reps.", "Input the prescribed RPE." };
            ChangeRoundingValueLabel = "You can change what value the Projected Weight rounds to in the Settings tab.";
        }




        string generateNewChartLabel;
        public string GenerateNewChartLabel
        {
            get => generateNewChartLabel;
            set => SetProperty(ref generateNewChartLabel, value);
        }

        List<string> generateNewChartSteps;
        public List<string> GenerateNewChartSteps
        {
            get => generateNewChartSteps;
            set => SetProperty(ref generateNewChartSteps, value);
        }

        string changeChartRepsLabel;
        public string ChangeChartRepsLabel
        {
            get => changeChartRepsLabel;
            set => SetProperty(ref changeChartRepsLabel, value);
        }

        List<string> changeChartRepsList;
        public List<string> ChangeChartRepsList
        {
            get => changeChartRepsList;
            set => SetProperty(ref changeChartRepsList, value);
        }

        string changeRoundingValueLabel;
        public string ChangeRoundingValueLabel
        {
            get => changeRoundingValueLabel;
            set => SetProperty(ref changeRoundingValueLabel, value);
        }
    }
}
