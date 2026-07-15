using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using IronPlus.Interfaces;
using IronPlus.ViewModels;
using IronPlus.Droid.Services;

namespace IronPlus;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.MainTheme);

            // TabLayoutResource = Resource.Layout.Tabbar;
            // ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            UserDialogs.Init(this);
            ViewModelLocator.RegisterSingleton<IPlatformService, PlatformService>();

            // Sync the status bar icon/text color with the current theme now that
            // IPlatformService and Platform.CurrentActivity are both available.
            ViewModelLocator.Resolve<IThemeService>().UpdateStatusBar(ViewModelLocator.Resolve<ISettingsService>().ThemeOption);

            // Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            // Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            // Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
}
