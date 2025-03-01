using MopTrack.Manager;

namespace MopTrack;

public partial class App : Application
{
	private readonly DatabaseManager mgr;
	public App(DatabaseManager dbManager)
	{
		InitializeComponent();
		mgr = dbManager;
		Task.Run(async () => await OnCheckDatabase());

#if ANDROID
	Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry),(handler,view) => 
	{
		var platformView = handler.PlatformView;

		platformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
	});

	Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(Picker),(handler,view) => 
	{
		var platformView = handler.PlatformView;

		platformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
	});
#endif
	}

	private async Task OnCheckDatabase()
	{
		await mgr.OnCheck();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}