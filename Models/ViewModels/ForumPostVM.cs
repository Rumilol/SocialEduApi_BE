using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class ForumPostVM
    {
        public ForumPostVM() { }
        public ForumPostVM(ForumPost post)
        {
            Id = post.Id;
            ForumID = post.ForumID;
            Forum = post.Forum;
            UserEmail = post.User.Email;
                UserFirstName = post.User?.FirstName;
                UserLastName = post.User?.LastName;
                UserImage = post.User?.Image;
            ParentPostID = post.ParentPostID;
            ParentPost = post.ParentPost;
            Content = post.Content;
            CreatedDate = post.CreatedDate;
        }

        public int? Id { get; set; }
        public int? ForumID { get; set; }
        public Forum? Forum { get; set; }
        public string? UserEmail { get; set; }
            public string? UserFirstName { get; set; }
            public string? UserLastName { get; set; }
            public string? UserImage { get; set; }
        public int? ParentPostID { get; set; }
        public ForumPost? ParentPost { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
