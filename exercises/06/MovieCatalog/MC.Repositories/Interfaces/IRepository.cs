using MC.Data.Entities;

namespace MC.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(bool isActive = true);

        Task<T> GetByIdAsync(int id, bool isActive = true);

        void Insert(T entity);

        void Update(T entity, string excludeProperties = "");

        void ActivateDeactivate(T entity);

        void ActivateDeactivate(int id);

        void Delete(T entity);

        void Delete(int id);
    }
}
