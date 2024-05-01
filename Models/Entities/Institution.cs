using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using SocialEduApi.Data;
using SocialEduApi.Models.Identity;
using SocialEduApi.Models.ViewModels;

namespace SocialEduApi.Models.Entities
{
    public class Institution
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? PageBackground { get; set; }


        public async Task<IList<UserVM_short>> GetUsersAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            var returned = new List<UserVM_short>();
            var belongses = context.UserInstitutionBelongses.Where(p => p.InstitutionID == Id).ToList();
            foreach (var belongs in belongses)
            {
                var user = await userManager.FindByIdAsync(belongs.UserID);
                returned.Add(new UserVM_short(user));
            }
            return returned;
        }

        public static Faker<Institution> GetFaker()
        {
            return new Faker<Institution>("hr")
                .RuleFor(x => x.Name, y => y.Company.CompanyName())
                .RuleFor(x => x.Abbreviation, y => y.Lorem.Letter(4))
                .RuleFor(x => x.Description, y => y.Lorem.Paragraph())
                //.RuleFor(x => x.Image, y => y.Image.LoremPixelUrl(LoremPixelCategory.Business))
                .RuleFor(x => x.Image, y => "")
                .RuleFor(x => x.PageBackground, y => "");
        }
    }
}
