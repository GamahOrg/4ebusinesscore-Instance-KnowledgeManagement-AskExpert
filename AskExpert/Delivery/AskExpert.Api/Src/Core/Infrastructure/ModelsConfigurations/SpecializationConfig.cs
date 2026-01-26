namespace AskExperts.Infrastructure.ModelsConfigurations
{
    public class SpecializationConfig : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasMany(a => a.Experiences)
                    .WithOne(a => a.Specialization)
                    .HasForeignKey(a => a.SpecializationId);

            builder.HasMany(a => a.Questions)
                    .WithOne(a => a.Specialization)
                    .HasForeignKey(a => a.SpecializationId);

            builder.HasMany(a => a.ExpertSpecializations)
                    .WithOne(a => a.Specialization)
                    .HasForeignKey(a => a.ExpertId);
        }
    }
}
