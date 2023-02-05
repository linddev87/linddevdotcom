using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;


namespace Infrastructure{
    public class AppContext : DbContext
    {
        DbSet<JournalEntry> JournalEntries {get; set;}
    }
};

