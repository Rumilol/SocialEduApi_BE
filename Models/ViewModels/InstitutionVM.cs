using Microsoft.AspNetCore.Identity;
using SocialEduApi.Data;
using SocialEduApi.Models.Entities;
using SocialEduApi.Models.Identity;

namespace SocialEduApi.Models.ViewModels
{
    public class InstitutionVM
    {
        public async Task Init(Institution institution, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            Id = institution.Id;
            Name = institution.Name;
            Abbreviation = institution.Abbreviation;
            Description = institution.Description;
            Image = institution.Image;
            PageBackground = institution.PageBackground;
            Users = await institution.GetUsersAsync(context, userManager);
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? PageBackground { get; set; }
        public IList<UserVM_short> Users { get; set; }
    }
}
