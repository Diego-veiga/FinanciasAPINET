using financias.src.database.Mapping;
using financias.src.models;
using Microsoft.EntityFrameworkCore;

namespace financias.src.database.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Banck> Bancks { get; set; }
        public DbSet<BanckAccount> BanckAccounts { get; set; }
        public DbSet<UserBancksAccounts> UserBancksAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new BanckMapping());
            modelBuilder.ApplyConfiguration(new BanckAccountMapping());
            modelBuilder.ApplyConfiguration(new UserBanckAccountMapping());
        }

    }
}