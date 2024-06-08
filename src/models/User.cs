using System.Collections.ObjectModel;

namespace financias.src.models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public Collection<Banck> Bancks { get; set; }
        public ICollection<UserBancksAccounts> UserBancksAccounts { get; set; }

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