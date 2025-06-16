
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Graphics;

namespace IronPlus.Interfaces
{
    public interface IThemeService
    {
        Color GetThemedResourceColor(string resource, bool opposite);
        void UpdateStatusBar(AppTheme requestedTheme);
    }
}