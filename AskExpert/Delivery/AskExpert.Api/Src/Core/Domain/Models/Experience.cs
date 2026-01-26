namespace AskExperts.Domain.Models
{
	public class Experience : BaseEntity<Guid>
	{
		public string ExperienceName { get; set; }
		public int ExperienceYears { get; set; }
        public Guid SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
		public string JobLocation { get; set; }
        public Guid ExpertId { get; set; }
		public Expert Expert { get; set; }
	}
}
