using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class UserOnSubjectVM
    {
        public UserOnSubjectVM() { }
        public UserOnSubjectVM(UserOnSubject userOnSubject)
        {
            Id = userOnSubject.Id;
            UserEmail = userOnSubject.User.Email;
                UserFirstName = userOnSubject.User.FirstName;
                UserLastName = userOnSubject.User.LastName;
                UserImage = userOnSubject.User.Image;
            SubjectID = userOnSubject.SubjectID;
            Subject = userOnSubject.Subject;
            HasAdminRights = userOnSubject.HasAdminRights;
            JoinedDate = userOnSubject.JoinedDate;
        }

        public int? Id { get; set; }
        public string? UserEmail { get; set; }
            public string? UserFirstName { get; set; } = string.Empty;
            public string? UserLastName { get; set; } = string.Empty;
            public string? UserImage { get; set; } = string.Empty;
        public int? SubjectID { get; set; }
        public Subject? Subject { get; set; }
        public bool? HasAdminRights { get; set; }
        public DateTime? JoinedDate { get; set; }
    }
}
