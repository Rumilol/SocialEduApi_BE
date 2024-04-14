

namespace SocialEduApi.Models.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PageBackground { get; set; } = string.Empty;
        public int InstitutionID { get; set; }
        public Institution? Institution { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
