namespace AskExperts.Domain.Models
{
	public class ExpertQuestion : BaseEntity<Guid>
	{
        public Guid ExpertId { get; set; }
        public Expert Expert { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
