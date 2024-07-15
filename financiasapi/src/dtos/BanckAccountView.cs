using financiasapi.src.dtos;
using financiasapi.src.models.Enums;

namespace financiasapi.src
{
    public class BanckAccountView: BaseEntityView
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public AccountType Type { get; set; }
        public Guid BanckId { get; set; }   
    }
}