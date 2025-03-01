using System;
using Microsoft.EntityFrameworkCore;
using MopTrack.Model;

namespace MopTrack.Context;

public class MTContext : DbContext
{
    public DbSet<UserModel> users { get; set; }
    public DbSet<WorkItemModel> workitems { get; set; }
    public DbSet<ObjectModel> objectModels { get; set; }

    public MTContext(DbContextOptions<MTContext> options) : base(options){ }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    

}
