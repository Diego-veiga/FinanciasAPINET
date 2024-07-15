

using financiasapi.src.models;

namespace financias.src.interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);

    }
}