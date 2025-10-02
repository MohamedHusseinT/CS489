using Microsoft.EntityFrameworkCore;
using MohamedElibrary.Models;

namespace MohamedElibrary.Data
{
    public class MohamedElibraryDbContext : DbContext
    {
        public MohamedElibraryDbContext(DbContextOptions<MohamedElibraryDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<PublisherName> PublisherNames { get; set; }
        public DbSet<PublisherPublisher> PublisherPublishers { get; set; }
        public DbSet<PublishingCompany> PublishingCompanies { get; set; }
        public DbSet<PublishingCompany_PublisherName> PublishingCompany_PublisherNames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Data equivalent to Spring Boot data.sql
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Bui Lam Nam" },
                new Author { Id = 2, Name = "Tran Dao Son" },
                new Author { Id = 3, Name = "Ham Quang Thai" },
                new Author { Id = 4, Name = "Tran Hoai Mai" },
                new Author { Id = 5, Name = "Ngo Vinh Long" }
            );

            modelBuilder.Entity<PublisherPublisher>().HasData(
                new PublisherPublisher { Id = 1, Name = "Bui Lam Nam Dong Tac Gia" },
                new PublisherPublisher { Id = 2, Name = "Han Song" },
                new PublisherPublisher { Id = 3, Name = "Chieu Mai" },
                new PublisherPublisher { Id = 4, Name = "Tran Dao Vu Tong" },
                new PublisherPublisher { Id = 5, Name = "Phung Mai Xuyen" }
            );

            modelBuilder.Entity<PublishingCompany>().HasData(
                new PublishingCompany { Id = 1, Name = "Dong Tac Gia Publication" },
                new PublishingCompany { Id = 2, Name = "Han Song Publication" },
                new PublishingCompany { Id = 3, Name = "Chieu Mai Publication" }
            );

            modelBuilder.Entity<PublisherName>().HasData(
                new PublisherName { Id = 1, Name = "Bui Lam Nam Dong Tac Gia", Role = "Editor", PublisherId = 1 },
                new PublisherName { Id = 2, Name = "Han Song", Role = "Publisher", PublisherId = 2 },
                new PublisherName { Id = 3, Name = "Chieu Mai", Role = "Editor", PublisherId = 3 }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book 
                { 
                    Id = 1, 
                    Isbn = "978-0134685950", 
                    Title = "Effective Java", 
                    DatePublished = new DateTime(2018, 1, 1),
                    PublisherPublisherId = 1,
                    AuthorId = 1,
                    PublishingCompanyId = 1
                },
                new Book 
                { 
                    Id = 2, 
                    Isbn = "978-0132143013", 
                    Title = "Core Java Volume I", 
                    DatePublished = new DateTime(2019, 2, 1),
                    PublisherPublisherId = 2,
                    AuthorId = 2,
                    PublishingCompanyId = 2
                },
                new Book 
                { 
                    Id = 3, 
                    Isbn = "978-0596009205", 
                    Title = "Spring in Action", 
                    DatePublished = new DateTime(2020, 3, 1),
                    PublisherPublisherId = 3,
                    AuthorId = 3,
                    PublishingCompanyId = 3
                }
            );

            // Configure relationships
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<PublisherPublisher>()
                .HasMany(p => p.PublisherNames)
                .WithOne(pn => pn.Publisher)
                .HasForeignKey(pn => pn.PublisherId);
        }
    }
}
