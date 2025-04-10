using IronPlus.Interfaces;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;

namespace IronPlus.ViewModels
{
    public class ThemeSettingsViewModel : BaseViewModel
    {
        public ThemeSettingsViewModel(IDialogService dialogService, ISettingsService settingsService) : base(dialogService, settingsService)
        {
            Title = "Theme Settings";
            SelectedTheme = settingsService.ThemeOption;
        }

        AppTheme selectedTheme;
        public AppTheme SelectedTheme
        {
            get => selectedTheme;
            set => SetProperty(ref selectedTheme, value);
        }

        Command updateThemeCommand;
        public Command UpdateThemeCommand => updateThemeCommand ??= new Command(() =>
        {
            var currentTheme = Application.Current.RequestedTheme;
            if (currentTheme == SelectedTheme)
                return;

            settingsService.ThemeOption = SelectedTheme;
            Application.Current.UserAppTheme = SelectedTheme;
        });
    }
}
