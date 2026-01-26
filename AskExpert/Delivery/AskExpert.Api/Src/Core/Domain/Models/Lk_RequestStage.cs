

namespace AskExperts.Domain.Models
{
    public class Lk_RequestStage : BaseEntity<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsSeed { get; set; }
    }
}
