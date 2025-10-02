using System.ComponentModel.DataAnnotations;

namespace MohamedElibrary.Models
{
    public class PublishingCompany
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<PublishingCompany_PublisherName> PublishingCompany_PublisherNames { get; set; } = new List<PublishingCompany_PublisherName>();
    }
}
