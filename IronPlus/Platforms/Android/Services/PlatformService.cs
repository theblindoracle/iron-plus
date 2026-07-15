using AndroidX.Core.View;
using IronPlus.Interfaces;
using Color = Microsoft.Maui.Graphics.Color;

namespace IronPlus.Droid.Services
{
    public class PlatformService : IPlatformService
    {
        // The status bar background is always transparent under the edge-to-edge
        // enforcement mandated at targetSdkVersion 36, so backgroundColor is unused
        // here; only the icon/text appearance can still be controlled.
        public void UpdateStatusBar(Color? backgroundColor, bool darkStatusBarTint)
        {
            var window = Platform.CurrentActivity.Window;
            var insetsController = WindowCompat.GetInsetsController(window, window.DecorView);
            insetsController.AppearanceLightStatusBars = darkStatusBarTint;
        }
    }
}
