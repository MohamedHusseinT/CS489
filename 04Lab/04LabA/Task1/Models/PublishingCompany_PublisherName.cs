using System.ComponentModel.DataAnnotations;

namespace MohamedElibrary.Models
{
    public class PublishingCompany_PublisherName
    {
        [Key]
        public int Id { get; set; }
        public int PublishingCompanyId { get; set; }
        public virtual PublishingCompany PublishingCompany { get; set; } = null!;
        public int PublisherNameId { get; set; }
        public virtual PublisherName PublisherName { get; set; } = null!;
    }
}
