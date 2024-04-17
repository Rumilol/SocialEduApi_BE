

using Bogus;

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

        public static Faker<Subject> GetFaker(int institutionID)
        {
            return new Faker<Subject>("hr")
                .RuleFor(x => x.Name, y => y.Name.JobTitle())
                .RuleFor(x => x.Abbreviation, y => y.Lorem.Letter(5))
                .RuleFor(x => x.Description, y => y.Lorem.Paragraph())
                .RuleFor(x => x.PageBackground, y => "")
                .RuleFor(x => x.InstitutionID, y => institutionID)
                .RuleFor(x => x.CreatedDate, y => y.Date.Between(DateTime.Today.AddYears(-1), DateTime.Today));
        }
    }
}
