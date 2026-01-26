namespace AskExperts.Domain.Models
{
    public class Expert : BaseEntity<Guid>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string? FourthName { get; set; }
        public string? ImgUrl { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Nationality { get; set; }
        public string? Address { get; set; }
        public string? InformationAboutExpert { get; set; }
        public bool IsExternalExpert { get; set; }
        public ICollection<Experience> Experiences { get; set; }
        public ICollection<ExpertSpecialization> ExpertSpecializations { get; set; }
        public ICollection<ExpertQuestion> ExpertQuestions { get; set; }
        public Guid? UserId { get; set; }
    }
}
