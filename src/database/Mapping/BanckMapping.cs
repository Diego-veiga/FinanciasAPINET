using financias.src.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace financias.src.database.Mapping
{
    public class BanckMapping : IEntityTypeConfiguration<Banck>
    {
        public void Configure(EntityTypeBuilder<Banck> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Cnpj)
                   .IsRequired()
                   .HasMaxLength(14);

            builder.HasOne(b => b.User)
                   .WithMany(b => b.Bancks)
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
            builder.HasData(
             new Banck
             {
                 Id = Guid.Parse("aff79997-841c-4cb4-a4e6-84d4f0e00e8d"),
                 Name = "Itau",
                 Cnpj = "60701190000104",
                 Active = true,
                 CreatedAt = DateTime.Now
             },
             new Banck
             {
                 Id = Guid.Parse("81a3902a-89b7-459e-a665-2a346ae829e1"),
                 Name = "Bradesco",
                 Cnpj = "60746948000112",
                 Active = true,
                 CreatedAt = DateTime.Now
             },
             new Banck
             {
                 Id = Guid.Parse("16d9075f-2ced-4358-be10-4cfeefac8647"),
                 Name = "Santander",
                 Cnpj = "90400888000142",
                 Active = true,
                 CreatedAt = DateTime.Now
             }
            );
        }
    }
}