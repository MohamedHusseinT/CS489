using Microsoft.EntityFrameworkCore;
using CAMS.Shared.Models;

namespace CAMS.Shared.Data
{
    public class CamsDbContext : DbContext
    {
        public CamsDbContext(DbContextOptions<CamsDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Customer entity
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            });

            // Configure Account entity
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountId);
                entity.Property(e => e.AccountNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.AccountType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Balance).HasColumnType("decimal(18,2)");

                // Configure one-to-one relationship
                entity.HasOne(e => e.Customer)
                      .WithOne(e => e.Account)
                      .HasForeignKey<Account>(e => e.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Ensure AccountNumber is unique
                entity.HasIndex(e => e.AccountNumber).IsUnique();
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "Bob", LastName = "Jones" },
                new Customer { CustomerId = 2, FirstName = "Anna", LastName = "Smith" },
                new Customer { CustomerId = 3, FirstName = "Carlos", LastName = "Jimenez" }
            );

            // Seed Accounts
            modelBuilder.Entity<Account>().HasData(
                new Account 
                { 
                    AccountId = 1, 
                    AccountNumber = "AC1002", 
                    AccountType = "Checking", 
                    DateOpened = new DateTime(2022, 7, 10), 
                    Balance = 10900.50m, 
                    CustomerId = 2 
                },
                new Account 
                { 
                    AccountId = 2, 
                    AccountNumber = "AC1001", 
                    AccountType = "Savings", 
                    DateOpened = new DateTime(2021, 11, 15), 
                    Balance = 125.95m, 
                    CustomerId = 1 
                },
                new Account 
                { 
                    AccountId = 3, 
                    AccountNumber = "AC1003", 
                    AccountType = "Savings", 
                    DateOpened = new DateTime(2022, 7, 11), 
                    Balance = 15000m, 
                    CustomerId = 3 
                }
            );
        }
    }
}
