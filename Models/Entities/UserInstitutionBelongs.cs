
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.Entities
{
    public class UserInstitutionBelongs
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public ApplicationUser? User { get; set; }
        public int InstitutionID { get; set; }
        public Institution? Institution { get; set; }
        public int RoleID { get; set; }
        public InstitutionRole? Role { get; set; }


    }
}
