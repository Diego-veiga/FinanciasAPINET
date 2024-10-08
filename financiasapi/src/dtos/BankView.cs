namespace financiasapi.src.dtos
{
    public class BankView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public Guid UserId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}