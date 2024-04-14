
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


    }
}
