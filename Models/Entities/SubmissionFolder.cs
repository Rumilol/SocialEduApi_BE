
using Bogus;
using Microsoft.EntityFrameworkCore;
using SocialEduApi.Data;
using SocialEduApi.Models.Identity;

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


        public static Faker<SubmissionFolder> GetFaker(string userID)
        {
            return new Faker<SubmissionFolder>("hr")
                .RuleFor(x => x.UserID, y => userID)
                .RuleFor(x => x.Name, y => y.Hacker.Noun() + " " + y.Hacker.IngVerb())
                .RuleFor(x => x.Description, y => y.Lorem.Paragraph());
        }
    }
}
