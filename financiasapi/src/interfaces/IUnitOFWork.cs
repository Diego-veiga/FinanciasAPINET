
namespace financias.src.interfaces
{
    public interface IUnitOFWork
    {
        IUserRepository userRepository { get; }
        IBanckRepository banckRepository { get; }
        IBanckAccountRepository banckAccountRepository { get; }
        IUserBancksAccountsRepository userBancksAccountsRepository { get; }
        Task Commit();
    }
}