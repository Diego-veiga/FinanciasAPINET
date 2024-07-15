

namespace financias.src.models
{
    public class Banck : BaseEntity
    {
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<BanckAccount> BanckAccounts { get; set; }

    }
}