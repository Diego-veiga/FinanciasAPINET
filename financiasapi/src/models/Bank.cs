
namespace financiasapi.src.models
{
    public class Bank: BaseEntity
    {
       public string Cnpj { get; set; }
        public string Name { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; } 
    }
}