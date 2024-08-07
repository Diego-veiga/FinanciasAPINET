using financiasapi.src.dtos;

namespace financias.src.interfaces
{
    public interface ITokenService
    {
        Task<string> Generate(UserView user);
    }
}