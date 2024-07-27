using financiasapi.src.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace financias.src.database.Mapping
{
    public class UserBankAccountMapping : IEntityTypeConfiguration<UserBanksAccounts>
    {
        public void Configure(EntityTypeBuilder<UserBanksAccounts> builder)
        {
            builder.HasKey(uba => uba.Id);
            builder.HasOne(uba => uba.BankAccount)
                   .WithMany(uba =>uba.UserBanksAccounts)
                   .HasForeignKey(uba => uba.BankAccountId);
           
            builder.HasOne(uba => uba.User)
                   .WithMany(uba => uba.UserBanksAccounts)
                   .HasForeignKey(uba => uba.UserId);
            builder.Property(uba=>uba.IsAdmin).HasDefaultValue(false);
        
        }
    }
}