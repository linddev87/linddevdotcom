using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Interfaces;

namespace Core.Application.Services {
    class JournalService : IJournalService
    {
        private readonly IGenericRepository<JournalEntry> _journalRepo;

        public JournalService(IGenericRepository<JournalEntry> journalRepo)
        {
            _journalRepo = journalRepo;
        }

        public async Task<JournalEntry> CreateEntry(JournalEntry entry)
        {
            var res = await _journalRepo.Create(entry);
            return res;
        }

        public async Task<bool> DeleteEntryById(int id)
        {
            var res = await _journalRepo.Delete(id);
            return res;
        }

        public async Task<IEnumerable<JournalEntry>> GetAllEntries()
        {
            var res = await _journalRepo.GetAll();
            return res;
        }

        public async Task<JournalEntry> GetEntryById(int id)
        {
            var res = await _journalRepo.GetById(id);
            return res;
        }

        public async Task<JournalEntry> UpdateEntry(JournalEntry entry)
        {
            var res = await _journalRepo.Update(entry);
            return res;
        }
    }
}