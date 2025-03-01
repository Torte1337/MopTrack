using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MopTrack.Manager;
using MopTrack.Model;

namespace MopTrack.PageModel;

public partial class UserPageModel : ObservableObject
{
    private readonly DatabaseManager manager;
    [ObservableProperty] private ObservableCollection<UserModel> userList;


    public UserPageModel(DatabaseManager db)
    {
        UserList = new ObservableCollection<UserModel>();
        UserList.Add(new UserModel() {Id=Guid.NewGuid(), Name="testvorname"});
        UserList.Add(new UserModel() {Id=Guid.NewGuid(), Name="testvorname2"});
        UserList.Add(new UserModel() {Id=Guid.NewGuid(), Name="testvorname3"});
        manager = db;
    }

    [RelayCommand]
    private async void OnButtonEditUser(UserModel user)
    {
        var result = await Shell.Current.DisplayPromptAsync("Namen ändern","","Speichern","Abbrechen","Name eingeben...");

        if(!string.IsNullOrEmpty(result))
        {
            // Neuer name muss in der datenbank gespeichert werden (User überschreiben)
        }
    }
    [RelayCommand]
    private async void OnButtonDeleteUser(UserModel user)
    {
        var result = await Shell.Current.DisplayActionSheet("Wirklich löschen?","Abbrechen","löschen");

        if(!string.IsNullOrEmpty(result))
        {
            //user aus der DB entfernen

            //liste aktualisieren
            UserList.Remove(user);
        }
    }
}
