using MopTrack.PageModel;

namespace MopTrack.Page;

public partial class UserPage : ContentPage
{
	public UserPage(UserPageModel pm)
	{
		InitializeComponent();
		BindingContext = pm;
	}
}