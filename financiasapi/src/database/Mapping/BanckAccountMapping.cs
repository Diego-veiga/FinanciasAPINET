using financiasapi.src.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace financias.src.database.Mapping
{
    public class BankAccountMapping : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(ba => ba.Id);

            builder.Property(ba => ba.Balance).HasDefaultValue(0);
            builder.HasOne(ba => ba.Bank)
                    .WithMany(ba => ba.BankAccounts)
                    .HasForeignKey(ca => ca.BankId);
            
        }
    }
}