namespace financias.src.interfaces
{
    public interface IUnitOFWork
    {
        IUserRepository userRepository { get; }
        IBankRepository bankRepository { get; }
        IBankAccountRepository bankAccountRepository { get; }
        IUserBanksAccountsRepository userBanksAccountsRepository { get; }
        Task Commit();
    }
}