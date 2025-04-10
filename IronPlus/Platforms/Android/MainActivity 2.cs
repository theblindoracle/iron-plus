using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.OS;
using IronSport.Services;
using IronSport.Enums;
using IronSport.ViewModels;
using IronSport.Interfaces;
using IronSport.Droid.Services;
using Acr.UserDialogs;

namespace IronSport.Droid
{
    [Activity(Label = "Iron Plus", Theme = "@style/MainTheme.Splash", MainLauncher = true, Icon = "@mipmap/ic_launcher", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.MainTheme);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

#if DEBUG
            // Inititializes with Beta Key
            AppCenterService.Init(Constants.AppCenterKeys.ANDROID_BETA);
#endif
            AppCenterService.Init(Constants.AppCenterKeys.ANDROID_PROD);

            UserDialogs.Init(this);
            ViewModelLocator.RegisterSingleton<IPlatformService, PlatformService>();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}