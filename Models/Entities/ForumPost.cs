
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


    }
}
