
using System.Collections.ObjectModel;

namespace financiasapi.src.models
{
    public class User: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public Collection<Bank> Banks { get; set; }
        public ICollection<UserBanksAccounts> UserBanksAccounts { get; set; }

        public User(Guid id, string name, string email, string password, bool active, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Active = active;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}