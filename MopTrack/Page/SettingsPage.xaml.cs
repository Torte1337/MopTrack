using MopTrack.PageModel;

namespace MopTrack.Page;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsPageModel pm)
	{
		InitializeComponent();
		BindingContext = pm;
	}
}