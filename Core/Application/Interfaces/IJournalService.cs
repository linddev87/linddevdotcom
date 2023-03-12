using Core.Application.Models;
using Core.Domain.Entities;

namespace Core.Application.Interfaces{

    public interface IJournalService{
        public Task<JournalEntry> GetById(int id);
        public Task<IEnumerable<JournalEntry>> List();
        public Task<JournalEntry> Create(UpsertJournalEntryRequest entry);
        public Task<JournalEntry> Update(int id, UpsertJournalEntryRequest entry);
        public Task<bool> DeleteById(int id);
    }
}