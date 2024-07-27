using financias.src.database.Mapping;
using financiasapi.src.models;
using Microsoft.EntityFrameworkCore;

namespace financias.src.database.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<UserBancksAccounts> UserBancksAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new BankMapping());
            modelBuilder.ApplyConfiguration(new BankAccountMapping());
            modelBuilder.ApplyConfiguration(new UserBanckAccountMapping());
        }

    }
}