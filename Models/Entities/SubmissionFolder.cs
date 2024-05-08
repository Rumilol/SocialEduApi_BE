
using Bogus;
using Microsoft.EntityFrameworkCore;
using SocialEduApi.Data;
using SocialEduApi.Models.Identity;
using SocialEduApi.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialEduApi.Models.Entities
{
    public class SubmissionFolder
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public ApplicationUser? User { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [NotMapped]
        public List<ProjectSubmissionVM> Submissions { get; set; }

        public void GetSubmissions(ApplicationDbContext context)
        {
            var submissions = context.SubmissionFolderContainses
                .Include(p => p.Submission)
                .ThenInclude(p => p.User)
                .Include(p => p.Submission.Subject)
                .Include(p => p.Submission.Subject.Institution)
                .Include(p => p.Submission.ProjectTask)
                .Where(p => p.FolderID == Id)
                .Select(p => p.Submission!)
                .ToList();
            var users = context.Users.ToList().Select(p => new UserVM_short(p)).ToList();
            Submissions = submissions.Select(p => new ProjectSubmissionVM(p, users)).ToList();
        }

        public static Faker<SubmissionFolder> GetFaker(string userID)
        {
            return new Faker<SubmissionFolder>("hr")
                .RuleFor(x => x.UserID, y => userID)
                .RuleFor(x => x.Name, y => y.Hacker.Noun() + " " + y.Hacker.IngVerb())
                .RuleFor(x => x.Description, y => y.Lorem.Paragraph());
        }
    }
}
