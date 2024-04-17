

using Bogus;

namespace SocialEduApi.Models.Entities
{
    public class Forum
    {
        public int Id { get; set; }
        public int SubjectID { get; set; }
        public Subject? Subject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

        public static Faker<Forum> GetFaker(int subjectID)
        {
            return new Faker<Forum>("hr")
                .RuleFor(x => x.SubjectID, y => subjectID)
                .RuleFor(x => x.Name, y => y.Commerce.Product() + ": " + y.Commerce.ProductAdjective() + "?")
                .RuleFor(x => x.Description, y => y.Lorem.Paragraph())
                .RuleFor(x => x.CreatedDate, y => y.Date.Between(DateTime.Today.AddYears(-1), DateTime.Today));
        }
    }
}
