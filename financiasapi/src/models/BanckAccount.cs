
using financiasapi.src.models.Enums;

namespace financiasapi.src.models
{
    public class BankAccount: BaseEntity
    {
         public string Name { get; set; }
        public double Balance { get; set; }
        public AccountType Type { get; set; }
        public Guid BankId { get; set; }
        public Bank Bank { get; set; }
        public ICollection<UserBanksAccounts> UserBanksAccounts { get; set; }

        public BankAccount(Guid id,string name, AccountType type, Guid bankId, bool active, DateTime createdAt, DateTime updatedAt)
        {
            Id= id;
            Name = name;
            Type = type;
            BankId = bankId;
            Active = active;
            CreatedAt =createdAt;
            UpdatedAt = updatedAt;
        }
    }
}