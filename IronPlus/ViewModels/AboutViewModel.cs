using System;
using IronPlus.Interfaces;


namespace IronPlus.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel(IDialogService dialogService, ISettingsService settingsService) : base(dialogService, settingsService)
        {
            Title = "About";

            Header1 = "About the Developer:";
            Body1 = "My name is Travis Napier and I am a software developer and a 110kg powerlifter. I started writing this app for myself as a personal project. I later found that this could be helpful for other lifters as well. This app is made by powerlifters, for powerlifters. If you have anything that you would like to see added, reach out to me at napesdotnet@gmail.com or on Instagram, @napier.lifts. ";
        }

        string header1;
        public string Header1
        {
            get => header1;
            set => SetProperty(ref header1, value);
        }

        string body1;
        public string Body1
        {
            get => body1;
            set => SetProperty(ref body1, value);
        }

        public string Version
        {
            get
            {
                return $"Version: {VersionTracking.CurrentVersion} ({VersionTracking.CurrentBuild})";
            }
        }

        Command goToInstagramCommand;
        public Command GoToInstagramCommand => goToInstagramCommand ??= new Command(async () =>
        {
            try
            {
                await Browser.OpenAsync("https://www.instagram.com/napes.lifts/");
            }
            catch (Exception ex)
            {
                Services.AppCenterService.Track_App_Exception(ex.Message);
            }
        });
    }
}
