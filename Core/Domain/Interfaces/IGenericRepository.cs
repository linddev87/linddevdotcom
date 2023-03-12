using Core.Domain.Entities;

namespace Core.Domain.Interfaces {
    public interface IGenericRepository<T> where T : BaseEntity{
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        public Task<T> Create(T entity);
        public Task<T> Update(T entity);
        public Task<bool> Delete(int id);
    }
}