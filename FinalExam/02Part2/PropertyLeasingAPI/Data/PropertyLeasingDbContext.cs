using Microsoft.EntityFrameworkCore;
using PropertyLeasingAPI.Models;

namespace PropertyLeasingAPI.Data
{
    public class PropertyLeasingDbContext : DbContext
    {
        public PropertyLeasingDbContext(DbContextOptions<PropertyLeasingDbContext> options) : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Lease> Leases { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Lease>()
                .HasOne(l => l.Property)
                .WithMany(p => p.Leases)
                .HasForeignKey(l => l.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lease>()
                .HasOne(l => l.Tenant)
                .WithMany(t => t.Leases)
                .HasForeignKey(l => l.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure unique constraint for lease number
            modelBuilder.Entity<Lease>()
                .HasIndex(l => l.LeaseNumber)
                .IsUnique();

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Properties
            modelBuilder.Entity<Property>().HasData(
                new Property { PropertyId = 1, PropertyRefName = "Kilo Drive Palace", City = "Denver", State = "CO" },
                new Property { PropertyId = 2, PropertyRefName = "Galeria Court", City = "Dallas", State = "TX" },
                new Property { PropertyId = 3, PropertyRefName = "Bells Tower", City = "Houston", State = "TX" }
            );

            // Seed Tenants
            modelBuilder.Entity<Tenant>().HasData(
                new Tenant { TenantId = 1, FirstName = "Bradley", LastName = "Koopa", PhoneNumber = "(480) 123-1355", Email = null },
                new Tenant { TenantId = 2, FirstName = "Julie", LastName = "Weinthrope", PhoneNumber = "(414) 998-0112", Email = "jwein@mail.com" }
            );

            // Seed Leases
            modelBuilder.Entity<Lease>().HasData(
                new Lease { LeaseId = 1, LeaseNumber = 5121543109, StartDate = new DateTime(2023, 9, 17), EndDate = new DateTime(2024, 3, 17), MonthlyRentalRate = 950.00m, PropertyId = 1, TenantId = 2 },
                new Lease { LeaseId = 2, LeaseNumber = 7000511568, StartDate = new DateTime(2023, 10, 20), EndDate = new DateTime(2024, 10, 20), MonthlyRentalRate = 1075.99m, PropertyId = 1, TenantId = 2 },
                new Lease { LeaseId = 3, LeaseNumber = 6927458265, StartDate = new DateTime(2022, 12, 9), EndDate = new DateTime(2023, 12, 9), MonthlyRentalRate = 3945.50m, PropertyId = 2, TenantId = 1 }
            );
        }
    }
}




