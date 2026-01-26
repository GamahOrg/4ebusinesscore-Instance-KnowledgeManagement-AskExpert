namespace AskExperts.Domain.Models
{
    public class ExpertSpecialization : BaseEntity<Guid>
    {
        public Guid ExpertId { get; set; }

        public Expert Expert { get; set; }
        public Guid SpecializationId { get; set; }

        public Specialization Specialization { get; set; }




    }
}
