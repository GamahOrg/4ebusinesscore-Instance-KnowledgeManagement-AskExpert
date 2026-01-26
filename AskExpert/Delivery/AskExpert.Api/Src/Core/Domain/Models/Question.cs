namespace AskExperts.Domain.Models
{
	public class Question : BaseEntity<Guid>
	{
        public Guid SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public string Body { get; set; }
        public QuestionStatus QuestionStatus { get; set; }
        public ICollection<ExpertQuestion> ExpertQuestions { get; set; }
        public ICollection<Comment> Comments { get; set; }
		public string? Notes { get; set; }
		//public RequestStage RequestStage { get; set; }
		public int RequestStage { get; set; }

        public QuestionType Type { get; set; }
	}
}
