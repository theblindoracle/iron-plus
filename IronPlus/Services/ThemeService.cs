using System;
using IronPlus.Interfaces;
using IronPlus.ViewModels;

using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace IronPlus.Services
{
    public class ThemeService : IThemeService
    {
        readonly ISettingsService settingsService;

        public ThemeService(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public Color GetThemedResourceColor(string resource, bool opposite = false)
        {
            var lightTheme = "Light";
            var darkTheme = "Dark";

            if (opposite)
            {
                lightTheme = "Dark";
                darkTheme = "Light";
            }

            string theme;
            switch (settingsService.ThemeOption)
            {
                case AppTheme.Dark:
                    theme = darkTheme;
                    break;
                case AppTheme.Light:
                    theme = lightTheme;
                    break;
                case AppTheme.Unspecified:
                    AppTheme deviceTheme = AppInfo.RequestedTheme;
                    switch (deviceTheme)
                    {
                        case AppTheme.Dark:
                            theme = darkTheme;
                            break;
                        case AppTheme.Light:
                        case AppTheme.Unspecified:
                        default:
                            theme = lightTheme;
                            break;
                    }
                    break;
                default:
                    theme = lightTheme;
                    break;
            }

            return (Color)App.Current.Resources[$"{resource}{theme}"];
        }



        public void UpdateStatusBar(AppTheme requestedTheme)
        {
            var platformService = ViewModelLocator.Resolve<IPlatformService>();

            var backgroundColor = GetThemedResourceColor("NavigationStatusBar");

            var darkTint = IsDarkTint();

            if (Device.RuntimePlatform == Device.Android)
            {
                platformService?.UpdateStatusBar(backgroundColor, darkTint);
            }
            else
            {
                platformService?.UpdateStatusBar(null, darkTint);

            }
        }

        bool IsDarkTint()
        {
            bool darkTint = false;
            switch (settingsService.ThemeOption)
            {
                case AppTheme.Dark:
                    darkTint = false;
                    break;
                case AppTheme.Light:
                    darkTint = true;
                    break;
                case AppTheme.Unspecified:
                    AppTheme deviceTheme = AppInfo.RequestedTheme;
                    switch (deviceTheme)
                    {
                        case AppTheme.Dark:
                            darkTint = false;
                            break;
                        case AppTheme.Light:
                        case AppTheme.Unspecified:
                        default:
                            darkTint = true;
                            break;
                    }
                    break;
                default:
                    break;
            }
            return darkTint;
        }
    }
}
