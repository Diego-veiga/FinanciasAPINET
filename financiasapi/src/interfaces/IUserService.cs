using financiasapi.src.dtos;

namespace financias.src.interfaces
{
    public interface IUserService
    {
        Task Create(CreateUser createUser);
        Task Delete(DeleteUser deleteUser);
        Task Update(UpdateUser updateUser);
        Task<UserView> GetById(Guid id);
        Task<List<UserView>> GetActive();
        Task<string> Login(Login login);
    }
}