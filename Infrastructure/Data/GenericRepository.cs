using Core.Domain.Interfaces;
using Core;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data{
    public class GenericRepository<T> : IGenericRepository<T> where T: BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            var created = await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return created.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await dbSet.FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
                return false;

            dbSet.Remove(entity);

            var res = await _context.SaveChangesAsync();

            return res > 0 ? true : false;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var res = await dbSet.ToListAsync();

            return res;
        }

        public async Task<T> GetById(int id)
        {
            var entity = await dbSet.FirstOrDefaultAsync(e => e.Id == id);

            return entity ?? throw new NullReferenceException();
        }

        public async Task<T> Update(T entity)
        {
            var res = dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return res.Entity;
        }
    }
}