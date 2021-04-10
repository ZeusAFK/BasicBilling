using System;
using BasicBilling.Data.Abstracts;
using BasicBilling.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicBilling.Data.Contexts
{
  public class BasicBillingContext : DbContext
  {
    public BasicBillingContext(DbContextOptions<BasicBillingContext> opt) : base(opt)
    {

    }

    public DbSet<Bill> Bills { get; set; } = default!;
    public DbSet<Client> Clients { get; set; } = default!;
    public DbSet<Service> Services { get; set; } = default!;

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
      OnBeforeSaving();
      return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    private void OnBeforeSaving()
    {
      var entries = ChangeTracker.Entries();
      var utcNow = DateTime.UtcNow;

      foreach (var entry in entries)
      {
        if (entry.Entity is AbstractDatabaseEntity trackable)
        {
          switch (entry.State)
          {
            case EntityState.Modified:
              trackable.UpdatedOn = utcNow;
              entry.Property("CreatedOn").IsModified = false;
              break;
            case EntityState.Added:
              trackable.CreatedOn = utcNow;
              trackable.UpdatedOn = utcNow;
              break;
          }
        }
      }
    }
  }
}