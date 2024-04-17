
using Bogus;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.Entities
{
    public class ForumPost
    {
        public int Id { get; set; }
        public int ForumID { get; set; }
        public Forum? Forum { get; set; }
        public string UserID { get; set; }
        public ApplicationUser? User { get; set; }
        public int? ParentPostID { get; set; }
        public ForumPost? ParentPost { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public static Faker<ForumPost> GetFaker(List<Forum> forums, List<string> userIDs)
        {
            return new Faker<ForumPost>("hr")
                .RuleFor(x => x.Forum, y => y.PickRandom(forums))
                .RuleFor(x => x.UserID, y => y.PickRandom(userIDs))
                .RuleFor(x => x.ParentPostID, y => null)
                .RuleFor(x => x.Content, y => y.Lorem.Paragraph())
                .RuleFor(x => x.CreatedDate, y => y.Date.Between(DateTime.Today.AddYears(-1), DateTime.Today));
        }
    }
}
