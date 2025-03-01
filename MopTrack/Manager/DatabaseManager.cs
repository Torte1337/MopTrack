using System;
using Microsoft.EntityFrameworkCore;
using MopTrack.Context;
using MopTrack.Model;

namespace MopTrack.Manager;

public class DatabaseManager
{
    private readonly MTContext ctx;
    public DatabaseManager(MTContext _mt)
    {
        ctx = _mt;
        OnCheck();
    }
    private async void OnCheck()
    {
        await OnCheckDatabaseExist();
    }
    private async Task<bool> OnCheckDatabaseExist()
    {
        try
        {
            await ctx.Database.EnsureCreatedAsync();
            return true;
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Fehler ", ex.Message,"Ok");
            return false;
        }
    }



#region User
    /// <summary>
    /// Methode lädt die Userliste aus der Datenbank
    /// </summary>
    /// <returns></returns>
    public async Task<List<UserModel>> OnGetUserlist()
    {
        try
        {
            return await ctx.users.ToListAsync();
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Fehler",ex.Message,"Ok");
            return null;
        }
    }
    /// <summary>
    /// Methode legt einen neuen User in der Datenbank an
    /// </summary>
    /// <param name="newUser"></param>
    /// <returns></returns>
    public async Task<bool> OnCreateUser(UserModel newUser)
    {
        try
        {
            var userExist = ctx.users.FirstOrDefaultAsync(x => x.Id == newUser.Id);

            if(userExist == null)
                await ctx.users.AddAsync(newUser);
            else
                return false;

            return true;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Fehler ", ex.Message,"Ok");
            return false;
        }
    }
    /// <summary>
    /// Methode bearbeitet einen User in der Datenbank
    /// </summary>
    /// <param name="editUser"></param>
    /// <returns></returns>
    public async Task<bool> OnEditUser(UserModel editUser)
    {
        try
        {
            var userExist = await ctx.users.FirstOrDefaultAsync(x => x.Id == editUser.Id);
            
            if(userExist != null)
                userExist.Name = editUser.Name;
            else
                return false;

            return true;
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Fehler",ex.Message,"Ok");
            return false;
        }
    }
    /// <summary>
    /// Methode löscht einen User aus der Datenbank
    /// </summary>
    /// <param name="deleteUser"></param>
    /// <returns></returns>
    public async Task<bool> OnDeleteUser(UserModel deleteUser)
    {
        try
        {
            var userExist = await ctx.users.FirstOrDefaultAsync(x => x.Id == deleteUser.Id);

            if(userExist != null)
                ctx.users.Remove(userExist);
            else
                return false;

            return true;
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Fehler", ex.Message,"Ok");
            return false;
        }
    }
#endregion





}