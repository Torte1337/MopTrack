using MopTrack.Page;

namespace MopTrack;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(OverviewPage),typeof(OverviewPage));
		Routing.RegisterRoute(nameof(SettingsPage),typeof(SettingsPage));
		Routing.RegisterRoute(nameof(UserPage),typeof(UserPage));
	}
}
