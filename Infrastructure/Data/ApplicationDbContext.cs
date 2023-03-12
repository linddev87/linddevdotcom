using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<JournalEntry> JournalEntries {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder options){ 
            options.UseSqlite($"Data Source='/Users/simonlauridsen/Projects/LinddevCom/Infrastructure/sqlite.db'"); 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
};

