

namespace financias.src.interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task<T> GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}