using System;
using IronPlus.Enums;
using IronPlus.Interfaces;

using Microsoft.Maui;

namespace IronPlus.Services
{
    public class SettingsService : ISettingsService
    {
        public double RpeChartRoundSetting
        {
            get => Preferences.Get(nameof(RpeChartRoundSetting), 5.0);
            set => Preferences.Set(nameof(RpeChartRoundSetting), value);
        }

        public double PoundsRoundSetting
        {
            get => Preferences.Get(nameof(PoundsRoundSetting), 5.0);
            set => Preferences.Set(nameof(PoundsRoundSetting), value);
        }

        public double KilogramsRoundSetting
        {
            get => Preferences.Get(nameof(KilogramsRoundSetting), 2.5);
            set => Preferences.Set(nameof(KilogramsRoundSetting), value);
        }

        public bool UnitConverterIsConvertToKilograms
        {
            get => Preferences.Get(nameof(UnitConverterIsConvertToKilograms), true);
            set => Preferences.Set(nameof(UnitConverterIsConvertToKilograms), value);
        }

        public bool UnitConverterIsUsingCompetitionCollar
        {
            get => Preferences.Get(nameof(UnitConverterIsUsingCompetitionCollar), true);
            set => Preferences.Set(nameof(UnitConverterIsUsingCompetitionCollar), value);
        }

        public bool WarmUpCalculatorIsKilograms
        {
            get => Preferences.Get(nameof(WarmUpCalculatorIsKilograms), false);
            set => Preferences.Set(nameof(WarmUpCalculatorIsKilograms), value);
        }

        public AppTheme ThemeOption
        {
            get => (AppTheme)Preferences.Get(nameof(ThemeOption), (int)AppTheme.Unspecified);
            set => Preferences.Set(nameof(ThemeOption), (int)value);
        }
    }
}
