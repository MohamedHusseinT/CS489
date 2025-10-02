using System.ComponentModel.DataAnnotations;

namespace MohamedElibrary.Models
{
    public class PublisherPublisher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<PublisherName> PublisherNames { get; set; } = new List<PublisherName>();
    }
}
