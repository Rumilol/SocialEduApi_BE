using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class UserVM_short
    {
        public UserVM_short() { }  
        public UserVM_short(ApplicationUser user) 
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Gender = user.Gender;
            Location = user.Location;
            Email = user.Email;
            UserName = user.UserName;
            Description = user.Description;
            Image = user.Image;
            General = user.General.Split(";;").ToList();
            Interests = user.Interests.Split(";;").ToList();
            Education = user.Education.Split(";;").ToList();
            Experience = user.Experience.Split(";;").ToList();
            Links = user.Links.Split(";;").ToList();
        }
        public UserVM_short(UserOnSubject userOnSubject)
        {
            FirstName = userOnSubject.User.FirstName;
            LastName = userOnSubject.User.LastName;
            Email = userOnSubject.User.Email;
            UserName = userOnSubject.User.UserName;
            Description = userOnSubject.User.Description;
            Image = userOnSubject.User.Image;
            General = userOnSubject.User.General.Split(";;").ToList();
            Interests = userOnSubject.User.Interests.Split(";;").ToList();
            Education = userOnSubject.User.Education.Split(";;").ToList();
            Experience = userOnSubject.User.Experience.Split(";;").ToList();
            Links = userOnSubject.User.Links.Split(";;").ToList();
        }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? UserName{ get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public List<string>? General { get; set; }
        public List<string>? Interests { get; set; }
        public List<string>? Education { get; set; }
        public List<string>? Experience { get; set; }
        public List<string>? Links { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
