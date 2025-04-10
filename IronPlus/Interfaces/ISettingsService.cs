using Microsoft.Maui;

namespace IronPlus.Interfaces
{
    public interface ISettingsService
    {
        double RpeChartRoundSetting { get; set; }
        double PoundsRoundSetting { get; set; }
        double KilogramsRoundSetting { get; set; }
        bool UnitConverterIsConvertToKilograms { get; set; }
        bool UnitConverterIsUsingCompetitionCollar { get; set; }
        bool WarmUpCalculatorIsKilograms { get; set; }
        AppTheme ThemeOption { get; set; }
    }
}