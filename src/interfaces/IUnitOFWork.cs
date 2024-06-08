
namespace financias.src.interfaces
{
    public interface IUnitOFWork
    {
        IUserRepository userRepository { get; }
        IBanckRepository banckRepository { get; }
        Task Commit();
    }
}