namespace financiasapi.src.dtos
{
    public class BaseEntityView
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }   
    }
}