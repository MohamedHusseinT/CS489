using System.ComponentModel.DataAnnotations;

namespace MohamedElibrary.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Isbn { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = string.Empty;
        public DateTime DatePublished { get; set; }
        public int PublisherPublisherId { get; set; }
        public virtual PublisherPublisher PublisherPublisher { get; set; } = null!;
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; } = null!;
        public int PublishingCompanyId { get; set; }
        public virtual PublishingCompany PublishingCompany { get; set; } = null!;
    }
}
