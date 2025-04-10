using System;
using IronPlus;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using UIKit;

[assembly: ExportRenderer(typeof(IronPlus.AppShell), typeof(IronSport.iOS.CustomShellRenderer))]
namespace IronSport.iOS
{
    public class CustomShellRenderer : ShellRenderer
    {
        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            return new CustomShellSectionRenderer(this);
        }

        public class CustomShellSectionRenderer : ShellSectionRenderer
        {
            IShellContext context;

            public CustomShellSectionRenderer(IShellContext context) : base(context)
            {
                this.context = context;

                SetNavBarTheme(App.Current.RequestedTheme);

                App.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
            }

            private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
            {
                SetNavBarTheme(e.RequestedTheme);

                App.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
            }

            public CustomShellSectionRenderer(IShellContext context, Type navigationBarType, Type toolbarType) : base(context, navigationBarType, toolbarType)
            {
                this.context = context;

                SetNavBarTheme(App.Current.RequestedTheme);

                App.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
            }

            void SetNavBarTheme(AppTheme theme)
            {
                UIColor foreground;
                UIColor background;

                string backgroundKey;
                string foregroundKey;
                switch (theme)
                {
                    case AppTheme.Dark:
                        backgroundKey = "NavigationPrimaryDark";
                        foregroundKey = "OnNavigationPrimaryDark";
                        break;
                    case AppTheme.Light:
                        backgroundKey = "NavigationPrimaryLight";
                        foregroundKey = "OnNavigationPrimaryLight";
                        break;
                    case AppTheme.Unspecified:
                        AppTheme deviceTheme = AppInfo.RequestedTheme;
                        switch (deviceTheme)
                        {
                            case AppTheme.Dark:
                                backgroundKey = "NavigationPrimaryDark";
                                foregroundKey = "OnNavigationPrimaryDark";
                                break;
                            case AppTheme.Light:
                            case AppTheme.Unspecified:
                            default:
                                backgroundKey = "NavigationPrimaryLight";
                                foregroundKey = "OnNavigationPrimaryLight";
                                break;
                        }
                        break;
                    default:
                        backgroundKey = "NavigationPrimaryLight";
                        foregroundKey = "OnNavigationPrimaryLight";
                        break;
                }

                background = ((Color)App.Current.Resources[backgroundKey]).ToUIColor();
                foreground = ((Color)App.Current.Resources[foregroundKey]).ToUIColor();

                var titleTextAttributes = new UIStringAttributes()
                {
                    Font = UIFont.FromName("Rubik", 17),
                    ForegroundColor = foreground
                };

                if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                {
                    if (NavigationBar != null)
                    {
                        NavigationBar.StandardAppearance.BackgroundColor = background;
                        NavigationBar.StandardAppearance.TitleTextAttributes = titleTextAttributes;
                    }
                }
                else
                {
                    UINavigationBar.Appearance.BackgroundColor = background;
                    UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
                    {
                        Font = UIFont.FromName("Rubik", 17),
                        TextColor = foreground
                    });
                }
            }
        }
    }
}