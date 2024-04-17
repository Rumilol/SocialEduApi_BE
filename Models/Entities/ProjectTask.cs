

using Bogus;

namespace SocialEduApi.Models.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public int SubjectID { get; set; }
        public Subject? Subject { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Criteria { get; set; }
        public DateTime CreatedDate { get; set; }
        public float MaxGrade { get; set; }

        public static Faker<ProjectTask> GetFaker(int subjectID)
        {
            return new Faker<ProjectTask>("hr")
                .RuleFor(x => x.SubjectID, y => subjectID)
                .RuleFor(x => x.Title, y => y.Hacker.Verb() + " " + y.Hacker.Adjective() + " " + y.Hacker.Noun() + " " + y.Hacker.IngVerb() + " " + y.Commerce.ProductName())
                .RuleFor(x => x.Description, y => y.Lorem.Paragraph())
                .RuleFor(x => x.Criteria, y => $"10% - {y.Hacker.Noun()}\n30% - {y.Hacker.Noun()}\n60% - {y.Hacker.Noun()}")
                .RuleFor(x => x.CreatedDate, y => y.Date.Between(DateTime.Today.AddYears(-1), DateTime.Today))
                .RuleFor(x => x.MaxGrade, y => y.Random.Int(1, 10) * 10);
        }
    }
}
