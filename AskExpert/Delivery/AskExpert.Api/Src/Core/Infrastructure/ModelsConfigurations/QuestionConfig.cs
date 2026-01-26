namespace AskExperts.Infrastructure.ModelsConfigurations
{
	public class QuestionConfig:IEntityTypeConfiguration<Question>
	{
		public void Configure(EntityTypeBuilder<Question> builder)
		{
			builder.HasMany(a => a.ExpertQuestions)
					.WithOne(a => a.Question)
					.HasForeignKey(a => a.QuestionId);

			builder.HasMany(a => a.Comments)
					.WithOne(a => a.Question)
					.HasForeignKey(a => a.QuestionId);
		}
	}
}
