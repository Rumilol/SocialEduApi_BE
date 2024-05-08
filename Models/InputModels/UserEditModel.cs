using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.InputModels
{
    public class UserEditModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? UserpageBackground { get; set; }
        public List<string>? General { get; set; }
        public List<string>? Interests { get; set; }
        public List<string>? Education { get; set; }
        public List<string>? Experience { get; set; }
        public List<string>? Links{ get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
