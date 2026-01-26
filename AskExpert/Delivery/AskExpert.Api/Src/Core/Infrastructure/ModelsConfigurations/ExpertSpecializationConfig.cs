namespace AskExperts.Infrastructure.ModelsConfigurations
{
    public class ExpertSpecializationConfig : IEntityTypeConfiguration<ExpertSpecialization>
    {
        public void Configure(EntityTypeBuilder<ExpertSpecialization> builder)
        {
            builder
                .HasKey(x => new { x.ExpertId, x.SpecializationId });
        }
    }
}
