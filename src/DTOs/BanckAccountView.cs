

using financias.src.models.Enums;

namespace financias.src.DTOs
{
    public class BanckAccountView: BaseEntityView
    {
         public string Name { get; set; }
        public double Balance { get; set; }
        public AccountType Type { get; set; }
        public Guid BanckId { get; set; }    
    }
}