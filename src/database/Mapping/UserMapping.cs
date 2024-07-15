using financias.src.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace financias.src.database.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(150);
            builder.HasMany(u => u.Bancks)
                    .WithOne(u => u.User)
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Cascade);


        }
    }
}