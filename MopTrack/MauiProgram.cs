using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MopTrack.Context;
using MopTrack.Manager;
using MopTrack.Page;
using MopTrack.PageModel;

namespace MopTrack;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


		builder.Services.AddDbContext<MTContext>(options => options.UseSqlite(PathManager.OnGetDbPath()));


		builder.Services.AddSingleton<DatabaseManager>();

		builder.Services.AddSingleton<SettingsPageModel>();
		builder.Services.AddSingleton<UserPageModel>();


		builder.Services.AddTransient<SettingsPage>();
		builder.Services.AddTransient<UserPage>();











#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
