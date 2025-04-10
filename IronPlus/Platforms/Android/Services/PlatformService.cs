using System;
using System.Drawing;
using Android.OS;
using IronPlus.Interfaces;
using Microsoft.Maui.Platform;
using Color = Microsoft.Maui.Graphics.Color;

namespace IronSport.Droid.Services
{
    public class PlatformService : IPlatformService
    {
        public void UpdateStatusBar(Color? backgroundColor, bool darkStatusBarTint)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
                return;

            var activity = Platform.CurrentActivity;
            var window = activity.Window;
            window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
            window.SetStatusBarColor(backgroundColor.ToPlatform());

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            {
                var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
                window.DecorView.SystemUiVisibility = darkStatusBarTint ? flag : 0;
            }
        }
    }
}
