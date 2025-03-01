namespace MopTrack;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();


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

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}