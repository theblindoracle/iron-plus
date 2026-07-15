using IronPlus.Interfaces;
using IronPlus.ViewModels;
using Microsoft.Maui.Controls;

namespace IronPlus;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		var settingsService = ViewModelLocator.Resolve<ISettingsService>();

		MainPage = new AppShell();
		UserAppTheme = settingsService.ThemeOption;
	}
}
