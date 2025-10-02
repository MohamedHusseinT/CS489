using System.ComponentModel.DataAnnotations;

namespace MohamedElibrary.Models
{
    public class PublisherName
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
        public int PublisherId { get; set; }
        public virtual PublisherPublisher Publisher { get; set; } = null!;
        public virtual ICollection<PublishingCompany_PublisherName> PublishingCompany_PublisherNames { get; set; } = new List<PublishingCompany_PublisherName>();
    }
}
