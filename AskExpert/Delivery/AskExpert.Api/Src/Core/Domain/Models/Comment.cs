using System.ComponentModel.DataAnnotations.Schema;

namespace AskExperts.Domain.Models
{
	public class Comment : BaseEntity<Guid>
	{
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public string Body { get; set; }
        public string? Attachment { get; set; }
		public Guid? ParentId { get; set; }
		[ForeignKey("ParentId")]
		public virtual ICollection<Comment> Replies { get; set; }
	}
}
