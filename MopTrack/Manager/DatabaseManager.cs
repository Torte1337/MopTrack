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
        if(ctx == null)
            ctx = _mt;
    }
    public async Task OnCheck()
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
            await ctx.users.AddAsync(newUser);
            await ctx.SaveChangesAsync();

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
                userExist = editUser;
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

#region Workitems
    /// <summary>
    /// Methode lädt alle Workitems aus der Datenbank
    /// </summary>
    /// <returns></returns>
    public async Task<List<WorkItemModel>> OnGetAllWorkitems()
    {
        try
        {
            return await ctx.workitems.ToListAsync();
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Fehler", ex.Message,"Ok");
            return null;
        }
    }
    /// <summary>
    /// Methode lädt nur die Workitems, die von diesem Tag sind
    /// </summary>
    /// <param name="selectedDate"></param>
    /// <returns></returns>
    public async Task<List<WorkItemModel>> OnGetWorkItemsFromDay(DateTime selectedDate)
    {
        try
        {
            return null;
            //var itemList = await ctx.workitems.Where(x => x.)
        }
        catch(Exception ex)
        {
            return null;
        }
    }





#endregion




}