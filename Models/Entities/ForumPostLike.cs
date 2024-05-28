using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.Entities
{
    public class ForumPostLike
    {
        public int Id { get; set; }
        public int ForumPostID { get; set; }
        public ForumPost? ForumPost { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
