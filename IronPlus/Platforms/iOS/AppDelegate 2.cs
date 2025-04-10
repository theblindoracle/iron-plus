using Foundation;
using IronPlus.Services;
using IronPlus.ViewModels;
using IronSport.iOS.Services;
using Syncfusion.SfNumericEntry.XForms.iOS;
using Syncfusion.SfNumericUpDown.XForms.iOS;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.TextInputLayout;
using UIKit;

namespace IronSport.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

#if DEBUG
            // Inititializes with Beta Key
            AppCenterService.Init(Constants.AppCenterKeys.IOS_BETA);
#endif
            AppCenterService.Init(Constants.AppCenterKeys.IOS_PROD);

            ViewModelLocator.RegisterSingleton<IPlatformService, PlatformService>();

            global::Xamarin.Forms.Forms.Init();

            SfTextInputLayoutRenderer.Init();
            SfNumericEntryRenderer.Init();
            SfSegmentedControlRenderer.Init();
            SfNumericUpDownRenderer.Init();

            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("Rubik", 12) }, UIControlState.Normal);
            UITabBarItem.Appearance.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("Rubik", 14) }, UIControlState.Selected);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
