using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;

[assembly: ExportRenderer(typeof(IronPlus.AppShell), typeof(IronSport.Droid.CustomShellRenderer))]
namespace IronSport.Droid
{
    public class CustomShellRenderer : ShellRenderer
    {
        public CustomShellRenderer(Context context) : base(context)
        {
        }

        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new CustomShellBottomNavViewAppearanceTracker(this, shellItem);
        }

        protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker()
        {
            return new CustomShellToolbarAppearanceTracker(this);
        }
    }

    public class CustomShellToolbarAppearanceTracker : ShellToolbarAppearanceTracker
    {
        IShellContext context;
        public CustomShellToolbarAppearanceTracker(IShellContext context) : base(context)
        {
            this.context = context;
        }

        public override void SetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
            base.SetAppearance(toolbar, toolbarTracker, appearance);

            if (toolbar.TitleFormatted == null)
                return;

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.P)
            {
                Typeface typeface = Typeface.CreateFromAsset(context.AndroidContext.Assets, "Rubik-Regular.ttf");
                var spannableString = new SpannableString(toolbar.TitleFormatted);
                spannableString.SetSpan(new TypefaceSpan(typeface), 0, spannableString.Length(), SpanTypes.ExclusiveExclusive);

                toolbar.TitleFormatted = spannableString;

            }
            else
            {
                var spannableString = new SpannableString(toolbar.TitleFormatted);
                spannableString.SetSpan(new TypefaceSpan("Rubik-Regular.ttf"), 0, spannableString.Length(), SpanTypes.ExclusiveExclusive);

                toolbar.TitleFormatted = spannableString;
            }
        }
    }

    public class CustomShellBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
    {
        IShellContext context;

        public CustomShellBottomNavViewAppearanceTracker(IShellContext context, ShellItem shellItem) : base(context, shellItem)
        {
            this.context = context;
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);

            IMenu menu = bottomView.Menu;
            for (int i = 0; i < bottomView.Menu.Size(); i++)
            {
                IMenuItem menuItem = menu.GetItem(i);

                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.P)
                {
                    Typeface typeface = Typeface.CreateFromAsset(context.AndroidContext.Assets, "Rubik-Regular.ttf");
                    var spannableString = new SpannableString(menuItem.TitleFormatted);
                    spannableString.SetSpan(new TypefaceSpan(typeface), 0, spannableString.Length(), SpanTypes.ExclusiveExclusive);

                    menuItem.SetTitle(spannableString);

                }
                else
                {
                    var spannableString = new SpannableString(menuItem.TitleFormatted);
                    spannableString.SetSpan(new TypefaceSpan("Rubik-Regular.ttf"), 0, spannableString.Length(), SpanTypes.ExclusiveExclusive);

                    menuItem.SetTitle(spannableString);
                }
            }

        }

    }
}