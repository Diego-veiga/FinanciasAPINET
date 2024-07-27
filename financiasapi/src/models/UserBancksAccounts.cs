namespace financiasapi.src.models
{
    public class UserBancksAccounts:BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid BankAccountId { get; set; }
        public User User { get; set; }
        public BankAccount BankAccount { get; set; }
        public bool IsAdmin { get; set; }
    }
}