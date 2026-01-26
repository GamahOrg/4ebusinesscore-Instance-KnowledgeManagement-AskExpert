namespace AskExperts.Infrastructure.ModelsConfigurations
{
	public class ExpertQuestionConfig:IEntityTypeConfiguration<ExpertQuestion>
	{
		public void Configure(EntityTypeBuilder<ExpertQuestion> builder)
		{
			builder
				.HasKey(x => new { x.ExpertId,x.QuestionId });
		}
	}
}
