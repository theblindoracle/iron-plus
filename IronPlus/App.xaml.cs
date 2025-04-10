using Microsoft.Maui.Controls;

namespace IronPlus;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
