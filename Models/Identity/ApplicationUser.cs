
using Microsoft.AspNetCore.Identity;

namespace SocialEduApi.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender {  get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string UserpageBackground { get; set; } = string.Empty;
        public string General { get; set; } = string.Empty;
        public string Interests { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public string Links { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public DateTime JoinedDate { get; set; }


    }
}
