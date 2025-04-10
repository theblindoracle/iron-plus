using System;
using System.Collections.Generic;
using IronPlus.Enums;
using IronPlus.Interfaces;
using Microsoft.Maui;

namespace IronPlus.ViewModels
{
    public class RPEChartSettingsViewModel : BaseViewModel
    {
        public RPEChartSettingsViewModel(IDialogService dialogService, ISettingsService settingsService) : base(dialogService, settingsService)
        {
            Title = "RPE Chart Settings";
            SelectedValue = settingsService.RpeChartRoundSetting;
        }

        double selectedValue;
        public double SelectedValue
        {
            get => selectedValue;
            set => SetProperty(ref selectedValue, value);
        }

        Command updateRpeChartRoundSettingCommand;
        public Command UpdateRpeChartRoundSettingCommand => updateRpeChartRoundSettingCommand ??= new Command(() =>
        {
            settingsService.RpeChartRoundSetting = SelectedValue;
            MessagingCenter.Send(this, MessageKeys.UpdateRpeChartSettings);
        });
    }
}
