using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Syncfusion.Maui.Core.Hosting;

namespace IronPlus;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Secrets.SyncfusionKey);
		
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCompatibility()
			.UseSentry(options =>
			{
				options.Dsn = Secrets.SentryDsn;
				options.EnableLogs = true;
				options.EnableMetrics = true;
				options.SendDefaultPii = true;
				options.IncludeTextInBreadcrumbs = false;
				options.IncludeTitleInBreadcrumbs = false;
				options.IncludeBackgroundingStateInBreadcrumbs = false;
#if DEBUG
				options.Debug = true;
				options.TracesSampleRate = 1.0;
#else
				options.TracesSampleRate = 0.2;
#endif
			})
			.ConfigureSyncfusionCore()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
