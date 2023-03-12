using Core.Application.Interfaces;
using Core.Application.Models;
using Core.Domain.Entities;
using Core.Domain.Interfaces;

namespace Core.Application.Services {
    public class JournalService : IJournalService
    {
        private readonly IGenericRepository<JournalEntry> _journalRepo;

        public JournalService(IGenericRepository<JournalEntry> journalRepo)
        {
            _journalRepo = journalRepo;
        }

        public async Task<JournalEntry> Create(UpsertJournalEntryRequest entry)
        {
            var timeStamp = DateTime.UtcNow;

            var entity = new JournalEntry(){
                Title = entry.Title,
                Description = entry.Description,
                CreatedDate = timeStamp,
                ModifiedDate = timeStamp
            };

            var res = await _journalRepo.Create(entity);
            return res;
        }

        public async Task<bool> DeleteById(int id)
        {
            var res = await _journalRepo.Delete(id);
            return res;
        }

        public async Task<IEnumerable<JournalEntry>> List()
        {
            var res = await _journalRepo.GetAll();
            return res;
        }

        public async Task<JournalEntry> GetById(int id)
        {
            var res = await _journalRepo.GetById(id);
            return res;
        }

        public async Task<JournalEntry> Update(int id, UpsertJournalEntryRequest entry)
        {
            var existing = await _journalRepo.GetById(id);

            if (existing == null)
                throw new NullReferenceException();

            foreach(var prop in entry.GetType().GetProperties()){
                existing.GetType().GetProperty(prop.Name)!.SetValue(existing, prop.GetValue(entry));
            }

            existing.ModifiedDate = DateTime.UtcNow;

            var res = await _journalRepo.Update(existing);
            return res;
        }
    }
}