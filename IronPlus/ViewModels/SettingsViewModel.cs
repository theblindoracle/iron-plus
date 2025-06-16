using System.Collections.Generic;
using IronPlus.Helpers;
using IronPlus.Interfaces;

using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace IronPlus.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel(IDialogService dialogService, ISettingsService settingsService) : base(dialogService, settingsService)
        {
            Title = "Settings";

            SettingsList = new List<SettingsModel>()
            {
                new SettingsModel
                {
                    Icon = FAIcons.Cog,
                    Title = "General",
                    Command = new Command(async () =>  await Shell.Current.GoToAsync("generalSettings"))
                },
                new SettingsModel
                {
                    Icon = FAIcons.Calculator,
                    Title = "RPE Chart",
                    Command = new Command(async () =>  await Shell.Current.GoToAsync("rpeChartSettings"))
                },
                new SettingsModel
                {
                    Icon = FAIcons.Barbell,
                    Title = "Barbells",
                    Command = new Command(async () => await Shell.Current.GoToAsync("barbellSettings"))
                },
                new SettingsModel
                {
                    Icon = FAIcons.Adjust,
                    Title = "Theme",
                    Command = new Command(async () =>  await Shell.Current.GoToAsync("themeSettings"))
                },
                new SettingsModel
                {
                    Icon = FAIcons.Info,
                    Title = "About",
                    Command = new Command(async () =>  await Shell.Current.GoToAsync("about"))
                }

            };
        }

        List<SettingsModel> settingsList;
        public List<SettingsModel> SettingsList
        {
            get => settingsList;
            set => SetProperty(ref settingsList, value);
        }

        public string Version => $"Version: {VersionTracking.CurrentVersion} ({VersionTracking.CurrentBuild})";
    }

    public class SettingsModel
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public Command Command { get; set; }
    }
}
