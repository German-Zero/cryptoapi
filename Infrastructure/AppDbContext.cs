using cryptoapi.Entity;
using Microsoft.EntityFrameworkCore;

namespace cryptoapi.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Crypto> Cryptos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.username).IsRequired();
                entity.Property(u => u.email).IsRequired();
                entity.Property(u => u.password).IsRequired();
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transactions");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Quantity).IsRequired();
                entity.Property(t => t.Money).IsRequired();
                entity.Property(t => t.Action).IsRequired();
                entity.Property(t => t.DateTime).IsRequired();

                entity.HasOne(t => t.User)
                    .WithMany(t => t.Transactions)
                    .HasForeignKey(t => t.UserId);
            });

            modelBuilder.Entity<Crypto>(entity =>
            {
                entity.ToTable("cryptos");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.CryptoCode).IsRequired();
                entity.Property(c => c.Amount).HasColumnType("decimal(18,8)");

                entity.HasOne(c => c.User)
                    .WithMany(u => u.Cryptos)
                    .HasForeignKey(c => c.UserId);
            });
        } 
    }
}
