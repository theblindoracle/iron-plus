using System;
using IronPlus.Interfaces;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;


namespace IronPlus.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel(IDialogService dialogService, ISettingsService settingsService) : base(dialogService, settingsService)
        {
            Title = "About";

            Header1 = "About the Developer";
            Body1 =
                "My name is Travis Napier. I'm a software developer, and I've been competing in powerlifting since 2019. I built IronPlus as a personal project to solve problems I was running into in my own training.";
            Body2 =
                "As other lifters found it useful too, it grew from a personal tool into something worth sharing. IronPlus is built by a powerlifter, for powerlifters — every calculator comes from a real need in the gym, not guesswork.";
            Body3 =
                "Have a feature request or found a bug? I'd love to hear from you — reach out at support@napes.dev or on Instagram, @napes.lifts.";
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

        string body2;
        public string Body2
        {
            get => body2;
            set => SetProperty(ref body2, value);
        }

        string body3;
        public string Body3
        {
            get => body3;
            set => SetProperty(ref body3, value);
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
                Services.AnalyticsService.Track_App_Exception(ex.Message);
            }
        });
    }
}