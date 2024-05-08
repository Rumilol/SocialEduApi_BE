using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialEduApi.Data;
using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class UserDetails
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public string? UserpageBackground { get; set; }
        public List<string>? General { get; set; }
        public List<string>? Interests { get; set; }
        public List<string>? Education { get; set; }
        public List<string>? Experience { get; set; }
        public List<string>? Links { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime JoinedDate { get; set; }

        public List<Institution> Institutions { get; set; }
        public List<SavedUsersFolder> SavedUsersFolders { get; set; }
        public List<SubmissionFolder> SubmissionFolders { get; set; }
        public List<string> Roles { get; set; }
        public List<Notification> Notifications { get; set; }

        public UserDetails(ApplicationUser user, ApplicationDbContext context)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Gender = user.Gender;
            Location = user.Location;
            Email = user.Email;
            UserName = user.UserName;
            Description = user.Description;
            Image = user.Image;
            UserpageBackground = user.UserpageBackground;
            General = user.General.Split(";;").ToList();
            Interests = user.Interests.Split(";;").ToList();
            Education = user.Education.Split(";;").ToList();
            Experience = user.Experience.Split(";;").ToList();
            Links = user.Links.Split(";;").ToList();
            DateOfBirth = user.DateOfBirth;
            JoinedDate = user.JoinedDate;
            Institutions = context.UserInstitutionBelongses.Include(p => p.Institution)
                                                           .Where(p => p.UserID == user.Id)
                                                           .Select(p => p.Institution!)
                                                           .ToList();
            
            SavedUsersFolders = context.SavedUsersFolders.Where(p => p.UserID == user.Id).ToList();
            SubmissionFolders = context.SubmissionFolders.Where(p => p.UserID == user.Id).ToList();
            foreach (var folder in SavedUsersFolders)
            {
                folder.GetUsers(context);
            }
            foreach (var folder in SubmissionFolders)
            {
                folder.GetSubmissions(context);
            }
            Notifications = new List<Notification>()
            {
                new Notification(){Timestamp=DateTime.Now, Message="Povratna informacija za zadatak \"Analiza tržišta\" (EPIDI)", Opened=false},
                new Notification(){Timestamp=DateTime.Now, Message="Nove objave na forumu \"Tehnologija i privatnost\" (EPIDI)", Opened=true},
                new Notification(){Timestamp=DateTime.Now.AddDays(-20), Message="Dodani ste na kolegij Elektroničko poslovanje i digitalne inovacije (FIDiT)", Opened=true}
            };
        }

        public async Task FetchRoles(UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.FindByEmailAsync(Email);
            var roles = await userManager.GetRolesAsync(user);
            Roles = roles.ToList();
        }
    }
}
