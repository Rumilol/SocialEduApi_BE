

using Bogus;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.Entities
{
    public class ProjectSubmission
    {
        public int Id { get; set; }
        public int? SubjectID { get; set; }
        public Subject? Subject { get; set; }
        public int? ProjectTaskID { get; set; }
        public ProjectTask? ProjectTask { get; set; }
        public string UserID { get; set; }
        public ApplicationUser? User { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string? ImageLink { get; set; }
        public float? Grade { get; set; }
        public DateTime UploadDate { get; set; }

        public static Faker<ProjectSubmission> GetFaker(List<ProjectTask> tasks, List<string> userIDs, int subjectID)
        {
            return new Faker<ProjectSubmission>("hr")
                .RuleFor(x => x.SubjectID, y => subjectID)
                .RuleFor(x => x.ProjectTask, y => y.PickRandom(tasks))
                .RuleFor(x => x.UserID, y => y.PickRandom(userIDs))
                .RuleFor(x => x.Title, y => y.Hacker.Noun() + " " + y.Hacker.Noun())
                .RuleFor(x => x.Description, y => y.Lorem.Paragraph())
                .RuleFor(x => x.Link, y => "https://github.com/")
                .RuleFor(x => x.ImageLink, y => y.Random.Bool(0.1f) ? y.Image.LoremFlickrUrl() : "")
                .RuleFor(x => x.Grade, y => y.Random.Int(1, 50))
                .RuleFor(x => x.UploadDate, y => y.Date.Between(DateTime.Today.AddYears(-1), DateTime.Today));
        }

    }
}
