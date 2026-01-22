using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public static readonly ILoggerFactory EfLoggerFactory =
            LoggerFactory.Create(builder =>
            {
                builder
                    .AddSerilog()
                    .SetMinimumLevel(LogLevel.Information);
            });

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            SavingChanges += AppDbContext_SavingChanges;
            SavedChanges += AppDbContext_SavedChanges;
            SaveChangesFailed += AppDbContext_SaveChangesFailed;

            ChangeTracker.Tracked += ChangeTracker_Tracked;
            ChangeTracker.StateChanged += ChangeTracker_StateChanged;
        }

        public DbSet<Employee> Employees => Set<Employee>();

        #region EF Event Handlers (POC)

        private void AppDbContext_SavingChanges(object? sender, SavingChangesEventArgs e)
        {
            Console.WriteLine("[EF EVENT] SavingChanges");
        }

        private void AppDbContext_SavedChanges(object? sender, SavedChangesEventArgs e)
        {
            Console.WriteLine($"[EF EVENT] SavedChanges - EntitiesSaved: {e.EntitiesSavedCount}");
        }

        private void AppDbContext_SaveChangesFailed(object? sender, SaveChangesFailedEventArgs e)
        {
            Console.WriteLine($"[EF EVENT] SaveChangesFailed - {e.Exception?.Message}");
        }

        private void ChangeTracker_Tracked(object? sender, EntityTrackedEventArgs e)
        {
            Console.WriteLine($"[EF EVENT] Tracked - {e.Entry.Entity?.GetType().Name} State={e.Entry.State}");
        }

        private void ChangeTracker_StateChanged(object? sender, EntityStateChangedEventArgs e)
        {
            Console.WriteLine($"[EF EVENT] StateChanged - {e.Entry.Entity?.GetType().Name} {e.OldState} => {e.NewState}");
        }

        #endregion
    }
}
