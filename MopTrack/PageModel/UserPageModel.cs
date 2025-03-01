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

    #region create user propertys
    [ObservableProperty] private string firstnameField;
    [ObservableProperty] private string surnameField;
    [ObservableProperty] private float dailyworktimeField;
    [ObservableProperty] private int vacationsField;
    [ObservableProperty] private byte[] signaturField;
    #endregion


    public UserPageModel(DatabaseManager db)
    {
        manager = db;
        OnLoadData();
    }
    private async void OnLoadData()
    {
        UserList = new ObservableCollection<UserModel>(await manager.OnGetUserlist());
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
            //UserList.Remove(user);
        }
    }
    [RelayCommand]
    private async void OnButtonSearchSignatur()
    {
        var result = await MediaPicker.PickPhotoAsync();

        if(result != null)
        {
            var stream = await result.OpenReadAsync();

            byte[] imageBytes;

            using(var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
            }

            SignaturField = imageBytes;
        }
    }

    [RelayCommand]
    private async void OnButtonSaveNewUser()
    {
        if(string.IsNullOrEmpty(FirstnameField))
        {
            await Shell.Current.DisplayAlert("Fehler","Bitte gebe einen Vornamen ein...!","Ok");
            return;
        }
        if(string.IsNullOrEmpty(SurnameField))
        {
            await Shell.Current.DisplayAlert("Fehler", "Bitte gebe einen Nachnamen ein...!","Ok");
            return;
        }
        if(DailyworktimeField <= 0)
        {
            await Shell.Current.DisplayAlert("Fehler", "Bitte gebe die Arbeitszeit am Tag an...!","Ok");
            return;
        }
        if(VacationsField <= 0)
        {
            await Shell.Current.DisplayAlert("Fehler", "Bitte gebe die Anzahl von Urlaubstagen an...!","Ok");
            return;
        }
        //Unterschrift muss nicht gefüllt sein

        UserModel newUser = new UserModel
        {
            Id = Guid.NewGuid(),
            Firstname = FirstnameField,
            Surname = SurnameField,
            WorkTimeDaily = DailyworktimeField,
            VacationDays = VacationsField,
            SignPath = SignaturField
        };

        if(await manager.OnCreateUser(newUser))
        {
            UserList.Add(newUser);
        }
    }
}
