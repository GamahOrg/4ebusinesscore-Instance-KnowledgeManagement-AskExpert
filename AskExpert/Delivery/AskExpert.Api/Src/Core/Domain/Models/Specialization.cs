namespace AskExperts.Domain.Models
{
    public class Specialization : BaseEntity<Guid>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public string CSSClasses { get; set; }
        public ICollection<Experience> Experiences { get; set; }
        public ICollection<ExpertSpecialization> ExpertSpecializations { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
