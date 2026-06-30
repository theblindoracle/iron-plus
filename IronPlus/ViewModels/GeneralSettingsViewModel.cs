using System;
using CommunityToolkit.Mvvm.Messaging;
using IronPlus.Enums;
using IronPlus.Interfaces;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace IronPlus.ViewModels
{
    public class GeneralSettingsViewModel : BaseViewModel
    {
        public GeneralSettingsViewModel(IDialogService dialogService, ISettingsService settingsService) : base(dialogService, settingsService)
        {
            Title = "General Settings";

            SelectedPoundsRoundValue = settingsService.PoundsRoundSetting;
            SelectedKilogramRoundValue = settingsService.KilogramsRoundSetting;
        }

        double selectedPoundsRoundValue;
        public double SelectedPoundsRoundValue
        {
            get => selectedPoundsRoundValue;
            set => SetProperty(ref selectedPoundsRoundValue, value);
        }

        double selectedKilogramRoundValue;
        public double SelectedKilogramRoundValue
        {
            get => selectedKilogramRoundValue;
            set => SetProperty(ref selectedKilogramRoundValue, value);
        }



        Command updatePoundsRoundSettingCommand;
        public Command UpdatePoundsRoundSettingCommand => updatePoundsRoundSettingCommand ??= new Command(() =>
        {
            settingsService.PoundsRoundSetting = SelectedPoundsRoundValue;
            WeakReferenceMessenger.Default.Send(new Messages.UpdateUnitConversionSettingsMessage());
        });

        Command updateKilogramsRoundSettingCommand;
        public Command UpdateKilogramsRoundSettingCommand => updateKilogramsRoundSettingCommand ??= new Command(() =>
        {
            settingsService.KilogramsRoundSetting = SelectedKilogramRoundValue;
            WeakReferenceMessenger.Default.Send(new Messages.UpdateUnitConversionSettingsMessage());
        });
    }
}
