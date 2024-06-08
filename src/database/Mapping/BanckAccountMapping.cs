using financias.src.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace financias.src.database.Mapping
{
    public class BanckAccountMapping : IEntityTypeConfiguration<BanckAccount>
    {
        public void Configure(EntityTypeBuilder<BanckAccount> builder)
        {
            builder.HasKey(ba => ba.Id);

            builder.Property(ba => ba.Balance).HasDefaultValue(0);
            builder.HasOne(ba => ba.Banck)
                    .WithMany(ba => ba.BanckAccounts)
                    .HasForeignKey(ca => ca.BanckId);
            
        }
    }
}