using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;

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