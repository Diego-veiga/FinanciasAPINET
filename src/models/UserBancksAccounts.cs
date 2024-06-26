
namespace financias.src.models
{
    public class UserBancksAccounts : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid BanckAccountId { get; set; }
        public User User { get; set; }
        public BanckAccount BanckAccount { get; set; }
        public bool IsAdmin { get; set; }
    }
}