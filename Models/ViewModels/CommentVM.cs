using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class CommentVM
    {
        public CommentVM() { }
        public CommentVM(int? id, string text, UserVM_short user)
        {
            ID = id;
            Text = text;
            UserEmail = user.Email;
            UserFirstName = user.FirstName;
            UserLastName = user.LastName;
            UserImage = user.Image;
        }

        public int? ID { get; set; }
        public string Text { get; set; }
        public string? UserEmail { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public string? UserImage { get; set; }
    }
}
