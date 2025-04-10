using System;
using System.Drawing;
using IronPlus.Enums;
using IronPlus.Interfaces;
using Microsoft.Maui.Platform;
using UIKit;
using Color = Microsoft.Maui.Graphics.Color;

namespace IronSport.iOS.Services
{
    public class PlatformService : IPlatformService
    {
        public void UpdateStatusBar(Color? backgroundColor, bool darkStatusBarTint)
        {
            var color = Microsoft.Maui.Graphics.Color.FromRgba(0, 0, 0, 0);
            if (!darkStatusBarTint)
            {
                UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
            }

            else if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.DarkContent;
            }
            else
            {
                UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.Default;
            }
        }
    }
}
