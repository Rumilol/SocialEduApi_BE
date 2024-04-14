
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.Entities
{
    public class SavedUser
    {
        public int Id { get; set; }
        public int FolderID { get; set; }
        public SavedUsersFolder? Folder { get; set; }
        public string UserID { get; set; }
        public ApplicationUser? User { get; set; }


    }
}
