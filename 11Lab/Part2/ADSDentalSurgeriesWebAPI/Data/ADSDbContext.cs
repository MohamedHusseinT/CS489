using Microsoft.EntityFrameworkCore;
using ADSDentalSurgeriesWebAPI.Models;

namespace ADSDentalSurgeriesWebAPI.Data
{
    /// <summary>
    /// Entity Framework DbContext for ADS Dental Surgeries system
    /// </summary>
    public class ADSDbContext : DbContext
    {
        public ADSDbContext(DbContextOptions<ADSDbContext> options) : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Surgery> Surgeries { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Dentist> Dentists { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints

            // Surgery -> Address (One-to-Many)
            modelBuilder.Entity<Surgery>()
                .HasOne(s => s.Address)
                .WithMany(a => a.Surgeries)
                .HasForeignKey(s => s.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient -> Address (One-to-Many)
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Address)
                .WithMany(a => a.Patients)
                .HasForeignKey(p => p.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment -> Patient (Many-to-One)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment -> Dentist (Many-to-One)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Dentist)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DentistId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment -> Surgery (Many-to-One)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Surgery)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.SurgeryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique constraints
            modelBuilder.Entity<Surgery>()
                .HasIndex(s => s.SurgeryNumber)
                .IsUnique();

            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.PatientNumber)
                .IsUnique();

            modelBuilder.Entity<Dentist>()
                .HasIndex(d => d.DentistNumber)
                .IsUnique();

            modelBuilder.Entity<Appointment>()
                .HasIndex(a => a.AppointmentNumber)
                .IsUnique();

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Addresses
            modelBuilder.Entity<Address>().HasData(
                new Address { AddressId = 1, Street = "123 Main Street", City = "Phoenix", State = "AZ", ZipCode = "85001" },
                new Address { AddressId = 2, Street = "456 Oak Avenue", City = "Phoenix", State = "AZ", ZipCode = "85002" },
                new Address { AddressId = 3, Street = "789 Pine Road", City = "Phoenix", State = "AZ", ZipCode = "85003" },
                new Address { AddressId = 4, Street = "321 Elm Street", City = "Phoenix", State = "AZ", ZipCode = "85004" },
                new Address { AddressId = 5, Street = "654 Maple Drive", City = "Phoenix", State = "AZ", ZipCode = "85005" }
            );

            // Seed Surgeries
            modelBuilder.Entity<Surgery>().HasData(
                new Surgery { SurgeryId = 1, SurgeryNumber = "S10", Name = "Phoenix Central Dental", PhoneNumber = "(602) 555-0101", AddressId = 1 },
                new Surgery { SurgeryId = 2, SurgeryNumber = "S13", Name = "Phoenix North Dental", PhoneNumber = "(602) 555-0102", AddressId = 2 },
                new Surgery { SurgeryId = 3, SurgeryNumber = "S15", Name = "Phoenix South Dental", PhoneNumber = "(602) 555-0103", AddressId = 3 }
            );

            // Seed Dentists
            modelBuilder.Entity<Dentist>().HasData(
                new Dentist { DentistId = 1, DentistNumber = "D001", FirstName = "Tony", LastName = "Smith", PhoneNumber = "(602) 555-0201", Email = "tony.smith@ads.com", Specialization = "General Dentistry" },
                new Dentist { DentistId = 2, DentistNumber = "D002", FirstName = "Helen", LastName = "Pearson", PhoneNumber = "(602) 555-0202", Email = "helen.pearson@ads.com", Specialization = "Orthodontics" },
                new Dentist { DentistId = 3, DentistNumber = "D003", FirstName = "Robin", LastName = "Plevin", PhoneNumber = "(602) 555-0203", Email = "robin.plevin@ads.com", Specialization = "Oral Surgery" }
            );

            // Seed Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient { PatientId = 1, PatientNumber = "P100", FirstName = "Gillian", LastName = "White", PhoneNumber = "(602) 555-0301", Email = "gillian.white@email.com", AddressId = 4 },
                new Patient { PatientId = 2, PatientNumber = "P105", FirstName = "Jill", LastName = "Bell", PhoneNumber = "(602) 555-0302", Email = "jill.bell@email.com", AddressId = 5 },
                new Patient { PatientId = 3, PatientNumber = "P108", FirstName = "Ian", LastName = "MacKay", PhoneNumber = "(602) 555-0303", Email = "ian.mackay@email.com", AddressId = 4 },
                new Patient { PatientId = 4, PatientNumber = "P110", FirstName = "John", LastName = "Walker", PhoneNumber = "(602) 555-0304", Email = "john.walker@email.com", AddressId = 5 }
            );

            // Seed Appointments (based on sample data)
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { AppointmentId = 1, AppointmentNumber = "A001", AppointmentDate = new DateTime(2013, 9, 12), AppointmentTime = new TimeSpan(10, 0, 0), PatientId = 1, DentistId = 1, SurgeryId = 3 },
                new Appointment { AppointmentId = 2, AppointmentNumber = "A002", AppointmentDate = new DateTime(2013, 9, 12), AppointmentTime = new TimeSpan(12, 0, 0), PatientId = 2, DentistId = 1, SurgeryId = 3 },
                new Appointment { AppointmentId = 3, AppointmentNumber = "A003", AppointmentDate = new DateTime(2013, 9, 12), AppointmentTime = new TimeSpan(10, 0, 0), PatientId = 3, DentistId = 2, SurgeryId = 1 },
                new Appointment { AppointmentId = 4, AppointmentNumber = "A004", AppointmentDate = new DateTime(2013, 9, 14), AppointmentTime = new TimeSpan(14, 0, 0), PatientId = 3, DentistId = 2, SurgeryId = 1 },
                new Appointment { AppointmentId = 5, AppointmentNumber = "A005", AppointmentDate = new DateTime(2013, 9, 14), AppointmentTime = new TimeSpan(16, 30, 0), PatientId = 2, DentistId = 3, SurgeryId = 3 },
                new Appointment { AppointmentId = 6, AppointmentNumber = "A006", AppointmentDate = new DateTime(2013, 9, 15), AppointmentTime = new TimeSpan(18, 0, 0), PatientId = 4, DentistId = 3, SurgeryId = 2 }
            );
        }
    }
}
