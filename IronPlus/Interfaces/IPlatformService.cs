using System;
using System.Drawing;
using IronPlus.Enums;

namespace IronPlus.Interfaces
{
    public interface IPlatformService
    {
        void UpdateStatusBar(Microsoft.Maui.Graphics.Color? backgroundColor, bool darkStatusBarTint);
    }
}
