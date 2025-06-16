using IronPlus.Views;
using Microsoft.Maui.Controls;

namespace IronPlus;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("showSpecificWeight", typeof(ShowSpecificWeightPage));
		Routing.RegisterRoute("howToUseRpeChart", typeof(HowToUseRpeChartPage));
		Routing.RegisterRoute("howToUseUnitConversion", typeof(HowToUseUnitConversionPage)); 
		Routing.RegisterRoute("howToUseWarmUpCalculation", typeof(HowToUseWarmUpCalculationPage));
		Routing.RegisterRoute("about", typeof(AboutPage));
		Routing.RegisterRoute("themeSettings", typeof(ThemeSettingsPage));
		Routing.RegisterRoute("barbellSettings", typeof(BarbellSettingsPage));
		Routing.RegisterRoute("addBarbellDetails", typeof(AddBarbellDetailsPage));
		Routing.RegisterRoute("rpeChartSettings", typeof(RPEChartSettingsPage));
		Routing.RegisterRoute("generalSettings", typeof(GeneralSettingsPage));
	}
}