using financias.src.models.Enums;

namespace financias.src.models
{
    public class BanckAccount : BaseEntity
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public AccountType Type { get; set; }
        public Guid BanckId { get; set; }
        public Banck Banck { get; set; }
        public ICollection<UserBancksAccounts> UserBancksAccounts { get; set; }

    }
}