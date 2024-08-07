
namespace financiasapi.src.models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}