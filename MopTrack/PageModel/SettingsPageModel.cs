using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MopTrack.PageModel;

public partial class SettingsPageModel : ObservableObject
{
    [RelayCommand]
    private async void OnButtonExportDatabase()
    {
        
    }
    [RelayCommand]
    private async void OnButtonImportDatabase()
    {

    }
    [RelayCommand]
    private async void OnButtonResetDatabase()
    {

    }

    [RelayCommand]
    private async void OnButtonUserManagement()
    {
        await Shell.Current.GoToAsync("//UserPage");
    }


}
