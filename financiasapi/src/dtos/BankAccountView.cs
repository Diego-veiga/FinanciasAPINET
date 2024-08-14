using financiasapi.src.models.Enums;

namespace financiasapi.src.dtos
{
    public class BankAccountView: BaseEntityView
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public AccountType Type { get; set; }
        public Guid BankId { get; set; }   
    }
}