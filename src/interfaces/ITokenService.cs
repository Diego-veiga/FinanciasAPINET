
using financias.src.DTOs;

namespace financias.src.interfaces
{
    public interface ITokenService
    {
        Task<string> Generate(UserView user);
    }
}