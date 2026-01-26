namespace AskExperts.Infrastructure.ModelsConfigurations
{
    public class ExpertConfig : IEntityTypeConfiguration<Expert>
    {
        public void Configure(EntityTypeBuilder<Expert> builder)
        {
            builder.HasMany(a => a.Experiences)
                    .WithOne(a => a.Expert)
                    .HasForeignKey(a => a.ExpertId);

            builder.HasMany(a => a.ExpertQuestions)
                    .WithOne(a => a.Expert)
                    .HasForeignKey(a => a.ExpertId);

            builder.HasMany(a => a.ExpertSpecializations)
                    .WithOne(a => a.Expert)
                    .HasForeignKey(a => a.SpecializationId);
        }
    }
}
