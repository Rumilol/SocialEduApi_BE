using SocialEduApi.Models.Entities;

namespace SocialEduApi.Models.ViewModels
{
    public class InstitutionBelongsVM
    {
        public string UserEmail { get; set; }
        public int InstitutionID { get; set; }
        public Institution? Institution { get; set; }
        public string? RoleName { get; set; }
    }
}
