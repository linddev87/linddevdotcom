using Core.Domain.Entities;

namespace Core.Application.Interfaces{

    public interface IJournalService{
        public Task<JournalEntry> GetEntryById(int id);
        public Task<IEnumerable<JournalEntry>> GetAllEntries();
        public Task<JournalEntry> CreateEntry(JournalEntry entry);
        public Task<JournalEntry> UpdateEntry(JournalEntry entry);
        public Task<bool> DeleteEntryById(int id);
    }
}