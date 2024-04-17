
using Bogus;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.Entities
{
    public class UserOnSubject
    {
        public int Id { get; set; } 
        public string UserID { get; set; }
        public ApplicationUser? User { get; set; }
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
        public bool HasAdminRights { get; set; }
        public DateTime JoinedDate { get; set; }

        public static Faker<UserOnSubject> GetFaker(List<string> userIDs, int subjectID)
        {
            return new Faker<UserOnSubject>("hr")
                .RuleFor(x => x.UserID, y => y.PickRandom(userIDs))
                .RuleFor(x => x.SubjectID, y => subjectID)
                .RuleFor(x => x.HasAdminRights, y => y.Random.Bool(0.1f))
                .RuleFor(x => x.JoinedDate, y => y.Date.Between(DateTime.Today.AddYears(-1), DateTime.Today)); ;
        }
    }
}
