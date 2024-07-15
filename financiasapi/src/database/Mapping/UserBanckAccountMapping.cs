using financiasapi.src.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace financias.src.database.Mapping
{
    public class UserBanckAccountMapping : IEntityTypeConfiguration<UserBancksAccounts>
    {
        public void Configure(EntityTypeBuilder<UserBancksAccounts> builder)
        {
            builder.HasKey(uba => uba.Id);
            builder.HasOne(uba => uba.BanckAccount)
                   .WithMany(uba =>uba.UserBancksAccounts)
                   .HasForeignKey(uba => uba.BanckAccountId);
           
            builder.HasOne(uba => uba.User)
                   .WithMany(uba => uba.UserBancksAccounts)
                   .HasForeignKey(uba => uba.UserId);
            builder.Property(uba=>uba.IsAdmin).HasDefaultValue(false);
        
        }
    }
}