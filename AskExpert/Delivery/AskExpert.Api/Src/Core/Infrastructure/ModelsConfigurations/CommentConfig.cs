namespace AskExperts.Infrastructure.ModelsConfigurations
{
	public class CommentConfig:IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.HasMany(a => a.Replies)
				.WithOne()
				.HasForeignKey(a => a.ParentId).IsRequired(false);
		}
	}
}
